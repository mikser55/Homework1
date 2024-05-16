using System;
using UnityEngine;

public class Kit : MonoBehaviour
{
    public event Action ObjectCollected;

    public void OnCollected()
    {
        ObjectCollected?.Invoke();
    }
}