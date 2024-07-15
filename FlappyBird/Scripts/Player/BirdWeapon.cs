using UnityEngine;

public class BirdWeapon : MonoBehaviour
{
    [SerializeField] PlayerPool _pool;

    private PlayerInputs _inputs;

    private void Awake()
    {
        _inputs = new();
        _inputs.Player.Shoot.performed += context => Shoot();
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    private void Shoot()
    {
        _pool.GetObject();
    }
}