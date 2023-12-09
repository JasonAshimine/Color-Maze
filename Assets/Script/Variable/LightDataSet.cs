using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;

namespace Variable
{
    [CreateAssetMenu(fileName = "LightDataSet", menuName = "Data/LightDataSet")]
    public class LightDataSet : ScriptableObject
    {
        public bool toggleLeft;
        public bool toggleRight;

        [SerializeField]
        private GameEventData _LightEvent;

        public ColorIntensity Middle { get; set; }
        public ColorIntensity Left { get; set; }
        public ColorIntensity Right { get; set; }

        public void Raise(LightEventType type)
        {
            _LightEvent.Raise(type);
        }
    }
}


