using UnityEngine;

public class Patrolling : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform[] _positions;

    private int _startPosition = 0;
    private int _currentPosition = 0;

    private void Start()
    {
        _spriteRenderer.flipX = true;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _positions[_currentPosition].position, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _positions[_currentPosition].position) < 0.1f)
        {
            Flip();

            if (_currentPosition < _positions.Length - 1)
                _currentPosition++;
            else
            _currentPosition = _startPosition;
        }
    }

    private void Flip()
    {
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }
}