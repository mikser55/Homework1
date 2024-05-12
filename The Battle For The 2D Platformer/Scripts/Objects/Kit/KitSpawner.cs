using System.Collections;
using UnityEngine;

public class KitSpawner : MonoBehaviour
{
    [SerializeField] private Kit _kit;
    [SerializeField] private ObjectsData _objectsData;

    private void OnEnable()
    {
        _kit.ObjectCollected += RespawnObject;
    }

    private void RespawnObject()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        WaitForSeconds wait = new(_objectsData.CoinSpawnTime);

        yield return wait;

        while (_kit.gameObject.activeSelf != true)
        {
            _kit.transform.position = new Vector2(Random.Range(_objectsData.XMinPosition, _objectsData.XMaxPosition), _objectsData.YPosition);
            _kit.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        _kit.ObjectCollected -= RespawnObject;
    }
}