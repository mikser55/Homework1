using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private const bool BasePointIsFree = true;

    [SerializeField] private Sensor _sensor;
    [SerializeField] private List<Bot> _bots;
    [SerializeField] private ResourceSpawner _resourcesSpawner;
    [SerializeField] private List<BasePoint> _basePoints;

    private readonly Dictionary<Resource, bool> _resources = new();
    private readonly Dictionary<BasePoint, bool> _points = new();

    public event Action ResourceAmountChanged;

    public int ResourcesAmount { get; private set; }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Awake()
    {
        foreach (BasePoint point in _basePoints)
        {
            _points.Add(point, BasePointIsFree);
        }
    }

    private void OrderToCollect()
    {
        if (_bots.Count > 0)
        {
            foreach (Bot bot in _bots)
            {
                if (bot.IsBusy == false)
                {
                    Transform randomResource = GetResource();

                    if (randomResource != null)
                    {
                        if (bot.TryGetComponent(out BotMover botMover))
                            botMover.MoveToResource(randomResource);
                    }
                }
            }
    }
}

    private Transform GetResource()
    {
        foreach (var obj in _resources)
        {
            if (obj.Value == false)
            {
                _resources[obj.Key] = true;
                return obj.Key.transform;
            }
        }

        return null;
    }

    private void IncreaseResourcesAmount()
    {
        ResourcesAmount++;
        ResourceAmountChanged?.Invoke();
    }

    private void DeleteCollectedResource(Resource resource)
    {
        _resources.Remove(resource);
        IncreaseResourcesAmount();
        _resourcesSpawner.ReleaseResource(resource);
    }

    private void FillResources(Dictionary<Resource, bool> resources)
    {
        foreach (var obj in resources)
        {
            if (!_resources.ContainsKey(obj.Key))
                _resources.Add(obj.Key, obj.Value);
        }

        OrderToCollect();
    }

    public BasePoint GetFreePoint()
    {
        foreach (var obj in _points)
        {
            if (obj.Value)
            {
                _points[obj.Key] = false;
                return obj.Key;
            }
        }

        return null;
    }

    private void SetFreeBasePoint(BasePoint point)
    {
        _points[point] = true;
    }

    private void ReserveBasePoint(BasePoint point)
    {
        _points[point] = false;
    }

    private void Subscribe()
    {
        foreach (Bot bot in _bots)
        {
            bot.ResourceArrived += DeleteCollectedResource;
            bot.Arrived += ReserveBasePoint;
            bot.Left += SetFreeBasePoint;
        }

        _sensor.FillingStarted += FillResources;
    }

    private void Unsubscribe()
    {
        foreach (Bot bot in _bots)
        {
            bot.ResourceArrived -= DeleteCollectedResource;
            bot.Arrived -= ReserveBasePoint;
            bot.Left -= SetFreeBasePoint;
        }

        _sensor.FillingStarted -= FillResources;
    }
}