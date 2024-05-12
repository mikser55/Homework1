using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable, IHealeable
{
    [SerializeField] private PlayerData _playerData;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _playerData.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Die();    
    }

    public void Heal(int healValue)
    {
        _currentHealth += healValue;

        if( _currentHealth > _playerData.MaxHealth)
            _currentHealth = _playerData.MaxHealth;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}