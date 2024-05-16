using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EnemyLook))]
public class EnemyChase : StateMachineBehaviour
{
    [SerializeField] private EnemyData _enemyData;

    private Transform _playerPosition;
    private Rigidbody2D _rigidbody;
    private EnemyLook _enemy;
    private PlayerDetector _playerDetector;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerDetector = animator.GetComponentInChildren<PlayerDetector>();
        _playerDetector.PlayerLost += MissPlayer;
        _playerPosition = GameObject.FindGameObjectWithTag("Player")?.transform;
        _rigidbody = animator.GetComponent<Rigidbody2D>();
        _enemy = animator.GetComponent<EnemyLook>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playerPosition != null)
        {
            _enemy.LookAtPlayer();

            Vector2 target = new(_playerPosition.position.x, _rigidbody.position.y);
            Vector2 newPosition = Vector2.MoveTowards(_rigidbody.position, target, _enemyData.EnemySpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPosition);

            if (Vector2.Distance(_playerPosition.position, _rigidbody.position) <= _enemyData.EnemyAttackRange)
            {
                animator.SetTrigger("Attack");
            }
        }

        animator.SetBool("isDetected", _enemyData.IsDetected);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    private void MissPlayer()
    {
        _enemyData.IsDetected = false;
    }
}