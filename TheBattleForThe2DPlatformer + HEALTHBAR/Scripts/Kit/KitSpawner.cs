using System.Collections;
using UnityEngine;

public class KitSpawner : MonoBehaviour
{
    [SerializeField] private Kit _kit;
    [SerializeField] private KitData _kitData;

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
        WaitForSeconds wait = new(_kitData.KitSpawnTime);

        yield return wait;

        while (_kit.gameObject.activeSelf != true)
        {
            _kit.transform.position = new Vector2(Random.Range(_kitData.XMinPosition, _kitData.XMaxPosition), _kitData.YPosition);
            _kit.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        _kit.ObjectCollected -= RespawnObject;
    }
}