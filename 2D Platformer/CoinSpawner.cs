using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime = 5f;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] Transform _spawnPosition;

    private int _maxCoins = 1;
    private int _currentNumberCoins = 0;
    private GameObject _currentCoin;

    private void Start()
    {
        _currentCoin = Instantiate(_coinPrefab, _spawnPosition.position, Quaternion.identity);
        _currentNumberCoins++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _currentCoin != null)
        {
            Destroy(_currentCoin);
            _currentNumberCoins--;
            StartCoroutine(SpawnCoinsCoroutine());
        }
    }

    private IEnumerator SpawnCoinsCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnTime);

        while (_currentNumberCoins < _maxCoins)
        {
            yield return wait;

            _currentCoin = Instantiate(_coinPrefab, _spawnPosition.position, Quaternion.identity);
            _currentNumberCoins++;
        }
    }
}