using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private int _amountOfJumps = 2;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;

    private PlayerInput _playerInput;
    private bool _canJump;
    private int _amountOfJumpsLeft;
    private bool _isGrounded;

    private void Start()
    {
        _amountOfJumpsLeft = _amountOfJumps;
        _playerInput = new();
    }

    void Update()
    {
        GetInput();
        GetJumpsInfo();
        UpdateAnimations();
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

    private void UpdateAnimations()
    {
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("yVelocity", _rigidbody.velocity.y);
        _animator.SetInteger("amountOfJumpsLeft", _amountOfJumpsLeft);
    }
}