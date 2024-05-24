using UnityEngine;

[CreateAssetMenu(fileName = "ExplosionData", menuName = "Data/ExplosionData")]
public class ExplosionData : ScriptableObject
{
    #region ExplosionInfo
    [field: SerializeField] public float ExplosionRadius { get; private set; } = 10f;
    [field: SerializeField] public float ExplosionForce { get; private set; } = 100f; 
    #endregion
}
