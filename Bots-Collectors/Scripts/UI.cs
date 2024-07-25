using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resourcesAmount;
    [SerializeField] private Base _base;

    private string _resourceText = "Amount of resources: ";

    private void OnEnable()
    {
        _base.ResourceAmountChanged += ChangeResourcesAmount;
    }

    private void OnDisable()
    {
        _base.ResourceAmountChanged -= ChangeResourcesAmount;
    }

    private void Start()
    {
        ChangeResourcesAmount();
    }

    private void ChangeResourcesAmount()
    {
        _resourcesAmount.text = _resourceText + _base.ResourcesAmount.ToString();
    }
}