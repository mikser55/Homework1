using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _delay = 1f;
    [SerializeField] private float _volumeChangingStep = 0.1f;

    private EventEnter _eventEnter;
    private EventExit _eventExit;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private bool _isIncreasing = false;
    private AudioSource _alarmSound;
    private Coroutine _changeVolumeCoroutine;

    private void Awake()
    {
        _alarmSound = GetComponent<AudioSource>();
        _eventEnter = GetComponent<EventEnter>();
        _eventExit = GetComponent<EventExit>();        
    }

    private void Start()
    {
        _eventEnter.Entered += RunAlarm;
        _eventExit.Exited += TurnOffAlarm;
        _alarmSound.volume = 0f;
    }

    private void RunAlarm()
    {
        _isIncreasing = true;

        if (_alarmSound.volume == 0)
            _alarmSound.Play();

        StartChangingVolume();
    }

    private void TurnOffAlarm()
    {
        _isIncreasing = false;

        StartChangingVolume();
    }

    private void StartChangingVolume()
    {
        if (_changeVolumeCoroutine != null)
            StopCoroutine(_changeVolumeCoroutine);

        _changeVolumeCoroutine = StartCoroutine(RunVolumeCoroutine(_isIncreasing));
    }

    private IEnumerator RunVolumeCoroutine(bool isIncrease)
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);
        bool isWork = true;

        while (isWork)
        {
            if (isIncrease)
                ChangeVolume(_maxVolume);
            else
                ChangeVolume(_minVolume);

            if (_alarmSound.volume == 0f)
            {
                StopCoroutine(_changeVolumeCoroutine);
                _alarmSound.Stop();
            }

            yield return wait;
        }
    }

    private void ChangeVolume(float targetVolume)
    {
        _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, targetVolume, _volumeChangingStep);
    }

    private void OnDestroy()
    {
        _eventExit.Exited -= TurnOffAlarm;
        _eventEnter.Entered -= RunAlarm;
    }
}