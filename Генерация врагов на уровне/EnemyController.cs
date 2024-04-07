using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private Vector3 _direction;

    private void Update()
    {
            transform.position += _direction * _speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}