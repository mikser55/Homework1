using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Base : MonoBehaviour
{
    [SerializeField] Sensor _sensor;
    [SerializeField] private List<Bot> _bots;
    [SerializeField] private ResourcePool _pool;

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
        Resource resource = _sensor.Resources[Random.Range(0, _sensor.Resources.Count)];

        if (resource.IsCollecting == false)
        {
            resource.ReserveResource();

            return resource.transform;
        }

        return null;
    }

    private void IncreaseResourcesAmount()
    {
        ResourcesAmount++;
        ResourceChanged?.Invoke();
    }
}