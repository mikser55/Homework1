public class BombPoolManager : PoolManager<Bomb>
{
    public Bomb GetBomb()
    {
        return Pool.Get();
    }

    public void ReturnBomb(Bomb bomb)
    {
        Pool.Release(bomb);
    }

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