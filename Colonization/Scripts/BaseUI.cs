using TMPro;
using UnityEngine;

[RequireComponent(typeof(Base))]
public class BaseUI : MonoBehaviour
{
    private TextMeshProUGUI _resourcesAmount;
    private Base _base;
    private Camera _camera;
    private float _yTextPosition = 20;
    private Vector3 _offset;

    private string _resourceText = "Amount of resources: ";

    private void Awake()
    {
        _offset = new Vector3(0, _yTextPosition, 0);
        _camera = Camera.main;
        _base = GetComponent<Base>();
        _resourcesAmount = GetComponentInChildren<TextMeshProUGUI>();
    }
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

    private void Update()
    {
        SetTextPosition();
    }

    private void ChangeResourcesAmount()
    {
        if(_resourcesAmount != null)
        _resourcesAmount.text = _resourceText + _base.ResourcesAmount.ToString();
    }

    private void SetTextPosition()
    {
        if (_resourcesAmount != null)
            _resourcesAmount.transform.position = _camera.WorldToScreenPoint(transform.position + _offset);
    }
}