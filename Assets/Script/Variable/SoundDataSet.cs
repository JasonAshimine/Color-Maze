using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;
using System;

namespace Variable
{
    [CreateAssetMenu(fileName = "SoundDataSet", menuName = "Data/Sound")]
    public class SoundDataSet : GameEventData
    {
        public AudioLevel musicLevel;
        public AudioLevel soundLevel;

        public AudioClip DefaultMusic;

        public int menuIndex = 0;


        public void SetMusicVolume(float value)
        {
            if (musicLevel.volume == value)
                return;

            musicLevel.volume = value;
            musicLevel.Save();
            Raise(SoundEventType.Volume);
        }

        public void SetMusicPitch(float value)
        {
            if (value == musicLevel.pitch)
                return;
            musicLevel.pitch = value;
            Raise(SoundEventType.Pitch);
        }

        public void Play(AudioClip audio)
        {
        }

        public void Stop(AudioClip audio)
        {

        }

        public void Reset()
        {
            menuIndex = PlayerPrefs.GetInt("SoundData_MenuIndex", 0);
            musicLevel.Reset();
            soundLevel.Reset();
        }

        public void Save()
        {
            PlayerPrefs.SetInt("SoundData_MenuIndex", menuIndex);
            PlayerPrefs.Save();
        }
    }
}
