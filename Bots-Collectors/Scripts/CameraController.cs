using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private const int MiddleMouseButton = 2;

    [SerializeField] private float _moveTime;
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _shiftSpeed;

    private Camera _camera;
    private PlayerInputs _inputs;

    private float _speed;
    private float _shift;
    private float _middleButtonValue;

    private Vector3 _newPosition;
    private Vector3 _newInputs;
    private Vector3 _mousePosition;
    private Vector3 _dragStartPosition;
    private Vector3 _dragCurrentPosition;

    private Plane _plane;

    private void Awake()
    {
        _inputs = new();
        _inputs.Player.CamMove.performed += context => SetStartMousePosition();
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    private void Start()
    {
        _camera = Camera.main;
        _newPosition = transform.position;
        _plane = new(Vector3.up, Vector3.zero);
    }

    private void Update()
    {
        TakeInputs();
        Move();
        OnMiddleButtonHold();
    }

    private void TakeInputs()
    {
        _shift = _inputs.Player.Shift.ReadValue<float>();
        _newInputs = _inputs.Player.Move.ReadValue<Vector3>();
        _mousePosition = _inputs.Player.MousePosition.ReadValue<Vector2>();
        _middleButtonValue = _inputs.Player.CamMove.ReadValue<float>();
    }

    private void Move()
    {
        if (_shift > 0)
            _speed = _shiftSpeed;
        else
            _speed = _normalSpeed;

        _newPosition += (_newInputs * _speed);
        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * _moveTime);
    }

    private void SetStartMousePosition()
    {
        Ray ray = _camera.ScreenPointToRay(_mousePosition);
        Mouse mouse = Mouse.current;

        if (mouse.middleButton.wasPressedThisFrame)
            if (_plane.Raycast(ray, out float entry))
                _dragStartPosition = ray.GetPoint(entry);
    }

    private void OnMiddleButtonHold()
    {
        Ray ray = _camera.ScreenPointToRay(_mousePosition);

        if (_middleButtonValue > 0)
        {
            if (_plane.Raycast(ray, out float entry))
            {
                _dragCurrentPosition = ray.GetPoint(entry);

                _newPosition = transform.position + _dragStartPosition - _dragCurrentPosition;
            }
        }
    }
}