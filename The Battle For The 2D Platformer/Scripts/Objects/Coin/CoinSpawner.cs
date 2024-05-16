using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private CoinData _coinData;

    private void OnEnable()
    {
        _coin.CoinCollected += RespawnCoin;
    }

    private void OnDisable()
    {
        _coin.CoinCollected -= RespawnCoin;
    }

    public CoinData GetData()
    {
        return _coinData;
    }

    private IEnumerator SpawnCoinsCoroutine()
    {
        yield return new WaitForSeconds(_coinData.CoinSpawnTime);

        while (_coin.gameObject.activeSelf != true)
            _coin.gameObject.SetActive(true);
    }

    private void RespawnCoin()
    {
        StartCoroutine(SpawnCoinsCoroutine());
    }
}