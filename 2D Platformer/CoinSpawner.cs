using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private float _spawnTime = 5f;
    [SerializeField] Transform _spawnPosition;

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
        WaitForSeconds wait = new(_spawnTime);

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