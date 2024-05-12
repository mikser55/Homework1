using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerData _playerData;

    private AnimationController _animator;
    private PlayerInput _inputs;
    private float _horizontalInput;

    private void Awake()
    {
        _inputs = new();
        _animator = GetComponent<AnimationController>();
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void Update()
    {
        GetHorizontalInput();
        ChangeMovementDirection();
        _animator.UpdateMover(_horizontalInput);
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private void GetHorizontalInput()
    {
        _horizontalInput = _inputs.Player.Move.ReadValue<Vector2>().x;
    }

    private void Walk()
    {
            _rigidbody.velocity = new Vector2(_horizontalInput * _playerData.PlayerSpeed, _rigidbody.velocity.y);
    }

    private void ChangeMovementDirection()
    {
        if (_horizontalInput > 0)
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else if (_horizontalInput < 0)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }
}