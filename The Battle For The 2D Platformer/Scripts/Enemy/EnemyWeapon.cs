using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Vector3 _attackOffset;
    [SerializeField] private LayerMask _attackMask;
    
    public void Attack()
    {
        Vector3 currentPosition = transform.position;
        currentPosition += transform.right * _attackOffset.x;
        currentPosition += transform.up * _attackOffset.y;

        Collider2D detectedCollider = Physics2D.OverlapCircle(currentPosition, _enemyData.EnemyAttackRange, _attackMask);

        if (detectedCollider != null)
            detectedCollider.GetComponent<PlayerStats>().TakeDamage(_enemyData.EnemyDamage);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 currentPosition = transform.position;
        currentPosition += transform.right * _attackOffset.x;
        currentPosition += transform.up * _attackOffset.y;

        Gizmos.DrawWireSphere(currentPosition, _enemyData.EnemyAttackRange);
    }
}