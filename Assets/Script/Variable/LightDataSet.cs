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
        public bool toggleMiddle = true;
        public bool toggleRight = true;
        
        [SerializeField]
        private GameEventData _LightEvent;

        public ColorIntensity Middle;
        public ColorIntensity Left;
        public ColorIntensity Right;



        public void SetLights(ColorIntensity left, ColorIntensity middle, ColorIntensity right)
        {
            Left = left;
            Middle = middle;
            Right = right;
            Raise(LightEventType.Color);
        }

        public void SetLights(ColorIntensity left, ColorIntensity right)
        {
            SetLights(left, Middle, right);
        }

        public void SetLights(ColorIntensity All)
        {
            SetLights(All, All, All);
        }


        public void Toggle(bool left, bool middle, bool right)
        {
            toggleLeft = left;
            toggleMiddle = middle;
            toggleRight = right;
            Raise(LightEventType.Toggle);
        }

        public void Toggle(bool left, bool right)
        {
            Toggle(left, toggleMiddle, right);
        }

        public ColorIntensity GetColor(string name)
        {
            switch (name)
            {
                case "Left": return Left;
                case "Right": return Right;
                case "Middle": 
                case "Center": return Middle;
                default: return null;
            }
        }

        public void Raise(LightEventType type)
        {
            _LightEvent.Raise(type);
        }

        public void Reset()
        {
            toggleLeft = true;
            toggleMiddle = true;
            toggleRight = true;
        }
    }
}


