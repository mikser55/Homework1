using System.Collections.Generic;
using UnityEngine;

public class Patrol : StateMachineBehaviour
{
    [SerializeField] private EnemyData _enemyData;

    private List<Transform> _pointPositions = new();
    private Transform _enemyTransform;
    private PlayerDetector _playerDetector;
    private Rigidbody2D _enemyRigidbody;
    private int _pointIndex = 0;
    private bool _isFlipped;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform parentPoint = GameObject.Find("EnemyPatrolPoints").transform;

        _playerDetector = animator.GetComponentInChildren<PlayerDetector>();
        _enemyRigidbody = animator.GetComponent<Rigidbody2D>();
        _enemyTransform = animator.GetComponent<Transform>();

        _playerDetector.PlayerDetected += DetectPlayer;

        foreach (Transform child in parentPoint)
            _pointPositions.Add(child);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_pointPositions != null)
        {
            Vector2 nextPosition = Vector2.MoveTowards(_enemyRigidbody.position, _pointPositions[_pointIndex].position, 2.5f * Time.fixedDeltaTime);
            _enemyRigidbody.MovePosition(nextPosition);

            if (Vector2.Distance(_enemyRigidbody.position, _pointPositions[_pointIndex].position) <= 0.2f)
                _pointIndex++;

            if (_pointIndex >= _pointPositions.Count)
                _pointIndex = 0;

            Flip();
        }

        animator.SetBool("isDetected", _enemyData.IsDetected);
    }
    private void Flip()
    {
        Vector2 scale = _enemyTransform.localScale;

        if (_pointPositions[_pointIndex].position.x > _enemyTransform.position.x)
            scale.x = Mathf.Abs(scale.x) * -1 * (_isFlipped ? -1 : 1);
        else
            scale.x = Mathf.Abs(scale.x) * (_isFlipped ? -1 : 1);

        _enemyTransform.localScale = scale;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _pointPositions.Clear();
    }

    private void DetectPlayer()
    {
        _enemyData.IsDetected = true;
    }
}