using System;
using UnityEngine;

[RequireComponent(typeof(BotMover))]
public class Bot : MonoBehaviour
{
    private BotMover _mover;
    private Transform _botTransform;
    private Transform _currentResource;

    public event Action<Resource> ResourceArrived;
    public event Action<BasePoint> Arrived;
    public event Action<BasePoint> Left;

    public bool IsCarryingResource { get; private set; }
    public bool IsBusy { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<BotMover>();
        _botTransform = transform;
    }

    private void OnEnable()
    {
        _mover.BotMoveStarted += SetBusy;
        _mover.ToResourceArrived += CollectResourse;
        _mover.ToBaseArrived += DropResourse;
    }

    private void OnDisable()
    {
        _mover.BotMoveStarted -= SetBusy;
        _mover.ToResourceArrived -= CollectResourse;
        _mover.ToBaseArrived -= DropResourse;
    }

    public void OnLeft(BasePoint point)
    {
        Left?.Invoke(point);
    }

    public void OnArrived(BasePoint point)
    {
        Arrived?.Invoke(point);
    }

    private void DropResourse()
    {
        _currentResource.transform.SetParent(null);
        IsBusy = false;
        IsCarryingResource = false;

        if (_currentResource.TryGetComponent(out Resource resource))
            ResourceArrived?.Invoke(resource);
    }

    private void CollectResourse(Transform target)
    {
        target.transform.SetParent(_botTransform);
        IsCarryingResource = true;
        _mover.MoveToCollectPoint();
    }

    public void TakeCurrentResource(Transform resource)
    {
        _currentResource = resource;
    }

    private void SetBusy()
    {
        IsBusy = true;
    }
}