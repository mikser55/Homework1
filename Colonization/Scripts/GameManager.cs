using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const float UpdateInterval = 6f;

    [SerializeField] private List<Base> _bases = new();

    private readonly Dictionary<Resource, bool> _allResources = new();

    private void Start()
    {
        StartCoroutine(UpdateResourcesRoutine());
    }

    public void AddBase(Base baseInstance)
    {
        if (!_bases.Contains(baseInstance))
        {
            _bases.Add(baseInstance);
        }
    }

    private IEnumerator UpdateResourcesRoutine()
    {
        WaitForSeconds wait = new(UpdateInterval);

        while (true)
        {
            AssignBotsToResources();
            yield return wait;
        }
    }

    private void AssignBotsToResources()
    {
        List<Bot> availableBots = _bases.SelectMany(bases => bases.Bots).Where(bot => bot.IsBusy == false).ToList();
        availableBots = availableBots.OrderBy(randomPosition => Random.value).ToList();

        foreach (Base baseInstance in _bases)
        {
            CollectAllResources();

            List<Resource> unassignedResources = _allResources
                .Where(pair => pair.Value == false && !_bases.Any(_base => _base != baseInstance &&
                _allResources.ContainsKey(pair.Key) && _allResources[pair.Key]))
                .Select(pair => pair.Key)
                .OrderBy(resource => Vector3.Distance(baseInstance.transform.position, resource.transform.position))
                .ToList();

            foreach (Resource resource in unassignedResources)
            {
                Bot bot = availableBots.FirstOrDefault(bot => bot.Base == baseInstance);

                if (bot != null)
                {
                    baseInstance.OrderToCollect(bot, resource.transform);
                    availableBots.Remove(bot);

                    if (_allResources.ContainsKey(resource))
                        _allResources[resource] = true;
                }
                else
                {
                    break;
                }
            }
        }
    }

    private void CollectAllResources()
    {
        _allResources.Clear();

        foreach (Base baseInstance in _bases)
            foreach (var resource in baseInstance.NearbyResources)
                if (!_allResources.ContainsKey(resource.Key))
                    _allResources.Add(resource.Key, resource.Value);
    }
}