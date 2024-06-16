using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _healthSmoothSlider;
    [SerializeField] private Health _health;
    [SerializeField] private SmoothHealth _smoothHealth;

    private float _changeSpeed = 10f;
    private int _current = 0;

    private void OnEnable()
    {
        _health.HealthUpdated += StartChanges;
        _smoothHealth.HealthUpdated += StartChanges;
    }

    private void OnDisable()
    {
        _smoothHealth.HealthUpdated -= StartChanges;
        _health.HealthUpdated -= StartChanges;
    }

    public void StartChanges()
    {
        _healthSlider.value = _health.GetCurrentHealth();
        StartCoroutine(UIChangeCoroutine(_healthSmoothSlider));
    }

    private IEnumerator UIChangeCoroutine(Slider slider)
    {
        while (slider.value != _current)
        {
            slider.value = Mathf.MoveTowards(slider.value, _smoothHealth.GetCurrentHealth(), _changeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
