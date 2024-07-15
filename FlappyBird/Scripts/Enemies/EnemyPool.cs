using UnityEngine;

public class EnemyPool : PoolManager<EnemyBullet>
{
    private Transform _transform;
    private float _offset = 1f;

    public void Init(Transform transform)
    {
        _transform = transform;
    }

    protected override void OnTakeFromPool(EnemyBullet instance)
    {
        base.OnTakeFromPool(instance);
        instance.Initialize(this);
        Vector3 newPosition = new Vector3(_transform.position.x - _offset, _transform.position.y, _transform.position.z);
        instance.transform.position = newPosition;
    }
}