using UnityEngine;

[CreateAssetMenu(fileName = "CubeSpawnerData", menuName = "Data/CubeSpawnerData", order = 51)]
public class CubeSpawnerData : ScriptableObject
{
    #region Spawn
    [field: SerializeField] public float SpawnTime { get; private set; } = 1f;
    #endregion
    #region Position
    [field: SerializeField] public float YSpawnPosition { get; private set; } = 15f; 
    #endregion

    public int HalfSizeCofficient { get; private set; } = 2;
    public bool IsWork { get; private set; } = true;
}