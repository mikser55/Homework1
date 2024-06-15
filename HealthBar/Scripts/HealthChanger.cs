using Assets.Scripts;
using UnityEngine;

public class HealthChanger : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private SmoothHealth _playerSmoothHealth;

    private int _damage = 10;
    private int _healValue = 10;

    public void DoDamage()
    {
        if (_playerHealth != null && _playerSmoothHealth != null)
        {
            _playerHealth.TakeDamage(_damage);
            _playerSmoothHealth.TakeDamage(_damage);
        }
    }

    public void DoHeal()
    {
        if (_playerHealth != null && _playerSmoothHealth != null)
        {
            _playerHealth.GetHeal(_healValue);
            _playerSmoothHealth.GetHeal(_healValue);
        }
    }
}