using UnityEngine;
using UnityEngine.Pool;

public abstract class PoolManager<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new(CreateObjects, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject);
    }

    public void GetObject()
    {
        _pool.Get();
    }

    public void ReturnObject(T instance)
    {
        _pool.Release(instance);
    }

    protected void OnDestroyPoolObject(T instance)
    {
        Destroy(instance.gameObject);
    }

    protected void OnReturnToPool(T instance)
    {
        instance.gameObject.SetActive(false);
    }

    protected virtual void OnTakeFromPool(T instance)
    {
        instance.gameObject.SetActive(true);
    }

    protected virtual T CreateObjects()
    {
        return Instantiate(_prefab);
    }
}