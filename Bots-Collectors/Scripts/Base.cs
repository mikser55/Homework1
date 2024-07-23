using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private const bool IsFree = true;

    [SerializeField] Sensor _sensor;
    [SerializeField] private List<Bot> _bots;
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private List<BasePoint> _basePoints;

    private Dictionary<Resource, bool> _resources = new();
    private Dictionary<BasePoint, bool> _points = new();

    public event Action ResourceChanged;

    public int ResourcesAmount { get; private set; }

    private void OnEnable()
    {
        _sensor.ResourseFinded += OrderToCollect;
        _pool.ResourceCollected += IncreaseResourcesAmount;
    }

    private void OnDisable()
    {
        _sensor.ResourseFinded -= OrderToCollect;
        _pool.ResourceCollected -= IncreaseResourcesAmount;
    }

    private void Awake()
    {
        foreach (BasePoint point in _basePoints)
        {
            _points.Add(point, IsFree);
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
                    Transform randomResource = GetRandomResource();

                    if (randomResource != null)
                    {
                        bot.TryGetComponent(out BotMover botMover);

                        if (botMover != null)
                            botMover.MoveToResource(randomResource);
                    }
                }
            }
        }
    }

    private Transform GetRandomResource()
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
        ResourceChanged?.Invoke();
    }

    public void DeleteCollectedResource(Resource resource)
    {
        _resources.Remove(resource);
    }

    public void FillResources(Dictionary<Resource, bool> resources)
    {
        foreach (var obj in resources)
        {
            if (!_resources.ContainsKey(obj.Key))
                _resources.Add(obj.Key, obj.Value);
        }
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

    public void SetFreeBasePoint(BasePoint point)
    {
        _points[point] = true;
    }

    public void ReserveBasePoint(BasePoint point)
    {
        _points[point] = false;
    }
}