using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float _shotSpeed;
    [SerializeField] private float _timeWaitShooting;
    [SerializeField] private GameObject _bullet;

    private Transform _target;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        bool isWork = true;

        while (isWork)
        {
            Vector3 shotDirection = (_target.position - transform.position).normalized;
            GameObject newBullet = Instantiate(_bullet, transform.position + shotDirection, Quaternion.identity);

            newBullet.transform.up = shotDirection;
            newBullet.GetComponent<Rigidbody>().velocity = shotDirection * _shotSpeed;

            yield return new WaitForSeconds(_timeWaitShooting);
        }
    }
}