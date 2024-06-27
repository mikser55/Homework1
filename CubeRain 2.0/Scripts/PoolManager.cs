using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class PoolManager<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;

    public event Action ObjectsCountChanged;

    public int AllCreatedObjects { get; private set; } = 0;

    private void Awake()
    {
        _pool = InitializePool();
    }

    public int GetNumberActiveObjects()
    {
        return _pool.CountActive;
    }

    public T GetObject()
    {
        return _pool.Get();
    }

    public void ReturnObject(T obj)
    {
        _pool.Release(obj);
    }

    protected virtual T CreatePooledItem()
    {
         return Instantiate(_prefab);
    }

    protected void OnDestroyPoolObject(T instance)
    {
        Destroy(instance.gameObject);
    }

    protected virtual void OnReturnToPool(T instance)
    {
        instance.gameObject.SetActive(false);
    }

    protected virtual void OnTakeFromPool(T instance)
    {
        instance.gameObject.SetActive(true);
        AllCreatedObjects++;
        OnCountChanged();
    }

    protected void OnCountChanged()
    {
        ObjectsCountChanged?.Invoke();
    }

    private ObjectPool<T> InitializePool()
    {
        return new ObjectPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject);
    }
}