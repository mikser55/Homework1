using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealeable
{
    [SerializeField] private HealthData _data;

    public float Current { get; private set; }

    public event Action HealthUpdated;

    private void Awake()
    {
        Current = _data.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            Current -= damage;
            Current = Mathf.Clamp(Current, 0, _data.MaxHealth);

            if (Current == 0)
                Die();

            OnHealthUpdated();
        }
    }

    public void TakeHeal(float healValue)
    {
        if (healValue > 0)
        {
            Current += healValue;
            Current = Mathf.Clamp(Current, 0, _data.MaxHealth);
            OnHealthUpdated(); 
        }
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