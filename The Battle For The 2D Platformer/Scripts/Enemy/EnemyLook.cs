using UnityEngine;

public class EnemyLook : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private bool _isFlipped;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMover>().transform;
    }

    public void LookAtPlayer()
    {
        Vector2 scale = transform.localScale;

        if (_player.transform.position.x > transform.position.x)
            scale.x = Mathf.Abs(scale.x) * -1 * (_isFlipped ? -1 : 1);
        else
            scale.x = Mathf.Abs(scale.x) * (_isFlipped ? -1 : 1);

        transform.localScale = scale;
    }
}