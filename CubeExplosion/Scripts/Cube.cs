using UnityEngine;

[RequireComponent(typeof(CubeExplosion), typeof(CubeCreator))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeCreatorData _cubeCreatorData;
    private CubeCreator _cubeCreator;
    public float SplitChance { get; private set; } = 1f;

    private void Awake()
    {
        _cubeCreator = GetComponent<CubeCreator>();
    }

    private void OnMouseUpAsButton()
    {
        if (Random.value <= SplitChance)
            _cubeCreator.CreateCubes();

        Destroy(gameObject);
    }

    public void DecreaseSplitChance(float splitChance)
    {
        SplitChance = splitChance * _cubeCreatorData.SplitCofficient;
    }
}