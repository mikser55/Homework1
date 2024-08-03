using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Color = UnityEngine.Color;

[RequireComponent(typeof(BotSpawner), typeof(Renderer), typeof(BaseFlagger))]
public class Base : MonoBehaviour
{
    private const int NewBaseCost = 5;

    private Transform _currentFlag;
    private BaseColorChanger _colorChanger;
    private BaseFlagger _flagger;
    private Scanner _scanner;
    private BotSpawner _botSpawner;
    private List<Bot> _bots = new();

    private readonly Dictionary<Resource, bool> _resources = new();

    public event Action ResourceAmountChanged;

    public IReadOnlyList<Bot> Bots => _bots.AsReadOnly();
    public IReadOnlyDictionary<Resource, bool> NearbyResources => _resources;

    public Color OriginalColor { get; private set; } = Color.blue;
    public int BotCost { get; private set; } = 3;
    public int ResourcesAmount { get; private set; } = 6;
    public bool IsSelected { get; private set; }

    private void Awake()
    {
        _colorChanger = GetComponent<BaseColorChanger>();
        _scanner = GetComponentInChildren<Scanner>();
        _botSpawner = GetComponent<BotSpawner>();
        _flagger = GetComponent<BaseFlagger>();
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    public int GetBotsCount()
    {
        return _bots.Count;
    }

    public void OrderToCollect(Bot bot, Transform resourceInstance)
    {
        if (resourceInstance != null && !bot.IsReserved)
        {
            if (bot.TryGetComponent(out BotMover botMover))
                botMover.MoveToResource(resourceInstance);

        }
        else
        {
            if (_currentFlag != null)
            {
                OrderToBuildNewBase(_currentFlag);
            }
        }
    }

    public class Factory : PlaceholderFactory<Base> { }

    private void OrderToBuildNewBase(Transform flag)
    {
        Bot bot = _bots[0];
        bot.SetReserved();

        if (bot.IsBusy == false && bot.IsReserved)
        {
            if (bot.TryGetComponent(out BotMover botMover))
            {
                ResourcesAmount -= NewBaseCost;
                ResourceAmountChanged?.Invoke();
                botMover.MoveToNewBase(flag);
            }
        }
        else
        {
            _currentFlag = flag;
        }
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
        resource.OnWaiting(resource);
    }

    private void FillResources(Dictionary<Resource, bool> resources)
    {
        foreach (var obj in resources)
            if (!_resources.ContainsKey(obj.Key))
                _resources.Add(obj.Key, obj.Value);
    }

    private void SetSelected()
    {
        IsSelected = true;
    }

    private void SetUnselected()
    {
        IsSelected = false;
    }

    private void Subscribe()
    {
        _colorChanger.Selected += SetSelected;
        _colorChanger.Unselected += SetUnselected;
        _botSpawner.BotSpawned += AddNewBot;
        _scanner.FillingStarted += FillResources;
        _flagger.CanBuildNewBase += OrderToBuildNewBase;
    }

    private void Unsubscribe()
    {
        foreach (Bot bot in _bots)
            bot.ResourceArrived -= DeleteCollectedResource;

        _colorChanger.Selected -= SetSelected;
        _colorChanger.Unselected -= SetUnselected;
        _botSpawner.BotSpawned -= AddNewBot;
        _scanner.FillingStarted -= FillResources;
    }

    private void DeleteBot(Bot bot)
    {
        _bots.Remove(bot);
    }

    private void AddNewBot(Bot bot)
    {
        ResourcesAmount -= BotCost;
        ResourceAmountChanged?.Invoke();
        bot.Init(this);
        _bots.Add(bot);
        bot.ResourceArrived += DeleteCollectedResource;
        if (bot.TryGetComponent(out BotMover mover))
            mover.ToNewBaseArrived += DeleteBot;
    }
}