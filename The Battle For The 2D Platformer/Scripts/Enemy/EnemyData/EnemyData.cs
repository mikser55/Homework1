using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
	#region State
	public bool IsDetected { get; set; }
	#endregion

	#region EnemyStats
	[field: SerializeField] public int EnemyMaxHealth { get; private set; } = 100;
	[field: SerializeField] public float EnemySpeed { get; private set; } = 2.5f;
	#endregion

	#region EnemyAttack
	[field: SerializeField] public int EnemyDamage { get; private set; } = 20;
	[field: SerializeField] public float EnemyAttackRange { get; private set; } = 2f;
	#endregion
}