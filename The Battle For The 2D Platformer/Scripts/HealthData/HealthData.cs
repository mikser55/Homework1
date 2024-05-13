using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "Data/HealthData")]
public class HealthData : ScriptableObject
{
    [field: SerializeField] public int MaxHealth { get; private set; } = 100; 
}
