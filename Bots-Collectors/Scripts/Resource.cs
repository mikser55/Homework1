using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsCollecting { get; private set; }

    public void Init()
    {
        IsCollecting = false;
    }

    public void ReserveResource()
    {
        IsCollecting = true;
    }
}