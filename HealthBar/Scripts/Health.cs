using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealeable
{
    [SerializeField] private HealthData _data;
    [SerializeField] private HealthUI _healthUI;

    public event Action HealthUpdated;
    private int _current;

    private void Awake()
    {
        _current = _data.MaxHealth;
    }

    private void OnEnable()
    {
        HealthUpdated += _healthUI.StartChanges;
    }

    private void OnDisable()
    {
        HealthUpdated -= _healthUI.StartChanges;
    }

    public void TakeDamage(int damage)
    {
        _current -= damage;

        if (_current <= 0)
            Die();

        OnHealthUpdated();
    }

    public void GetHeal(int healValue)
    {
        _current += healValue;
        _current = Mathf.Clamp(_current, 0, _data.MaxHealth);
        OnHealthUpdated();
    }

    public int GetCurrentHealth()
    {
        return _current;
    }

    private void OnHealthUpdated()
    {
        HealthUpdated?.Invoke();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}