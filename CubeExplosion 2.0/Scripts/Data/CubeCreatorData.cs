using UnityEngine;

[CreateAssetMenu(fileName = "CubeCreatorData", menuName = "Data/CubeCreatorData")]
public class CubeCreatorData : ScriptableObject
{
    #region CubeAmount
    [field: SerializeField] public int MinCubeAmount { get; private set; } = 2;
    [field: SerializeField] public int MaxCubeAmount { get; private set; } = 7;
    #endregion
    #region Split
    [field: SerializeField] public float SplitCofficient { get; private set; } = 0.5f;
    #endregion
    #region CubeSize
    [field: SerializeField] public float ScaleCofficient { get; private set; } = 0.5f; 
    #endregion
}