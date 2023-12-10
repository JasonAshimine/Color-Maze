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
        public float volume = 1f;
        [Range(-3f, 3f)]
        public float pitch = 1f;
        [Range(-1f, 1f)]
        public float stereoPan = 0f;
    }
}