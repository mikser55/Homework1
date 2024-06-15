using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class SmoothHealth : MonoBehaviour
    {
        [SerializeField] private HealthData _data;
        [SerializeField] private HealthUI _healthUI;

        private event Action _healthUpdated;
        private int _current;

        private void OnEnable()
        {
            _healthUpdated += _healthUI.StartChanges;
        }

        private void OnDisable()
        {
            _healthUpdated -= _healthUI.StartChanges;
        }

        private void Start()
        {
            _current = _data.MaxHealth;
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

        public void OnHealthUpdated()
        {
            _healthUpdated?.Invoke();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}