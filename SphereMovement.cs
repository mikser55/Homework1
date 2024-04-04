using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _speed;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime);
    }
}