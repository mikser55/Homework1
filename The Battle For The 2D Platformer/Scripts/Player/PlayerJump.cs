using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerData _playerData;

    private AnimationController _animator;
    private PlayerInput _inputs;
    private bool _isGrounded;
    private float _coyteTimeCounter;

    private void Awake()
    {
        _inputs = new();
        _inputs.Player.Jump.canceled += context => CancelCoyoteTimer();
        _inputs.Player.Jump.performed += context => Jump();
        _animator = GetComponent<AnimationController>();
    }


    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void Start()
    {
        SetGravityScale(_playerData.GravityScale);
    }

    private void Update()
    {
        TryCoyoteJump();
        _animator.UpdateJump(_isGrounded, _rigidbody.velocity.y);
        AccelerateFall();
    }

    private void FixedUpdate()
    {
        GetGroundInfo();
    }

    private void Jump()
    {
        if (_coyteTimeCounter > 0f)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _playerData.JumpForce);
        }
    }

    private void CancelCoyoteTimer()
    {
        _coyteTimeCounter = 0f;
    }
     
    private void TryCoyoteJump()
    {
        if (_isGrounded)
            _coyteTimeCounter = _playerData.CoyoteTime;
        else
            _coyteTimeCounter -= Time.deltaTime;
    }

    private void AccelerateFall()
    {
        if (_rigidbody.velocity.y < 0)
        {
            SetGravityScale(_playerData.GravityScale * _playerData.GravityMult);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Max(_rigidbody.velocity.y, -_playerData.MaxFallSpeed));
        }
        else
        {
            SetGravityScale(_playerData.GravityScale);
        }
    }

    private void GetGroundInfo()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _playerData.GroundCheckRadius, _groundLayer);
    }

    private void SetGravityScale(float scale)
    {
        _rigidbody.gravityScale = scale;
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }
}