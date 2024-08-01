using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const float UpdateInterval = 5f;

    [SerializeField] private List<Base> _bases = new();

    private List<Resource> _allResources = new();

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

    public void AddResource(Resource resource)
    {
        if (!_allResources.Contains(resource))
        {
            _allResources.Add(resource);
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
        availableBots = availableBots.OrderBy(x => Random.value).ToList();

        foreach (Base baseInstance in _bases)
        {
            List<Resource> unassignedResources = baseInstance.NearbyResources
                .Where(pair => pair.Value == false && !_bases.Any(_base => _base != baseInstance &&
                _base.NearbyResources.ContainsKey(pair.Key) && _base.NearbyResources[pair.Key]))
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
                }
                else
                {
                    break;
                }
            }
        }
    }
}