using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void UpdateMover(float _horizontalInput)
    {
        _animator.SetFloat("Speed", Mathf.Abs(_horizontalInput));
    }

    public void UpdateJump(bool isGrounded, float yVelocity, int jumpsLeft)
    {
        _animator.SetBool("IsGrounded", isGrounded);
        _animator.SetFloat("yVelocity", yVelocity);
        _animator.SetInteger("amountOfJumpsLeft", jumpsLeft);
    }
}