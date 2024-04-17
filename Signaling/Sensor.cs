using UnityEngine;
using System;

public class Sensor : MonoBehaviour
{
    public event Action Entered;
    public event Action Exited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            OnEntered();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            OnExited();
    }

    public void OnExited()
    {
        Exited?.Invoke();
    }

    public void OnEntered()
    {
        Entered?.Invoke();
    }
}