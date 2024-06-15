using UnityEngine;

public class HealthChanger : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;

    private int _damage = 10;
    private int _healValue = 10;

    public void DoDamage()
    {
        if (_playerHealth != null)
            _playerHealth.TakeDamage(_damage);
    }

    public void DoHeal()
    {
        if (_playerHealth != null)
            _playerHealth.Heal(_healValue);
    }
}