using UnityEngine;

[CreateAssetMenu(fileName = "CoinData", menuName = "Data/CoinData")]
public class CoinData : ScriptableObject
{
    #region CoinSpawn
    [field: SerializeField] public float CoinSpawnTime { get; private set; } = 5f;
    #endregion

    #region Coin
    [field: SerializeField] public int CoinRotateSpeed { get; private set; } = 180; 
    #endregion
}