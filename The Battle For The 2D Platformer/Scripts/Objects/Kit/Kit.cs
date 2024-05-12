using System;
using UnityEngine;

public class Kit : MonoBehaviour
{
    [SerializeField] private ObjectsData _objectsData;

    public event Action ObjectCollected;

    public void OnCollected()
    {
        ObjectCollected?.Invoke();
    }

    public int GetHealNumber()
    {
        return _objectsData._kitHealNumber;
    }
}