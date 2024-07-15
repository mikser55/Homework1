using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyPool))]
public partial class EnemyWeapon : MonoBehaviour
{
    private EnemyPool _pool;
    private Coroutine _coroutine;
    private float _attackDelay = 1f;

    private void Awake()
    {
        _pool = GetComponent<EnemyPool>();
    }

    public void Shoot()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_attackDelay);

        while (enabled)
        {
            _pool.Init(this.transform);
            _pool.GetObject();

            yield return wait;
        }
    }
}