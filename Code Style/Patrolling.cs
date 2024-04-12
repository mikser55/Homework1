using UnityEngine;

public class Patrolling : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _destinationPoint;

    private Transform[] _places;
    private int _numberOfPlace;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        _places = new Transform[_destinationPoint.childCount];

        for (int i = 0; i < _destinationPoint.childCount; i++)
            _places[i] = _destinationPoint.GetChild(i).GetComponent<Transform>();
    }

    private void Update()
    {
        Transform _pointByIndex = _places[_numberOfPlace];
        _transform.position = Vector3.MoveTowards(_transform.position, _pointByIndex.position, _speed * Time.deltaTime);

        if (_transform.position == _pointByIndex.position)
            MoveNextPlace();
    }

    private void MoveNextPlace()
    {
        _numberOfPlace++;

        if (_numberOfPlace == _places.Length)
            _numberOfPlace = 0;

        Vector3 destination = _places[_numberOfPlace].transform.position;
        _transform.forward = destination - _transform.position;
    }
}