using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float _shotSpeed;
    [SerializeField] private float _timeWaitShooting;
    [SerializeField] private Rigidbody _bulletPrefab;

    private Transform _target;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        bool isWork = true;

        WaitForSeconds wait = new WaitForSeconds(_timeWaitShooting);

        while (isWork)
        {
            Vector3 shotDirection = (_target.position - transform.position).normalized;
            Rigidbody newBullet = Instantiate(_bulletPrefab, transform.position + shotDirection, Quaternion.identity);

            newBullet.transform.up = shotDirection;
            newBullet.GetComponent<Rigidbody>().velocity = shotDirection * _shotSpeed;

            yield return wait;
        }
    }
}