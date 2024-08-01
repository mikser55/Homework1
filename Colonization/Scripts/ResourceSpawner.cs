using System.Collections;
using UnityEngine;
using Zenject;

public class ResourceSpawner : MonoBehaviour
{
    private const int Divider = 2;

    [SerializeField] private Vector2 _spawnAreaCenter;
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private float _spawnAreaSize;
    [SerializeField] private float _delay;
    [SerializeField] private float _spawnHeight;
    [SerializeField] private GameManager _gameManager;

    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new(_delay);
        StartCoroutine(SpawnCoroutine());
        
    }

    public void ReleaseResource(Resource resource)
    {
        resource.ReleaseWaiting -= ReleaseResource;
        _pool.ReleaseResource(resource);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (enabled)
        {
            Resource resource = _pool.GetResource();
            _gameManager.AddResource(resource);
            resource.ReleaseWaiting += ReleaseResource;
            resource.transform.position = GetSpawnPosition();
            yield return _wait;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float randomX = Random.Range(-_spawnAreaSize / Divider, _spawnAreaSize / Divider);
        float randomZ = Random.Range(-_spawnAreaSize / Divider, _spawnAreaSize / Divider);

        return new Vector3(_spawnAreaCenter.x + randomX, _spawnHeight, _spawnAreaCenter.y + randomZ);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(_spawnAreaCenter.x, 0, _spawnAreaCenter.y), new Vector3(_spawnAreaSize, 0, _spawnAreaSize));
    }
}