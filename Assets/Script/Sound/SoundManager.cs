using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variable;


public enum SoundEventType 
{
    Music,
    Volume
}


public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundDataSet _soundData;
    [SerializeField] private LightDataSet _lightData;
    //[SerializeField] private List<AudioClip> _soundList;

    [SerializeField] private StateDataSet _stateData;

    [SerializeField, Range(0f, 1f)] 
    private float _panMax = 0.8f;

/*    [SerializeField, Range(0.5f, 2f)]
    private float _frontMin;
    [SerializeField, Range(0f, 2f)]
    private float _frontMax;*/

    [SerializeField]
    private AudioSource _audioSourceMenu;

    [SerializeField]
    private AudioSource _audioSource;

    private void Awake()
    {
        _soundData.Reset();
    }

    public void FixedUpdate()
    {
        if (_soundData.musicLevel.toggle)
        {
            UpdateAudioLevel(_soundData.musicLevel);
        }
    }

    public void HandleEvent(object data)
    {
        switch ((SoundEventType)data) 
        {
            case SoundEventType.Music:
                ChangeMusic(_soundData.menuIndex);
                break;
            case SoundEventType.Volume:
                break;
        }
    }

    private void UpdateAudioLevel(AudioLevel level) 
    { 
        _audioSource.volume = level.volume;
        _audioSource.panStereo = CalcPanDirection();
        _audioSource.pitch = level.pitch;
    }

    private float CalcPanDirection()
    {
        float left = _lightData.Left.intensity;
        float right = _lightData.Right.intensity;

        return Mathf.Lerp(-_panMax, _panMax, right / (left+right));
    }

/*    private float CalcPictchDirection()
    {
        float front = _lightData.Middle.intensity;

        return Mathf.Lerp(_frontMin, _frontMax, front / 5f);
    }*/

    private void ChangeMusic(int index)
    {
        ChangeMusic(_stateData.LevelList[index].Music);
    }

    public void ChangeLevelMusic(object obj)
    {
        GameStage data = (GameStage)obj;

        switch (data)
        {
            case GameStage.LoadLevel:
                ChangeMusic(_stateData.GetLevel().Music);
                break;
            case GameStage.MainMenu:
                ChangeMusic(_soundData.DefaultMusic);
                break;
        }
    }

    private void ChangeMusic(AudioClip audioClip)
    {
        _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    private void ToggleMusic()
    {
        if (_soundData.musicLevel.toggle)
            _audioSource.UnPause();
        else
            _audioSource.Pause();
    }


    private void Play(AudioClip source)
    {
        if (_soundData.soundLevel.toggle == false)
            return;

        if (source != null)
            _audioSource.PlayOneShot(source, _soundData.soundLevel.volume);
    }
}
