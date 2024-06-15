using System;
using UnityEngine;

public class HealthTracker : MonoBehaviour
{
    public event Action HealthUpdated;

    public void OnHealthUpdated()
    {
        HealthUpdated?.Invoke();
    }
}