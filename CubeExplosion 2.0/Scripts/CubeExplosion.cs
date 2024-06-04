using System.Collections.Generic;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    public void ExplodeNewCubes(List<GameObject> cubes, float explosionForce, float explosionRadius)
    {
        foreach (var cube in cubes)
        {
            cube.TryGetComponent(out Rigidbody cubeRigidbody);
            cubeRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }

    public void ExplodeInRadius(Collider[] colliders, float explosionForce, float explosionRadius)
    {
        float upwardsModifier = 3f;
        Vector3 cubePosition = transform.position;

        foreach (Collider hit in colliders)
        {
            hit.TryGetComponent(out Rigidbody rigidbody);

            float distance = Vector3.Distance(cubePosition, hit.transform.position);
            float force = explosionForce * (1f - (distance / explosionRadius));

            rigidbody?.AddExplosionForce(force, cubePosition, explosionRadius, upwardsModifier, ForceMode.Impulse);
        }
    }
}