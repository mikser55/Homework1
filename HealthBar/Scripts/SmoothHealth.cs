using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SmoothHealth : MonoBehaviour
    {
        [SerializeField] private HealthData _data;

        private float _current;
        private float _targetValue;
        private float _changeSpeed = 10f;

        public event Action HealthUpdated;

        private void Start()
        {
            _current = _data.MaxHealth;
            _targetValue = _current;
        }

        public void TakeDamage(float damage)
        {
            StartCoroutine(DecreaseHealthCoroutine(damage));

            if (_current <= 0)
                Die();

            OnHealthUpdated();
        }

        public void GetHeal(float healValue)
        {
            StartCoroutine(IncreaseHealthCoroutine(healValue));
            _current = Mathf.Clamp(_current, 0, _data.MaxHealth);
            OnHealthUpdated();
        }

        public float GetCurrentHealth()
        {
            return _current;
        }

        public void OnHealthUpdated()
        {
            HealthUpdated?.Invoke();
        }

        private IEnumerator IncreaseHealthCoroutine(float value)
        {
            _targetValue += value;
            _targetValue = Mathf.Clamp(_targetValue, 0, _data.MaxHealth);

            while (_current != _targetValue)
            {
                _current = Mathf.MoveTowards(_current, _targetValue, _changeSpeed * Time.deltaTime);
                yield return null;
            }
        }

        private IEnumerator DecreaseHealthCoroutine(float value)
        {
            _targetValue -= value;
            _targetValue = Mathf.Clamp(_targetValue, 0, _data.MaxHealth);

            while (_current != _targetValue)
            {
                _current = Mathf.MoveTowards(_current, _targetValue, _changeSpeed * Time.deltaTime);
                yield return null;
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}