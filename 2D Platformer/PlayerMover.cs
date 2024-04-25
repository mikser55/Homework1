using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;

    private PlayerInput _playerInput;
    private float _horizontalInput;

    private void Start()
    {
        _playerInput = new();
    }

    private void Update()
    {
        GetInput();
        ChangeMovementDirection();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private void Walk()
    {
        _rigidbody.velocity = new Vector2(_horizontalInput * _speed, _rigidbody.velocity.y);
    }

    private void GetInput()
    {
        _horizontalInput = _playerInput.GetHorizontalInput();
    }

    private void ChangeMovementDirection()
    {
        if (_horizontalInput > 0)
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else if (_horizontalInput < 0)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

    private void UpdateAnimations()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_horizontalInput));
    }
}