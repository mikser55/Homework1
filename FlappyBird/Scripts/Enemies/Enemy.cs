using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(EnemyWeapon))]
public class Enemy : MonoBehaviour
{
    private EnemyWeapon _weapon;
    private Camera _camera;
    private Vector3 _startPosition;

    private float _speed = 5f;
    private float _randomValue;
    private float _minSpawnPosition = 0.2f;
    private float _maxSpawnPosition = 0.8f;
    private float _xSpawnPostion = 1.1f;
    private float _stopPositionX;
    private float _screenThirdPart = 0.67f;

    private bool _isMoving = true;

    public event Action Destroyed;

    private void Awake()
    {
        _weapon = GetComponent<EnemyWeapon>();
    }

    private void Start()
    {
        _randomValue = Random.Range(_minSpawnPosition, _maxSpawnPosition);
        _camera = Camera.main;
        _startPosition = _camera.ViewportToWorldPoint(new Vector3(_xSpawnPostion, _randomValue, 0f));
        _startPosition.z = 0f;
        transform.position = _startPosition;
        _stopPositionX = _camera.ViewportToWorldPoint(new Vector3(_screenThirdPart, 0, 0)).x;
    }

    private void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerBullet bullet))
        {
            Destroyed?.Invoke();
            Destroy(this.gameObject);
        }
    }

    private void Move()
    {
        if (_isMoving)
            transform.Translate(Vector3.left * _speed * Time.deltaTime);

        if (transform.position.x <= _stopPositionX)
        {
            _isMoving = false;
            transform.position = new Vector3(_stopPositionX, transform.position.y, transform.position.z);
            _weapon.Shoot();
        }
    }
}