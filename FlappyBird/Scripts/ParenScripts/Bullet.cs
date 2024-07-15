using UnityEngine;

public abstract class Bullet<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected float _speed;

    protected Camera _camera;
    protected Vector3 _screenPosition;
    protected PoolManager<T> _pool;
    protected float padding = 0.1f;

    protected void Start()
    {
        _camera = Camera.main;
    }

    protected void Update()
    {
        _screenPosition = _camera.WorldToViewportPoint(transform.position);
        Move();
        CheckOnScreen();
    }

    protected abstract void OnCollisionEnter2D(Collision2D collision);

    public void Initialize(PoolManager<T> pool)
    {
        _pool = pool;
    }

    protected abstract void Move();

    protected abstract void CheckOnScreen();
}