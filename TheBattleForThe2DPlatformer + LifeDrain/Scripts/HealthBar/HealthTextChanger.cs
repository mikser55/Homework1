using TMPro;
using UnityEngine;

public class HealthTextChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private HealthData _healthData;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.HealthUpdated += ChangeHealthText;
    }

    private void OnDisable()
    {
        _health.HealthUpdated -= ChangeHealthText;
    }

    private void Start()
    {
        ChangeHealthText();
    }

    private void ChangeHealthText()
    {
        _healthText.text = $"Health: {_health.Current}/{_healthData.MaxHealth}";
    }
}
