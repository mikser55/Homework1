using UnityEngine;

public class PlayerBullet : Bullet<PlayerBullet>
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        _pool.ReturnObject(this);
    }

    protected override void Move()
    {
        transform.Translate(transform.right * _speed * Time.deltaTime);
    }

    protected override void CheckOnScreen()
    {
        if (_screenPosition.x > 1 + padding)
            _pool.ReturnObject(this);
    }
}