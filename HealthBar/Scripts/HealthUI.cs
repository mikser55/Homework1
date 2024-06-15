using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _healthSmoothSlider;
    [SerializeField] private SmoothHealth _smoothHealth;

    private float _changeSpeed = 10f;
    private int _current = 0;

    public void StartChanges()
    {
        _current = _smoothHealth.GetCurrentHealth();
        _healthSlider.value = _current;
        StartCoroutine(HealthChangeCoroutine(_healthSmoothSlider));
    }

    private IEnumerator HealthChangeCoroutine(Slider slider)
    {
        while (slider.value != _current)
        {
            slider.value = Mathf.MoveTowards(slider.value, _current, _changeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
