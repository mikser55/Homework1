using UnityEngine;

public class PlayerPool : PoolManager<PlayerBullet>
{
    [SerializeField] private Transform _gunPosition;

    protected override void OnTakeFromPool(PlayerBullet instance)
    {
        base.OnTakeFromPool(instance);
        instance.transform.position = _gunPosition.position;
        instance.Initialize(this);
    }
}