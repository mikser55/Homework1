using UnityEngine;

[RequireComponent(typeof(CubeExplosion), typeof(CubeCreator))]
public class Cube : MonoBehaviour
{
    private const float MaxExplosionForceMultiplier = 1f;

    [SerializeField] private CubeCreatorData _cubeCreatorData;
    [SerializeField] private ExplosionData _explosionData;

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
        else
            Explode();

        Destroy(gameObject);
    }

    private void Explode()
    {
        Vector3 cubeSize = transform.localScale;
        float currentExplosionRadius = _explosionData.ExplosionRadius * (MaxExplosionForceMultiplier / cubeSize.magnitude);
        float currentExplosionForce = _explosionData.ExplosionForce * cubeSize.magnitude;

        Collider[] colliders = Physics.OverlapSphere(transform.position, currentExplosionRadius);

        TryGetComponent(out CubeExplosion cubeExplosion);
        cubeExplosion?.ExplodeInRadius(colliders, currentExplosionForce, currentExplosionRadius);
    }

    public void DecreaseSplitChance(float splitChance)
    {
        SplitChance = splitChance * _cubeCreatorData.SplitCofficient;
    }
}