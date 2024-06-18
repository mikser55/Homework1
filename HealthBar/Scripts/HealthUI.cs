using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Health _health;

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
        _healthSlider.value = _health.Current;
    }
}