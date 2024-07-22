using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bot))]
public class BotMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _arriveDistance;
    [SerializeField] private List<BasePoint> _basePoints;
    [SerializeField] private Transform _collectPoint;

    private Bot _bot;
    private Transform _botTransform;
    private Transform _target;
    private BasePoint _currentBasePoint;
    private Coroutine _currentCoroutine;

    private float _sqrArriveDistance;

    private bool _isForCollecting;
    private bool _isCarryingResource;

    private void Awake()
    {
        _bot = GetComponent<Bot>();
    }

    private void Start()
    {
        _botTransform = transform;
        _sqrArriveDistance = _arriveDistance * _arriveDistance;
        MoveToBase();
    }

    public void MoveToResource(Transform resourсe)
    {
        _target = resourсe;
        _bot.TakeCurrentResource(resourсe);
        _bot.SetBusy();
        _isForCollecting = true;
        _currentBasePoint.SetFree();
        _currentBasePoint = null;
        StartCoroutine();
    }

    private IEnumerator MoveCoroutine()
    {
        if (_target != null)
        {
            while ((_target.position - _botTransform.position).sqrMagnitude > _sqrArriveDistance)
            {
                _botTransform.position = Vector3.MoveTowards(_botTransform.position, _target.position, _speed * Time.deltaTime);
                yield return null;
            }

            if (_isForCollecting)
                _bot.CollectResourse(_target);
            else if (_isForCollecting == false && _bot.IsCarryingResource)
            {
                _bot.DropResourse();
                MoveToBase();
            }
        }
    }

    private void MoveToBase()
    {
        FindFreePoint();
        StartCoroutine();
    }

    public void MoveToCollectPoint()
    {
        _target = _collectPoint;
        _isForCollecting = false;
        StartCoroutine();
    }

    private void StartCoroutine()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(MoveCoroutine());
        }
        else
        {
            _currentCoroutine = StartCoroutine(MoveCoroutine());
        }
    }

    private void FindFreePoint()
    {
        if (_basePoints.Count > 0)
        {
            foreach (var basePoint in _basePoints)
            {
                if (basePoint.IsFree)
                {
                    _target = basePoint.transform;
                    basePoint.ReservePoint();
                    _currentBasePoint = basePoint;
                    return;
                }
            }
        }
    }
}