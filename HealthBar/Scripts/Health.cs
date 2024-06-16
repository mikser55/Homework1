using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealeable
{
    [SerializeField] private HealthData _data;

    private float _current;

    public event Action HealthUpdated;

    private void Awake()
    {
        _current = _data.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        _current -= damage;

        if (_current <= 0)
            Die();

        OnHealthUpdated();
    }

    public void GetHeal(float healValue)
    {
        _current += healValue;
        _current = Mathf.Clamp(_current, 0, _data.MaxHealth);
        OnHealthUpdated();
    }

    public float GetCurrentHealth()
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