using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Variable
{
    [CreateAssetMenu(fileName = "ColorData", menuName = "Variable/ColorData")]
    public class ColorDirection : ScriptableObject
    {
        public ColorIntensity Top;
        public ColorIntensity Bot;
        public ColorIntensity Left;
        public ColorIntensity Right;
        public ColorIntensity Center;

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
            }

            return null;
        }

    }
}


