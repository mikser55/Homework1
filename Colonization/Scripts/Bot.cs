using System;
using UnityEngine;

[RequireComponent(typeof(BotMover))]
public class Bot : MonoBehaviour
{
    private BotMover _mover;
    private Transform _transform;
    private Transform _currentResource;

    public event Action<Resource> ResourceArrived;

    public Base Base { get; private set; }
    public bool IsCarryingResource { get; private set; }
    public bool IsBusy { get; private set; }
    public bool IsReserved { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<BotMover>();
        _transform = transform;
    }

    private void OnEnable()
    {
        _mover.BotMoveStarted += SetBusy;
        _mover.ToResourceArrived += CollectResourse;
        _mover.ToCollectPointArrived += DropResourse;
    }

    private void OnDisable()
    {
        _mover.BotMoveStarted -= SetBusy;
        _mover.ToResourceArrived -= CollectResourse;
        _mover.ToCollectPointArrived -= DropResourse;
    }

    public void Init(Base baseInstance)
    {
        Base = baseInstance;
    }

    public void SetReserved()
    {
        IsReserved = true;
    }

    public void TakeCurrentResource(Transform resource)
    {
        _currentResource = resource;
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
        target.transform.SetParent(_transform);
        IsCarryingResource = true;
        _mover.MoveToCollectPoint();
    }

    private void SetBusy()
    {
        IsBusy = true;
    }
}