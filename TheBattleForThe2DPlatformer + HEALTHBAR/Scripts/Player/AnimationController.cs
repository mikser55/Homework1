using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerData _playerData;

    public void UpdateMover(float horizontalInput)
    {
        _animator.SetFloat(_playerData.AnimationSpeed, Mathf.Abs(horizontalInput));
    }

    public void UpdateJump(bool isGrounded, float yVelocity)
    {
        _animator.SetBool(_playerData.AnimationIsGrounded, isGrounded);
        _animator.SetFloat(_playerData.AnimationyVelocity, yVelocity);
    }

    public void UpdateAttack(int currentAttack)
    {
        _animator.SetTrigger(_playerData.AnimationAttack +  currentAttack);
    }
}