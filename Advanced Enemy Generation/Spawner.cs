using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Movement _enemyPrefab;
    [SerializeField] private float _delay = 2f;
    [SerializeField] private Transform _targetPoint;

    private Vector3 _position;
    private bool _isWork = false;
    private Coroutine _coroutine;

    private void Start()
    {
        _position = transform.position;
    }

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
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_isWork)
        {
            Movement enemy = Instantiate(_enemyPrefab, _position, Quaternion.identity);
            enemy.SetTargetPoint(_position, _targetPoint.position);

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