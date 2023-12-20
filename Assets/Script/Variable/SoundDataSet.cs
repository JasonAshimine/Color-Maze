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

        public void Play(AudioClip audio)
        {
        }

        public void Stop(AudioClip audio)
        {

        }

        public void Reset()
        {
            menuIndex = PlayerPrefs.GetInt("SoundData_MenuIndex", 0);
        }

        public void Save()
        {
            PlayerPrefs.SetInt("SoundData_MenuIndex", menuIndex);
            PlayerPrefs.Save();
        }
    }
}
