using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseInteractor : MonoBehaviour
{
    [SerializeField] private Color _selectColor;

    private Color _originalColor = Color.blue;
    private Camera _camera;
    private Vector2 _mousePosition;
    private EventSystem _eventSystem;
    private bool _isOnGUI;

    public event Action<Base, Color, bool> BaseClicked;

    private void Awake()
    {
        PlayerInput.Get.Player.LeftMouseButton.canceled += context => OnBaseClick();
    }

    private void OnEnable()
    {
        PlayerInput.Get.Enable();
    }

    private void OnDisable()
    {
        PlayerInput.Get.Disable();
    }

    private void Start()
    {
        _eventSystem = EventSystem.current;
        _camera = Camera.main;
    }

    private void Update()
    {
        _isOnGUI = _eventSystem.IsPointerOverGameObject();
        _mousePosition = PlayerInput.Get.Player.MousePosition.ReadValue<Vector2>();
    }

    private void OnBaseClick()
    {
        if (!_isOnGUI)
        {
            Ray ray = _camera.ScreenPointToRay(_mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out Base _base))
                {
                    if (_base.TryGetComponent(out Renderer renderer))
                    {
                        if (renderer.material.color == _base.OriginalColor)
                        {
                            BaseClicked?.Invoke(_base, _selectColor, true);
                        }
                    }
                }
                else
                {
                    BaseClicked?.Invoke(_base, _originalColor, false);
                }
            }
        }
    }
}