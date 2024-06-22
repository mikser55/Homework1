using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EnemyLook))]
public class EnemyChase : StateMachineBehaviour
{
    [SerializeField] private EnemyData _data;

    private Transform _playerPosition;
    private Rigidbody2D _rigidbody;
    private EnemyLook _enemy;
    private PlayerDetector _playerDetector;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerDetector = animator.GetComponentInChildren<PlayerDetector>();
        _playerDetector.PlayerLost += MissPlayer;
        _rigidbody = animator.GetComponentInParent<Rigidbody2D>();
        _enemy = animator.GetComponentInChildren<EnemyLook>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPosition = _playerDetector.PlayerTransform;
        _enemy.LookAtPlayer();

        if (_playerPosition != null && _enemy != null)
        {
            if (Vector2.Distance(_playerPosition.position, _rigidbody.position) <= _data.EnemyAttackRange)
            {
                animator.TryGetComponent(out EnemyWeapon weapon);
                weapon.Attack();
                animator.SetTrigger("Attack");
            }

            Vector2 target = new(_playerPosition.position.x, _rigidbody.position.y);
            Vector2 newPosition = Vector2.MoveTowards(_rigidbody.position, target, _data.EnemySpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPosition);
        }

        animator.SetBool("isDetected", _data.IsDetected);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    private void MissPlayer()
    {
        _data.IsDetected = false;
    }
}