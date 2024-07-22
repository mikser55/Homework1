using UnityEngine;

public class BasePoint : MonoBehaviour
{
    public bool IsFree { get; private set; } = true;

    public void ReservePoint()
    {
        IsFree = false;
    }

    public void SetFree()
    {
        IsFree = true;
    }
}