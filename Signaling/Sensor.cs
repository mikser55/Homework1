using UnityEngine;

public class Sensor : MonoBehaviour
{
    private EventExit _eventExit;
    private EventEnter _eventEnter;

    private void Start()
    {
        _eventExit = GetComponent<EventExit>();
        _eventEnter = GetComponent<EventEnter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _eventEnter.OnEntered();
    }

    private void OnTriggerExit(Collider other)
    {
        _eventExit.OnExited();
    }
}