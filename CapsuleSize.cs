using UnityEngine;

public class CapsuleSize : MonoBehaviour
{
    [SerializeField] private Vector3 _speed;

    private void Update()
    {
        transform.localScale += _speed * Time.deltaTime;
    }
}