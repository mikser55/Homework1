using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(_playerData.AttackDamage);
        }
    }
}