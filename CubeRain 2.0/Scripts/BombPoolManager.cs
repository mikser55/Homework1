using UnityEngine.Pool;

public class BombPoolManager : PoolManager<Bomb>
{
    protected override Bomb CreatePooledItem()
    {
        Bomb bomb = base.CreatePooledItem();
        bomb.TakeObjectPool(this);
        OnCountChanged();

        return bomb;
    }
}