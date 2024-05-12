using UnityEngine;

[CreateAssetMenu(fileName = "ObjectsData", menuName = "Data/ObjectsData")]
public class ObjectsData : ScriptableObject
{
	#region Kit
	[field: SerializeField] public int _kitHealNumber { get; private set; } = 20;
    #endregion

    #region KitSpawn
    [field: SerializeField] public float KitSpawnTime { get; private set; } = 5f;

    public float XMinPosition { get; private set; } = -16f;
    public float XMaxPosition { get; private set; } = 8f;
    public float YPosition { get; private set; } = -4.5f;
    #endregion

    #region CoinSpawn
    [field: SerializeField] public float CoinSpawnTime { get; private set; } = 5f; 
    #endregion
}
