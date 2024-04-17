using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Sensor))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _delay = 1f;
    [SerializeField] private float _volumeChangingStep = 0.1f;

    private Sensor _sensor;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private AudioSource _alarmSound;
    private Coroutine _changeVolumeCoroutine;

    private void Awake()
    {
        _alarmSound = GetComponent<AudioSource>();
        _sensor = GetComponent<Sensor>();
    }

    private void Start()
    {
        _sensor.Entered += RunAlarm;
        _sensor.Exited += TurnOffAlarm;
        _alarmSound.volume = 0f;
    }

    private void RunAlarm()
    {
        if (_alarmSound.volume == 0)
            _alarmSound.Play();

        StartChangingVolume(_maxVolume);
    }

    private void TurnOffAlarm()
    {
        StartChangingVolume(_minVolume);
    }

    private void StartChangingVolume(float targetVolume)
    {
        if (_changeVolumeCoroutine != null)
            StopCoroutine(_changeVolumeCoroutine);

        _changeVolumeCoroutine = StartCoroutine(RunVolumeCoroutine(targetVolume));
    }

    private IEnumerator RunVolumeCoroutine(float targetVolume)
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_alarmSound.volume != targetVolume)
        {
            if (_alarmSound.volume < targetVolume)
                ChangeVolume(_maxVolume);
            else
                ChangeVolume(_minVolume);

            yield return wait;
        }

        if (_alarmSound.volume == 0f)
            _alarmSound.Stop();
    }

    private void ChangeVolume(float targetVolume)
    {
        _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, targetVolume, _volumeChangingStep);
    }

    private void OnDestroy()
    {
        _sensor.Exited -= TurnOffAlarm;
        _sensor.Entered -= RunAlarm;
    }
}