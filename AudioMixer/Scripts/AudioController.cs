using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    private const int _correctionNumber = 20;

    [SerializeField] private AudioMixerGroup _mixer;

    private string _soundsGroup = "ButtonsMusic";
    private string _masterGroup = "MasterMusic";
    private string _backgroundGroup = "BackgroundMusic";

    private float _musicNormalValue = 0f;
    private float _musicMinValue = -80f;

    public void ToggleMusic(bool value)
    {
        if (value)
            _mixer.audioMixer.SetFloat(_masterGroup, _musicMinValue);
        else
            _mixer.audioMixer.SetFloat(_masterGroup, _musicNormalValue);
    }

    public void ChangeAllMusicVolume(float volume)
    {
        _mixer.audioMixer.SetFloat(_masterGroup, Mathf.Log10(volume) * _correctionNumber);
    }

    public void ChangeBackgroundMusicVolume(float volume)
    {
        _mixer.audioMixer.SetFloat(_backgroundGroup, Mathf.Log10(volume) * _correctionNumber);
    }

    public void ChangeButtonsMusicVolume(float volume)
    {
        _mixer.audioMixer.SetFloat(_soundsGroup, Mathf.Log10(volume) * _correctionNumber);
    }
}