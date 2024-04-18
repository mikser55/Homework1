using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody;

    private float _horizontalInput;

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
        _horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void ChangeMovementDirection()
    {
        if (_horizontalInput > 0)
            _spriteRenderer.flipX = false;
        else if (_horizontalInput < 0)
            _spriteRenderer.flipX = true;
    }

    private void UpdateAnimations()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_horizontalInput));
    }
}