using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Transform _attackArea;

    private int _currentAttack = 0;
    private float _timeSinceAttack = 0f;
    private AnimationController _animator;
    private PlayerInput _inputs;

    private void Awake()
    {
        _animator = GetComponent<AnimationController>();
        _inputs = new();
        _inputs.Player.Attack.performed += context => Attack();
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void Update()
    {
        _timeSinceAttack += Time.deltaTime;
    }

    private void Attack()
    {
        if (_inputs.Player.Attack.IsPressed() && _timeSinceAttack > _playerData.TimeToCombo)
        {
            _attackArea.gameObject.SetActive(true);
            _currentAttack++;

            if (_currentAttack > _playerData.MaxComboAttacks)
                _currentAttack = 1;

            if (_timeSinceAttack > _playerData.TimeToReset)
                _currentAttack = 1;

            _animator.UpdateAttack(_currentAttack);

            _timeSinceAttack = 0f;
            StartCoroutine(CooldownCoroutine());
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_playerData.AttackCooldown);

        yield return wait;
        _attackArea.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }
}