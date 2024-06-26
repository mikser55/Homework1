using UnityEngine;

public class CubePoolManager : PoolManager<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;

    private Color _primalColor = Color.yellow;

    protected override void OnReturnToPool(Cube cube)
    {
        base.OnReturnToPool(cube);
        _bombSpawner.Spawn(cube.transform.position);
    }

    protected override void OnTakeFromPool(Cube cube)
    {
        base.OnTakeFromPool(cube);
        cube.Init(_primalColor);
    }

    protected override Cube CreatePooledItem()
    {
        Cube cube = base.CreatePooledItem();
        cube.TakeObjectPool(this);
        cube.gameObject.SetActive(false);
        OnCountChanged();

        return cube;
    }
}