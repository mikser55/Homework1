using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class PoolManager<T> : MonoBehaviour where T : MonoBehaviour
{
    public event Action ObjectsCountChanged;

    [SerializeField] protected T Prefab;

    protected ObjectPool<T> Pool;

    public int AllCreatedObjects { get; protected set; } = 0;

    protected void Awake()
    {
        Pool = new ObjectPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject);
    }

    public T GetObject()
    {
        return Pool.Get();
    }

    public void ReturnObject(T obj)
    {
        Pool.Release(obj);
    }

    protected abstract T CreatePooledItem();

    protected virtual void OnDestroyPoolObject(T instance)
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
}