using UnityEngine;
using UnityEngine.Pool;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Resource _prefab;

    private ObjectPool<Resource> _pool;

    private void Awake()
    {
        _pool = new(CreateResource, OnGet, OnRelease, OnDestroyResource);
    }

    public Resource GetResource()
    {
        return _pool.Get();
    }

    public void ReleaseResource(Resource resource)
    {
        _pool.Release(resource);
    }

    private void OnDestroyResource(Resource resource)
    {
        Destroy(resource);
    }

    private void OnRelease(Resource resource)
    {
        resource.gameObject.SetActive(false);
    }

    private void OnGet(Resource resource)
    {
        resource.gameObject.SetActive(true);
    }

    private Resource CreateResource()
    {
        return Instantiate(_prefab);
    }
}