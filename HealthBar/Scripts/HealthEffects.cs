using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HealthEffects : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private HealthTracker _healthTracker;
        [SerializeField] private Slider _damageSlider;
        [SerializeField] private Slider _healthSlider;

        private int _currentHealth = 0;
        private float _changeSpeed = 10f;

        private void OnEnable()
        {
            _healthTracker.HealthUpdated += DoHealthEffect;
        }

        private void OnDisable()
        {
            _healthTracker.HealthUpdated -= DoHealthEffect;
        }

        public void TakeHealthInfo(int health)
        {
            _currentHealth = health;
        }

        private void DoHealthEffect()
        {
            _healthSlider.value = _currentHealth;
            StartCoroutine(EffectCoroutine(_damageSlider));
        }

        private IEnumerator EffectCoroutine(Slider slider)
        {
            while (slider.value > _currentHealth)
            {
                slider.value = Mathf.MoveTowards(slider.value, _currentHealth, _changeSpeed * Time.deltaTime);
                yield return null;
            }

            _damageSlider.value = _healthSlider.value;
        }
    }
}