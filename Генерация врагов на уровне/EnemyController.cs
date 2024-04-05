using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    private Vector3 _direction;

    private void FixedUpdate()
    {
            transform.position += _direction * _speed * Time.fixedDeltaTime;
    }

    public void SetPosition(Vector3 direction)
    {
        _direction = direction;
    }
}