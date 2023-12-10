using Game.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Variable
{
    [CreateAssetMenu(fileName = "ColorIntensity", menuName = "Variable/ColorIntensity")]
    public class ColorIntensity : ScriptableObject
    {
        public Color color;
        public float intensity = 1f;
    }
}

