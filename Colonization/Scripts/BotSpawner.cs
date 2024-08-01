using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpawnUnitOrder), typeof(Base))]
public class BotSpawner : MonoBehaviour
{
    private const int FirstChild = 0;

    [SerializeField] private float _spawnRadius = 20;
    [SerializeField] private Bot _botPrefab;

    [SerializeField] private BaseSpawner _baseSpawner;
    private Transform _collectPoint;
    private Base _base;
    private SpawnUnitOrder _spawnUnitOrder;

    public event Action<Bot> BotSpawned;

    private void Awake()
    {
        _spawnUnitOrder = GetComponent<SpawnUnitOrder>();
        _base = GetComponent<Base>();
        _collectPoint = transform.GetChild(FirstChild);
    }

    private void OnEnable()
    {
        _spawnUnitOrder.SpawnUnitClicked += SpawnBot;
    }

    private void OnDisable()
    {
        _spawnUnitOrder.SpawnUnitClicked -= SpawnBot;
    }

    public void Init(BaseSpawner baseSpawner)
    {
        _baseSpawner = baseSpawner;
        _baseSpawner.BaseSpawned += SpawnBot;
    }

    private void SpawnBot()
    {
        if (_base.ResourcesAmount >= _base.BotCost)
        {
            Vector2 randomOffset = Random.insideUnitCircle * _spawnRadius;
            Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, 0, randomOffset.y);

            if (Physics.Raycast(spawnPosition, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            {
                spawnPosition = hit.point;
                Bot bot = Instantiate(_botPrefab, spawnPosition, Quaternion.identity);
                _baseSpawner.Subscribe(bot);

                if (bot.TryGetComponent(out BotMover mover))
                {
                    mover.Init(_collectPoint);
                }

                BotSpawned?.Invoke(bot);
            }
        }
    }
}