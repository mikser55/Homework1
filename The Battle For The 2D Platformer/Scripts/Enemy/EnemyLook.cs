using System;
using UnityEngine;

public class EnemyLook : MonoBehaviour
{
    private Transform _player;
    private Vector3 _playerPosition;
    private PlayerDetector _playerDetector;

    private bool _isFlipped;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void OnEnable()
    {
        _playerDetector = GetComponentInChildren<PlayerDetector>();
        _playerDetector.PlayerStayed += FindPlayer;
    }

    private void OnDisable()
    {
        _playerDetector.PlayerStayed -= FindPlayer;
    }

    private void FindPlayer()
    {
        _playerPosition = _player.position;
    }

    public void LookAtPlayer()
    {
        Vector2 scale = transform.localScale;

        if (_playerPosition.x > transform.position.x)
            scale.x = Mathf.Abs(scale.x) * -1 * (_isFlipped ? -1 : 1);
        else
            scale.x = Mathf.Abs(scale.x) * (_isFlipped ? -1 : 1);

        transform.localScale = scale;
    }
}