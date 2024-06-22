using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthUI : MonoBehaviour
{
    [SerializeField] private Slider _healthSmoothSlider;
    [SerializeField] private Health _health;

    private float _changeSpeed = 5f;

    private void OnEnable()
    {
        _health.HealthUpdated += StartChanges;
    }

    private void OnDisable()
    {
        _health.HealthUpdated -= StartChanges;
    }

    public void StartChanges()
    {
        StartCoroutine(UIChangeCoroutine(_healthSmoothSlider));
    }

    private IEnumerator UIChangeCoroutine(Slider slider)
    {
        while (slider.value != _health.Current)
        {
            slider.value = Mathf.MoveTowards(slider.value, _health.Current, _changeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}