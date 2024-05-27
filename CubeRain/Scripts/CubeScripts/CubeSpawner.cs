using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeSpawnerData _data;
    [SerializeField] private ObjectPoolManager _poolManager;
    [SerializeField] private Ground _ground;

    private MeshRenderer _groundMeshRenderer;
    private float _planeSizeX;
    private float _planeSizeZ;

    private void Awake()
    {
        _groundMeshRenderer = _ground.GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        if (_groundMeshRenderer != null)
        {
            _planeSizeX = _groundMeshRenderer.bounds.size.x;
            _planeSizeZ = _groundMeshRenderer.bounds.size.z;
        }

        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_data.SpawnTime);

        while (_data.IsWork)
        {
            Vector3 spawnPosition = GetRandomPositionAbovePlane();
            yield return wait;
            Cube newCube = _poolManager.GetCube();
            newCube.transform.position = spawnPosition;
        }
    }

    private Vector3 GetRandomPositionAbovePlane()
    {
        float halfSizeX = _planeSizeX / _data.HalfSizeCofficient;
        float halfSizeZ = _planeSizeZ / _data.HalfSizeCofficient;
        float planePositionX = _ground.transform.position.x;
        float planePositionZ = _ground.transform.position.z;

        float xRandomPosition = Random.Range(planePositionX - halfSizeX, planePositionX + halfSizeX);
        float zRandomPosition = Random.Range(planePositionZ - halfSizeZ, planePositionZ + halfSizeZ);

        return new Vector3(xRandomPosition, _data.YSpawnPosition, zRandomPosition);
    }
}