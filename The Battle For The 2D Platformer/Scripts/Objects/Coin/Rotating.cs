using UnityEngine;

public class Rotating : MonoBehaviour
{
    [SerializeField] private Transform _coinTransform;

    private void Update()
    {
        _coinTransform.Rotate(0f, 180f * Time.deltaTime, 0f);
    }
}