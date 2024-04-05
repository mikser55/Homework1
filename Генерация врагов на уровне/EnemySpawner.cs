using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _originalEnemy;
    [SerializeField] private float spawnWaitTime = 2f;
    [SerializeField] private List<Transform> _positions;

    private bool _isWork = false;
    private Vector3 _direction;
    private Coroutine _coroutine;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            ToggleSpawner();

            if (_isWork)
                _coroutine = StartCoroutine(SpawnEnemy());
            else
                StopCoroutine(_coroutine);
        }
    }

    private IEnumerator SpawnEnemy()
    {
        int[] directionNumber = { -1, 1 };

        WaitForSeconds wait = new WaitForSeconds(spawnWaitTime);

        while (_isWork)
        {
            float randomDirection = directionNumber[Random.Range(0, directionNumber.Length)];
            Transform position = _positions[Random.Range(0, _positions.Count)];
            _direction = new Vector3(randomDirection, 0, randomDirection);
            GameObject enemyCopy = Instantiate(_originalEnemy, position.position, Quaternion.identity);
            enemyCopy.GetComponent<EnemyController>().SetPosition(_direction);

            yield return wait;
        }
    }

    private void ToggleSpawner()
    {
        _isWork = !_isWork;
    }
}