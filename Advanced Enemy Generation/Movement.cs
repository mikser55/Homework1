using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Vector3 _spawnPosition;
    private Vector3 _targetPoint;
    private float elapsedTime;

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentComplete = elapsedTime / _speed;
        transform.position = Vector3.Lerp(_spawnPosition, _targetPoint, percentComplete);
    }

    public void SetTargetPoint(Vector3 spawnPosition, Vector3 target)
    {
        _spawnPosition = spawnPosition;
        _targetPoint = target;
    }
}