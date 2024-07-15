using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Bird _bird;
    [SerializeField] private float _delay = 5f;

    private int _maxEnemyNumber = 3;
    private int _currentEnemyNumber = 1;
    private Coroutine _coroutine;

    private void Start()
    {
        StartSpawn();
    }

    private void StartSpawn()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(SpawnCoroutine());

        _currentEnemyNumber--;
    }

    private IEnumerator SpawnCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_currentEnemyNumber != _maxEnemyNumber)
        {
            Enemy enemy = Instantiate(_prefab);
            enemy.Destroyed += StartSpawn;
            enemy.Destroyed += _bird.IncreaseScoreNumber;
            _currentEnemyNumber++;

            yield return wait;
        }

        _coroutine = null;
    }
}