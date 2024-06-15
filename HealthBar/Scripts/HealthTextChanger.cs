using TMPro;
using UnityEngine;

public class HealthTextChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private HealthData _healthData;
    [SerializeField] private Health _health;
    [SerializeField] private HealthTracker _healthTracker;

    private void OnEnable()
    {
        _healthTracker.HealthUpdated += ChangeHealthText;
    }

    private void OnDisable()
    {
        _healthTracker.HealthUpdated -= ChangeHealthText;
    }

    private void Start()
    {
        ChangeHealthText();
    }

    private void ChangeHealthText()
    {
        _healthText.text = $"Health: {_health.GetCurrentHealth()}/{_healthData.MaxHealth}";
    }
}
