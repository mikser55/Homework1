using System.Collections;
using UnityEngine;

public class LifeDrainAbility : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _radius = 3f;
    [SerializeField] private float _delay = 1f;
    [SerializeField] private float _maxDuration = 6f;
    [SerializeField] private Health _playerHealth;

    private PlayerInput _inputs;
    private float _currentDuration;
    private EnemyWeapon _closestEnemy;
    private bool _isDrain;

    private void Awake()
    {
        _inputs = new();
        _inputs.Player.LifeDrain.performed += context => ActivateSpell();
    }


    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    private void ActivateSpell()
    {
        if (!_isDrain)
        {
            _currentDuration = _maxDuration;
            StartCoroutine(DoAbilityCoroutine());
            _isDrain = true;
        }
    }

    private IEnumerator DoAbilityCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_currentDuration > 0)
        {
            _currentDuration--;
            _closestEnemy = FindClosestEnemy();

            if (_closestEnemy != null)
            {
                _closestEnemy.TryGetComponent(out Health health);
                health?.TakeDamage(_damage);
                _playerHealth.TakeHeal(_damage);
            }

            if (_currentDuration == 0)
                _isDrain = false;

            yield return wait;
        }
    }

    private EnemyWeapon FindClosestEnemy()
    {
        float shortestDistance = Mathf.Infinity;
        float distanceToEnemy;
        EnemyWeapon closestEnemy = null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radius);

        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                collider.TryGetComponent(out EnemyWeapon enemy);

                if (enemy != null)
                {
                    distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                    if (distanceToEnemy <= shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        closestEnemy = enemy;
                    }
                }
            }
        }

        return closestEnemy;
    }
}