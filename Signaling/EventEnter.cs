using System;
using UnityEngine;

public class EventEnter : MonoBehaviour
{
    public static event Action Entered;

    public static void OnEntered()
    {
        Entered?.Invoke();
    }
}