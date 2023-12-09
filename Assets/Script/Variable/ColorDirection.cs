using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Variable
{
    [CreateAssetMenu(fileName = "ColorData", menuName = "Variable/ColorData")]
    public class ColorDirection : ScriptableObject
    {
        public ColorVariable Top;
        public ColorVariable Bot;
        public ColorVariable Left;
        public ColorVariable Right;
        public ColorVariable Center;

        public Game.Events.GameEventData _OnChange;

        public ColorVariable[] Forward;

        public void SetDirection(int index)
        {
            for(int i = 0; i < 3; i++)
            {
                Forward[i] = GetColor(index - i - 1);
            }

            _OnChange.Raise();
        }


        public ColorVariable GetColor(int index)
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
            }

            return null;
        }

    }
}


