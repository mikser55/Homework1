using UnityEngine;

public class EnemyBullet : Bullet<EnemyBullet>
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent(out Bird bird);

        if (bird != null)
            bird.Die();

        _pool.ReturnObject(this);
    }

    protected override void Move()
    {
        transform.Translate(transform.right * -_speed * Time.deltaTime);
    }

    protected override void CheckOnScreen()
    {
        if (_screenPosition.x < 0 - padding)
            _pool.ReturnObject(this);
    }
}