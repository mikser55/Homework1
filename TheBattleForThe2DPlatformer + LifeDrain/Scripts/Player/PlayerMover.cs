using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerSprite _sprite;

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

    private void OnDisable()
    {
        _inputs.Disable();
    }

    private void Update()
    {
        TakeHorizontalInput();
        _animator.UpdateMover(_horizontalInput);
    }

    private void FixedUpdate()
    {
        Walk();
    }

    public float GetHorizontalInput()
    {
        return _horizontalInput;
    }

    private void TakeHorizontalInput()
    {
        _horizontalInput = _inputs.Player.Move.ReadValue<Vector2>().x;
    }

    private void Walk()
    {
        _rigidbody.velocity = new Vector2(_horizontalInput * _playerData.PlayerSpeed, _rigidbody.velocity.y);
    }
}