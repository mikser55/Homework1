using System;
using UnityEngine;
using UnityEngine.Pool;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Resource _prefab;

    private ObjectPool<Resource> _pool;

    public event Action ResourceCollected;

    private void Awake()
    {
        _pool = new(CreateObject, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    public Resource GetResource()
    {
        return _pool.Get();
    }

    public void ReleaseResource(Resource resource)
    {
        _pool.Release(resource);
    }

    private void ActionOnDestroy(Resource resource)
    {
        Destroy(resource);
    }

    private void ActionOnRelease(Resource resource)
    {
        ResourceCollected?.Invoke();
        resource.gameObject.SetActive(false);
    }

    private void ActionOnGet(Resource resource)
    {
        resource.gameObject.SetActive(true);
        resource.Init();
    }

    private Resource CreateObject()
    {
        return Instantiate(_prefab);
    }
}