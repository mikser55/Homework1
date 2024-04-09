using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour
{
    [SerializeField] private float _delay = 1f;
    [SerializeField] private float _volumeChangingStep = 0.1f;

    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private AudioSource _alarmSound;
    private Coroutine increaseCoroutine;
    private Coroutine decreaseCoroutine;

    private void Start()
    {
        _alarmSound = GetComponent<AudioSource>();
        _alarmSound.volume = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (decreaseCoroutine != null)
            StopCoroutine(decreaseCoroutine);

        increaseCoroutine = StartCoroutine(IncreaseVolume());
    }

    private void OnTriggerExit(Collider other)
    {
        if (increaseCoroutine != null)
            StopCoroutine(increaseCoroutine);

        decreaseCoroutine = StartCoroutine(DecreaseVolume());
    }

    private IEnumerator IncreaseVolume()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        _alarmSound.Play();


        while (_alarmSound.volume < _maxVolume)
        {
            _alarmSound.volume += Mathf.MoveTowards(_minVolume, _maxVolume, _volumeChangingStep);

            yield return wait;
        }
    }

    private IEnumerator DecreaseVolume()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_alarmSound.volume > 0)
        {
            _alarmSound.volume -= Mathf.MoveTowards(_minVolume, _maxVolume, _volumeChangingStep);

            yield return wait;
        }

        _alarmSound.Stop();
    }
}