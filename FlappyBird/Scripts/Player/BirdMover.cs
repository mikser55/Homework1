using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdMover : MonoBehaviour
{
    [SerializeField] private float _tapForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private Vector3 _startPosition;
    private Rigidbody2D _rigidbody;
    private Quaternion _minRotation;
    private Quaternion _maxRotation;
    private PlayerInputs _inputs;

    private void Awake()
    {
        _inputs = new();
        _inputs.Player.Tap.performed += context => Move();
        _startPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();

        _minRotation = Quaternion.Euler(0f, 0f, _minRotationZ);
        _maxRotation = Quaternion.Euler(0f, 0f, _maxRotationZ);
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
        Fall();
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_speed, _tapForce);
        transform.rotation = _maxRotation;
    }

    private void Fall()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }
}