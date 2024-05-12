using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    #region AnimationController
    public string AnimationSpeed { get; private set; } = "Speed";
    public string AnimationIsGrounded { get; private set; } = "IsGrounded";
    public string AnimationyVelocity { get; private set; } = "yVelocity";
    public string AnimationJumpsLeft { get; private set; } = "amountOfJumpsLeft";
    public string AnimationAttack { get; private set; } = "Attack";
    #endregion

    #region PlayerMover
    [field: SerializeField] public float PlayerSpeed { get; private set; } = 8f;
    #endregion

    #region PlayerJump
    [field: SerializeField] public float JumpForce { get; private set; } = 8f;
    [field: SerializeField] public float GroundCheckRadius { get; private set; } = 0.3f;
    [field: SerializeField] public float CoyoteTime { get; private set; } = 0.2f;
    #endregion

    #region PlayerAttack
    [field: SerializeField] public float TimeToReset { get; private set; } = 1f;
    [field: SerializeField] public float TimeToCombo { get; private set; } = 0.25f;
    [field: SerializeField] public float AttackCooldown { get; private set; } = 0.2f;
    [field: SerializeField] public int AttackDamage { get; private set; } = 30;

    public int MaxComboAttacks { get; private set; } = 3;
    #endregion

    #region PlayerFall
    [field: SerializeField] public float GravityScale { get; private set; } = 1f;
    [field: SerializeField] public float GravityMult { get; private set; } = 2f;
    [field: SerializeField] public float MaxFallSpeed { get; private set; } = 10f;
    #endregion

    #region PlayerHealth
    [field: SerializeField] public int MaxHealth { get; private set; } = 100;
    #endregion
}