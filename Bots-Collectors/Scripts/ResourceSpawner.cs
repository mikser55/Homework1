﻿using System.Collections;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _spawnAreaCenter;
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private float _spawnAreaSize;
    [SerializeField] private float _delay;
    [SerializeField] private float _spawnHeight;

    private WaitForSeconds _wait;
    private int _divider = 2;

    private void Start()
    {
        _wait = new (_delay);
        StartCoroutine(SpawnCoroutine());
        
    }

    public void ReleaseResource(Resource resource)
    {
        _pool.ReleaseResource(resource);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (enabled)
        {
            Resource resource = _pool.GetResource();
            resource.transform.position = GetSpawnPosition();
            yield return _wait;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float randomX = Random.Range(-_spawnAreaSize / _divider, _spawnAreaSize / _divider);
        float randomZ = Random.Range(-_spawnAreaSize / _divider, _spawnAreaSize / _divider);

        return new Vector3(_spawnAreaCenter.x + randomX, _spawnHeight, _spawnAreaCenter.y + randomZ);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(_spawnAreaCenter.x, 0, _spawnAreaCenter.y), new Vector3(_spawnAreaSize, 0, _spawnAreaSize));
    }
}