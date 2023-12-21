using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Variable
{
    [CreateAssetMenu(fileName = "AudioLevel", menuName = "Variable/AudioLevel")]
    public class AudioLevel : ScriptableObject
    {
        public bool toggle = true;

        [Range(0f, 1f)]
        public float volume;
        [Range(-3f, 3f)]
        public float pitch;
        [Range(-1f, 1f)]
        public float stereoPan;

        public void Reset()
        {
            volume = PlayerPrefs.GetFloat(Name("Volume"), 0.7f);
            //pitch = PlayerPrefs.GetFloat(Name("Pitch"), 1f);
            //stereoPan = PlayerPrefs.GetFloat(Name("StereoPan"), 0f);
            pitch = 1f;
            stereoPan = 0f;
        }
        public void Save()
        {
            PlayerPrefs.SetFloat(Name("Volume"), volume);
            //PlayerPrefs.SetFloat(Name("Pitch"), pitch);
            //PlayerPrefs.SetFloat(Name("StereoPan"), stereoPan);

            PlayerPrefs.Save();
        }

        private string Name(string tag)
        {
            return $"AudioLevel_{name}_{tag}";
        }
    }
}