public class BombPoolManager : PoolManager<Bomb>
{
    public int GetNumberActiveObjects()
    {
        return Pool.CountActive;
    }

    protected override void OnDestroyPoolObject(Bomb bomb)
    {
        base.OnDestroyPoolObject(bomb);
    }

    protected override void OnReturnToPool(Bomb bomb)
    {
        base.OnReturnToPool(bomb);
    }

    protected override void OnTakeFromPool(Bomb bomb)
    {
        base.OnTakeFromPool(bomb);
    }

    protected override Bomb CreatePooledItem()
    {
        Bomb bomb = Instantiate(Prefab);
        bomb.TakeObjectPool(this);
        OnCountChanged();

        return bomb;
    }
}