using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _delay;
    [SerializeField] private LayerMask _layerMask;

    private WaitForSeconds _wait;

    public event Action<Dictionary<Resource,bool>> FillingStarted;

    private void Start()
    {
        _wait = new(_delay);
        StartCoroutine(SearchCoroutine());
    }

    private Dictionary<Resource, bool> FindResourses()
    {
        Dictionary<Resource, bool> resources = new();

        Collider[] nearObjects = Physics.OverlapSphere(transform.position, _radius, _layerMask);

        foreach (var obj in nearObjects)
        {
            obj.TryGetComponent(out Resource resource);

            if (resource != null)
                resources.Add(resource, false);
        }

        return resources;
    }

    private IEnumerator SearchCoroutine()
    {
        while (enabled)
        {
            FillingStarted?.Invoke(FindResourses());
            yield return _wait;
        }
    }
}