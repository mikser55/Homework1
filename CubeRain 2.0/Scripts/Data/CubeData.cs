using UnityEngine;

[CreateAssetMenu(fileName = "CubeData", menuName = "Data/CubeData", order = 51)]
public class CubeData : ScriptableObject
{
    [field: SerializeField] public int MinCubeLiveTime { get; private set; } = 2; 
    [field: SerializeField] public int MaxCubeLiveTime { get; private set; } = 5; 
}
