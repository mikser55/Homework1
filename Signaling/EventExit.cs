using System;
using UnityEngine;

public class EventExit : MonoBehaviour
{
    public static event Action Exited;

    public static void OnExited()
    {
        Exited?.Invoke();
    }
}