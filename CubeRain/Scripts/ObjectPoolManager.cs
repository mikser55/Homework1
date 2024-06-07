using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private CubeData _cubeData;
    [SerializeField] private Cube _cubePrefab;

    private Color _primalColor = Color.yellow;
    private ObjectPool<Cube> _cubes;

    private void Awake()
    {
        _cubes = new(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject);
    }
    
    public Cube GetCube()
    {
        return _cubes.Get();
    }

    public void ReturnCube(Cube cube)
    {
        _cubes.Release(cube);
    }

    private void OnDestroyPoolObject(Cube cube)
    {
        Destroy(cube);
    }

    private void OnReturnedToPool(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.Init(_primalColor);
    }

    private Cube CreatePooledItem()
    {
        Cube cube = Instantiate(_cubePrefab);
        cube.TakeObjectPool(this);
        cube.gameObject.SetActive(false);

        return cube;
    }
}