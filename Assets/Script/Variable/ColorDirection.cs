using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;

namespace Variable
{
    [CreateAssetMenu(fileName = "ColorData", menuName = "Variable/ColorData")]
    public class ColorDirection : ScriptableObject
    {
        public GameEventData Event;

        [Space()]
        public ColorIntensity Top;
        public ColorIntensity Bot;
        public ColorIntensity Left;
        public ColorIntensity Right;
        public ColorIntensity Center;

        [Space()]
        public ColorIntensity Default;

        public void Raise()
        {
            Event.Raise();
        }

        public ColorIntensity GetColor(int index)
        {
            if (index < 0)
                index = 3;
            else
                index %= 4;

            switch (index) 
            {
                case 0: return Top;
                case 1: return Left;
                case 2: return Bot;
                case 3: return Right;
                default: return null;
            }
        }

        public ColorIntensity GetColorAll(int index)
        {
            switch (index)
            {
                case 0: return Top;
                case 1: return Left;
                case 2: return Bot;
                case 3: return Right;
                default: return Center;
            }
        }

        public ColorIntensity GetColor(string name)
        {
            switch (name)
            {
                case "Top": return Top;
                case "Bot": return Bot;
                case "Left": return Left;
                case "Right": return Right;
                case "Center": return Center;
                default: return null;
            }
        }

        public void Reset()
        {
            Center.Reset();
            Top.Reset();
            Bot.Reset();
            Left.Reset();
            Right.Reset();
            Default.Reset();
        }

        public void HardReset()
        {
            Center.color = Color.white;
            Top.color = Color.red;
            Bot.color = Color.green;
            Left.color = Color.blue;
            Right.color = Color.yellow;
        }


        public void Save()
        {
            Center.Save();
            Top.Save();
            Bot.Save();
            Left.Save();
            Right.Save();

            PlayerPrefs.Save();
        }

    }
}


