using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Bot))]
public class BotMover : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _arriveDistance = 2f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _radiusBasePoint = 10;

    private Transform _collectPoint;
    private Bot _bot;
    private Transform _transform;
    private Vector3 _targetPosition;
    private Vector3 _currentBasePoint;
    private Coroutine _currentCoroutine;
    private float _sqrArriveDistance;

    private bool _isForCollecting;
    private bool _toNewBase;

    public event Action BotMoveStarted;
    public event Action ToCollectPointArrived;
    public event Action<Transform> ToResourceArrived;
    public event Action<Bot> ToNewBaseArrived;

    public Transform Target { get; private set; }

    private void Awake()
    {
        _bot = GetComponent<Bot>();
    }

    private void Start()
    {
        _transform = transform;
        _sqrArriveDistance = _arriveDistance * _arriveDistance;
        MoveToBasePoint();
    }
 
    public void Init(Transform collectPoint)
    {
        _collectPoint = collectPoint;
    }

    public void MoveToResource(Transform resourсe)
    {
        Target = resourсe;
        _targetPosition = resourсe.position;
        _bot.TakeCurrentResource(resourсe);
        BotMoveStarted?.Invoke();
        _isForCollecting = true;
        StartMove();
    }

    public void MoveToCollectPoint()
    {
        _targetPosition = _collectPoint.position;
        _isForCollecting = false;
        StartMove();
    }

    public void MoveToNewBase(Transform flag)
    {
        Target = flag;
        _targetPosition = flag.position;
        _toNewBase = true;
        StartMove();
    }

    private IEnumerator MoveCoroutine()
    {
        if (Target != null)
        {
            while ((_targetPosition - _transform.position).sqrMagnitude > _sqrArriveDistance)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, _targetPosition, _speed * Time.deltaTime);
                ChangeRotation();
                yield return null;
            }

            TakeNewOrder();
        }
    }

    private void TakeNewOrder()
    {
        if (_isForCollecting)
        {
            ToResourceArrived?.Invoke(Target);
        }
        else if (_isForCollecting == false && _bot.IsCarryingResource)
        {
            ToCollectPointArrived?.Invoke();
            MoveToBasePoint();
        }
        else if (_toNewBase)
        {
            _toNewBase = false;
            ToNewBaseArrived?.Invoke(_bot);
        }
    }

    private void MoveToBasePoint()
    {
        FindFreePoint();
        StartMove();
    }

    private void StartMove()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(MoveCoroutine());
    }

    private void FindFreePoint()
    {
        Vector2 randomOffset = Random.insideUnitCircle * _radiusBasePoint;
        Vector3 basePointPosition = _collectPoint.transform.position + new Vector3(randomOffset.x, 0, randomOffset.y);
        _currentBasePoint = basePointPosition;
        _targetPosition = _currentBasePoint;
    }

    private void ChangeRotation()
    {
        Vector3 direction = (_targetPosition - _transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        }
    }
}