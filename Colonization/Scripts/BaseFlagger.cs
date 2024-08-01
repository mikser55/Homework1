using System;
using UnityEngine;

public class BaseFlagger : MonoBehaviour
{
    private const int MinBotsOnBase = 1;

    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private int _resourcesToBuildNewBase = 5;

    private Vector2 _mousePosition;
    private Camera _camera;
    private Flag _currentFlag;

    public event Action<Transform> CanBuildNewBase;

    private void Awake()
    {
        PlayerInput.Get.Player.RightMouseButton.performed += context => PutFlag();
        _camera = Camera.main;
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
        _currentFlag = Instantiate(_flagPrefab, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        _mousePosition = PlayerInput.Get.Player.MousePosition.ReadValue<Vector2>();
    }

    private void PutFlag()
    {
        TryGetComponent(out Base _base);

        if (_base.ResourcesAmount >= _resourcesToBuildNewBase && _base.GetBotsCount() > MinBotsOnBase)
        {
            Ray ray = _camera.ScreenPointToRay(_mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out Ground _) && _base.IsSelected)
                {
                    if (_currentFlag != null)
                    {
                        Destroy(_currentFlag.gameObject);
                    }

                    _currentFlag = Instantiate(_flagPrefab, hit.point, Quaternion.identity);
                    CanBuildNewBase?.Invoke(_currentFlag.transform);
                }
            }
        }
    }
}