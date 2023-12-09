using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;

namespace Variable
{
    [CreateAssetMenu(fileName = "LightDataSet", menuName = "Data/LightDataSet")]
    public class LightDataSet : ScriptableObject
    {
        public bool toggleLeft = true;
        public bool toggleRight = true;

        [SerializeField]
        private GameEventData _LightEvent;

        public ColorIntensity Middle;
        public ColorIntensity Left;
        public ColorIntensity Right;

        public void Raise(LightEventType type)
        {
            _LightEvent.Raise(type);
        }
    }
}


