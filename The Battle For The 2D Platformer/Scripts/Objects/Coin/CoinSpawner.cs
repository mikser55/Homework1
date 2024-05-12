using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private ObjectsData _objectsData;

    private void OnEnable()
    {
        _coin.CoinCollected += RespawnCoin;
    }

    private void RespawnCoin()
    {
        StartCoroutine(SpawnCoinsCoroutine());
    }

    private IEnumerator SpawnCoinsCoroutine()
    {
        WaitForSeconds wait = new(_objectsData.CoinSpawnTime);

        yield return wait;

        while (_coin.gameObject.activeSelf != true)
        {
            _coin.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        _coin.CoinCollected -= RespawnCoin;
    }
}