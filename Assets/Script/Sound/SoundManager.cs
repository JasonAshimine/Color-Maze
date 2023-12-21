using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variable;


public enum SoundEventType 
{
    Music,
    Volume,
    Pitch
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
        UpdateVolume();
        ChangeMainMenuMusic(_soundData.DefaultMusic);
    }

    public void FixedUpdate()
    {
        if (_soundData.musicLevel.toggle)
        {
            UpdateAudioLevel(_soundData.soundLevel);
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
                UpdateVolume();
                break;
            case SoundEventType.Pitch:
                UpdatePitch();
                break;
        }
    }

    private void UpdateVolume()
    {
        _audioSource.volume = _soundData.musicLevel.volume;
        _audioSourceMenu.volume = _soundData.musicLevel.volume;
    }

    private void UpdatePitch()
    {
        _audioSource.pitch = _soundData.soundLevel.pitch;
        _audioSourceMenu.pitch = _soundData.musicLevel.pitch;
    }


    private void UpdateAudioLevel(AudioLevel level) 
    {
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
        ChangeMainMenuMusic(_stateData.LevelList[index].Music);
    }

    public void ChangeLevelMusic(object obj)
    {
        GameStage data = (GameStage)obj;

        switch (data)
        {
            case GameStage.LoadLevel:
                ChangeLevelMusic(_stateData.GetLevel().Music);
                break;
            case GameStage.MainMenu:
                PlayMenuMusic();
                break;
        }
    }

    private void PlayMenuMusic()
    {
        if (_audioSourceMenu.isPlaying)
        {
            return;
        }

        PauseMusic();
        _audioSourceMenu.Play();
    }

    private void ChangeLevelMusic(AudioClip audioClip)
    {
        ChangeMusic(_audioSource, audioClip);
    }

    private void ChangeMainMenuMusic(AudioClip audioClip)
    {
        ChangeMusic(_audioSourceMenu, audioClip);
    }


    private void ChangeMusic(AudioSource source, AudioClip audioClip)
    {
        PauseMusic();
        if (source.clip == audioClip)
        {
            source.Play();
            return;
        }

        source.Stop();
        source.clip = audioClip;
        source.Play();
    }

    private void PauseMusic()
    {
        _audioSource.Pause();
        _audioSourceMenu.Pause();
    }
}
