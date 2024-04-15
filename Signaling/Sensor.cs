using UnityEngine;

public class Sensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventEnter.OnEntered();
    }

    private void OnTriggerExit(Collider other)
    {
        EventExit.OnExited();
    }
}