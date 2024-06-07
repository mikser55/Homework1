using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Ground : MonoBehaviour
{
    public MeshRenderer GetMeshRenderer()
    {
        return GetComponent<MeshRenderer>();
    }
}