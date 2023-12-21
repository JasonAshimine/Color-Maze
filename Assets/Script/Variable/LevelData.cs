using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Variable
{
    [CreateAssetMenu(fileName = "Level", menuName = "Variable/Level")]
    public class LevelData : ScriptableObject
    {
        public Stage Stage;
        public Sprite Sprite;
        public AudioClip Music;
    }
}
