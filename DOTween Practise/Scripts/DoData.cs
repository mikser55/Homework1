using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "newDoData", menuName = "Data/ObjectsData", order = 52)]
public class DoData : ScriptableObject
{
    [field: SerializeField] public LoopType LoopType {  get; private set; }
    [field: SerializeField] public float Delay { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }
}