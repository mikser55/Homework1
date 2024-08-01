using System;
using UnityEngine;
using Zenject;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private Base.Factory _factory;

    public event Action BaseSpawned;

    [Inject]
    private void Constructor(Base.Factory factory)
    {
        _factory = factory;
    }

    public void Subscribe(Bot bot)
    {
        if (bot.TryGetComponent(out BotMover botMover))
            botMover.ToNewBaseArrived += CreateBase;
    }

    private void CreateBase(Bot bot)
    {
        Base _base = _factory.Create();
        _gameManager.AddBase(_base);

        if (_base.TryGetComponent(out BotSpawner spawner))
            spawner.Init(this);

        _base.transform.position = bot.transform.position + new Vector3(0,5,0);
        Destroy(bot.gameObject);

        if(bot.TryGetComponent(out BotMover mover))
            Destroy(mover.Target.gameObject);

        BaseSpawned?.Invoke();
    }
}