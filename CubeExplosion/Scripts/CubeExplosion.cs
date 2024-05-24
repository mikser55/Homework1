using System.Collections.Generic;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    public void Explode(List<GameObject> cubes, float explosionForce, float explosionRadius)
    {
        foreach (var cube in cubes)
        {
            cube.TryGetComponent(out Rigidbody cubeRigidbody);
            cubeRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }
}