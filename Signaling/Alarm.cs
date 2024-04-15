using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _delay = 1f;
    [SerializeField] private float _volumeChangingStep = 0.1f;

    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private bool _isIncreasing = false;
    private AudioSource _alarmSound;
    private Coroutine _changeVolumeCoroutine;


    private void Start()
    {
        _alarmSound = GetComponent<AudioSource>();
        _alarmSound.volume = 0f;
        EventEnter.Entered += RunAlarm;
        EventExit.Exited += TurnOffAlarm;
    }

    private void RunAlarm()
    {
        _isIncreasing = true;

        if (_alarmSound.volume == 0)
            _alarmSound.Play();

        if (_changeVolumeCoroutine != null)
            StopCoroutine(_changeVolumeCoroutine);

        _changeVolumeCoroutine = StartCoroutine(RunVolumeCoroutine(_isIncreasing));
    }

    private void TurnOffAlarm()
    {
        _isIncreasing = false;

        if (_changeVolumeCoroutine != null)
            StopCoroutine(_changeVolumeCoroutine);

        _changeVolumeCoroutine = StartCoroutine(RunVolumeCoroutine(_isIncreasing));
    }

    private void OnDestroy()
    {
        EventExit.Exited -= TurnOffAlarm;
        EventEnter.Entered -= RunAlarm;
    }

    private IEnumerator RunVolumeCoroutine(bool increase)
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);
        bool isWork = true;

        while (isWork)
        {
            if (increase)
                ChangeVolume(_maxVolume);
            else
                ChangeVolume(_minVolume);

            if (_alarmSound.volume == 0f)
                StopCoroutine(_changeVolumeCoroutine);

            yield return wait;
        }
    }

    private void ChangeVolume(float targetVolume)
    {
        _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, targetVolume, _volumeChangingStep);
    }
}