using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable, IHealeable
{
    [SerializeField] private HealthData _healthData;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _damageSlider;
    [SerializeField] private HealthTracker _healthTracker;
    [SerializeField] private HealthEffects _healthEffecter;
    
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = _healthData.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healthEffecter.TakeHealthInfo(_currentHealth);
        _healthSlider.value = _currentHealth;

        if (_currentHealth <= 0)
            Die();

        _healthTracker.OnHealthUpdated();
    }

    public void Heal(int healValue)
    {
        _currentHealth += healValue;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _healthData.MaxHealth);
        _healthEffecter.TakeHealthInfo(_currentHealth);
        _healthTracker.OnHealthUpdated();
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}