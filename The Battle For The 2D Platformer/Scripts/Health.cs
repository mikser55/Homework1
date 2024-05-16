using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealeable
{
    [SerializeField] private HealthData _healthData;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _healthData.MaxHealth;
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

        Mathf.Clamp(_currentHealth, 0, _healthData.MaxHealth);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}