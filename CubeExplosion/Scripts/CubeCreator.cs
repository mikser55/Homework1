using System.Collections.Generic;
using UnityEngine;

public class CubeCreator : MonoBehaviour
{
    [SerializeField] private CubeCreatorData _cubeCreatorData;
    [SerializeField] private ExplosionData _explosionData;

    public void CreateCubes()
    {
        Vector3 newScale = transform.localScale * _cubeCreatorData.ScaleCofficient;
        List<GameObject> newCubes = new();

        for (int i = 0; i < Random.Range(_cubeCreatorData.MinCubeAmount, _cubeCreatorData.MaxCubeAmount); i++)
        {
            GameObject newCubeObject = Instantiate(transform.gameObject, transform.position, transform.rotation);
            newCubeObject.transform.localScale = newScale;
            newCubeObject.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
            newCubeObject.TryGetComponent(out Cube cubeComponent);
            Cube currentCube = gameObject.GetComponent<Cube>();
            cubeComponent.DecreaseSplitChance(currentCube.SplitChance) ;
            newCubes.Add(newCubeObject);
        }

        transform.gameObject.TryGetComponent(out CubeExplosion cubeExplosion);
        cubeExplosion.Explode(newCubes, _explosionData.ExplosionForce, _explosionData.ExplosionRadius);
    }
}