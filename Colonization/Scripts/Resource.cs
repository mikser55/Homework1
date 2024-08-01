using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public event Action<Resource> ReleaseWaiting;

    public void OnWaiting(Resource resource)
    {
        ReleaseWaiting?.Invoke(resource);
    }
}