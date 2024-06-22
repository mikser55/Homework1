using UnityEngine;

public class HealthChanger : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;

    private float _damage = 10;
    private float _healValue = 10;

    public void DoDamage()
    {
        if (_playerHealth != null)
        {
            _playerHealth.TakeDamage(_damage);
        }
    }

    public void DoHeal()
    {
        if (_playerHealth != null)
        {
            _playerHealth.TakeHeal(_healValue);
        }
    }
}