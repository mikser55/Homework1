using System;
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
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Base _base;

    private Bot _bot;
    private Transform _botTransform;
    private Transform _target;
    private BasePoint _currentBasePoint;
    private Coroutine _currentCoroutine;

    private float _sqrArriveDistance;

    private bool _isForCollecting;

    public event Action BotMoveStarted;
    public event Action ToBaseArrived;
    public event Action<Transform> ToResourceArrived;

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
        BotMoveStarted?.Invoke();
        _isForCollecting = true;
        _bot.OnLeft(_currentBasePoint);
        _currentBasePoint = null;
        StartMove();
    }

    private IEnumerator MoveCoroutine()
    {
        if (_target != null)
        {
            while ((_target.position - _botTransform.position).sqrMagnitude > _sqrArriveDistance)
            {
                _botTransform.position = Vector3.MoveTowards(_botTransform.position, _target.position, _speed * Time.deltaTime);
                ChangeRotation();
                yield return null;
            }

            if (_isForCollecting)
            {
                ToResourceArrived?.Invoke(_target);
            }
            else if (_isForCollecting == false && _bot.IsCarryingResource)
            {
                ToBaseArrived?.Invoke();
                MoveToBase();
            }
        }
    }

    private void MoveToBase()
    {
        FindFreePoint();
        StartMove();
    }

    public void MoveToCollectPoint()
    {
        _target = _collectPoint;
        _isForCollecting = false;
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
        _currentBasePoint = _base.GetFreePoint();
        _target = _currentBasePoint.transform;
    }

    private void ChangeRotation()
    {
        Vector3 direction = (_target.position - _botTransform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        _botTransform.rotation = Quaternion.Slerp(_botTransform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }
}