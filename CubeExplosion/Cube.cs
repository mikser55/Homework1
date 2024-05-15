using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 10f;
    [SerializeField] private float _explosionForce = 100f;

    private int _minCubeAmount = 2;
    private int _maxCubeAmount = 7;
    private float _reproduceChance = 1f;
    private float _scaleCofficient = 0.5f;
    private float _reproduceCofficient = 0.5f;

    private void OnMouseUpAsButton()
    {
        if (Random.value <= _reproduceChance)
        {
            CreateCubes();
            Explode();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CreateCubes()
    {
        Vector3 newScale = transform.localScale * _scaleCofficient;

        for (int i = 0; i < Random.Range(_minCubeAmount, _maxCubeAmount); i++)
        {
            GameObject newCube = Instantiate(transform.gameObject, transform.position, transform.rotation);
            newCube.transform.localScale = newScale;
            newCube.GetComponent<Cube>().DecreaseSpawnChance(_reproduceChance);
            newCube.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        }

    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);

    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> explodedObjects = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                explodedObjects.Add(hit.attachedRigidbody);

        return explodedObjects;
    }

    private void DecreaseSpawnChance(float reproduceChance)
    {
        _reproduceChance = reproduceChance * _reproduceCofficient;
    }
}