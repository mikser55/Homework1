using UnityEngine;

public interface IEnemyMoveable
{
    public Rigidbody2D RigidBody { get; set; }
    public bool IsFacingRight { get; set; }

    public void Move(Vector2 velocity) { }

    public void ChangeFacing(Vector2 velocity) { }
}