using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private int _amountOfJumps = 2;
    [SerializeField] private Rigidbody2D _rigidbody;

    private AnimationController _animator;
    private PlayerInput _playerInput;
    private bool _canJump;
    private int _amountOfJumpsLeft;
    private bool _isGrounded;

    private void Start()
    {
        _animator = GetComponent<AnimationController>();
        _playerInput = new();
        _amountOfJumpsLeft = _amountOfJumps;
    }

    void Update()
    {
        GetInput();
        GetJumpsInfo();
        _animator.UpdateJump(_isGrounded, _rigidbody.velocity.y, _amountOfJumpsLeft);
    }

    private void FixedUpdate()
    {
        GetGroundInfo();
    }

    private void GetInput()
    {
        if (_playerInput.GetJumpInput())
            Jump();
    }

    private void Jump()
    {
        if (_canJump)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            _amountOfJumpsLeft--;
        }
    }

    private void GetGroundInfo()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }

    private void GetJumpsInfo()
    {
        if (_isGrounded && _rigidbody.velocity.y <= 0)
            _amountOfJumpsLeft = _amountOfJumps;

        if (_amountOfJumpsLeft <= 0)
            _canJump = false;
        else
            _canJump = true;

        if (_isGrounded == false && _amountOfJumpsLeft == _amountOfJumps)
            _amountOfJumpsLeft = 1;
    }
}