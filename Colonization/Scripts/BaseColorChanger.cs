using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Renderer))]
public class BaseColorChanger : MonoBehaviour
{
    [SerializeField] private Canvas _buildMenu;

    private BaseInteractor _interactor;
    private Renderer _renderer;
    private Color _originalColor = Color.blue;

    public event Action Selected;
    public event Action Unselected;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _originalColor;
    }

    private void OnEnable()
    {
        _interactor.BaseClicked += ChangeBaseSelect;
    }

    private void OnDisable()
    {
        _interactor.BaseClicked -= ChangeBaseSelect;
    }

    [Inject]
    private void Constructor(BaseInteractor interactor)
    {
        _interactor = interactor;
    }

    private void ChangeBaseSelect(Base selectedBase, Color color, bool isActive)
    {
        TryGetComponent(out Base _base);

        if (selectedBase == _base)
        {
            _renderer.material.color = color;
            _buildMenu.gameObject.SetActive(isActive);
            Selected?.Invoke();
        }
        else
        {
            ResetSelect();
        }
    }

    private void ResetSelect()
    {
        _renderer.material.color = _originalColor;
        _buildMenu.gameObject.SetActive(false);
        Unselected?.Invoke();
    }
}