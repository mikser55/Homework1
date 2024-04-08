using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyPrefab;
    [SerializeField] private float _delay = 2f;
    [SerializeField] private List<Transform> _positions;

    private bool _isWork = false;
    private Vector3 _direction;
    private Coroutine _coroutine;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (_isWork)
                TurnOff();
            else
                TurnOn();

            if (_isWork)
                _coroutine = StartCoroutine(SpawnEnemy());
            else
                StopCoroutine(_coroutine);
        }
    }

    private IEnumerator SpawnEnemy()
    {
        int[] directionNumber = { -1, 1 };

        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_isWork)
        {
            float randomDirection = directionNumber[Random.Range(0, directionNumber.Length)];
            Transform randomPosition = _positions[Random.Range(0, _positions.Count)];
            _direction = new Vector3(randomDirection, 0, randomDirection);
            EnemyMovement enemy = Instantiate(_enemyPrefab, randomPosition.position, Quaternion.identity);
            enemy.SetDirection(_direction);

            yield return wait;
        }
    }

    private void TurnOn()
    {
        _isWork = true;
    }

    private void TurnOff()
    {
        _isWork = false;
    }
}