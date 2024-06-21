using UnityEngine;

public class EnemyLook : MonoBehaviour
{
    [SerializeField] private EnemyData _data;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private PlayerDetector _playerDetector;

    private Transform _player;
    private Vector3 _playerPosition;
    private bool _isFacingRight;

    private void OnEnable()
    {
        _playerDetector.PlayerStayed += FindPlayer;
    }

    private void OnDisable()
    {
        _playerDetector.PlayerStayed -= FindPlayer;
    }

    private void FindPlayer()
    {
        _player = _playerDetector.PlayerTransform;

        if (_player != null)
            _playerPosition = _player.position;
    }

    public void LookAtPlayer()
    {
        Vector2 scale = transform.localScale;

        if (_playerPosition != null)
        {
            if (_playerPosition.x > _parentTransform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * (_data.IsFacingRight ? -1 : 1);
                _data.IsFacingRight = true;
            }
            else
            {
                scale.x = Mathf.Abs(scale.x) * (_data.IsFacingRight ? -1 : 1);
                _data.IsFacingRight = false;
            }

            transform.localScale = scale;
        }
    }
}