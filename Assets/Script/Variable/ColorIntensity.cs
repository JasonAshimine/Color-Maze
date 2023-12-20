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
        [SerializeField]
        private Color _defaultColor;
        public Color color;
        public float intensity = 1f;

        public void Reset()
        {
            intensity = 1f;

            color = new Color(
                PlayerPrefs.GetFloat(Name("r"), _defaultColor.r),
                PlayerPrefs.GetFloat(Name("g"), _defaultColor.g),
                PlayerPrefs.GetFloat(Name("b"), _defaultColor.b),
                1f);
        }

        public void Save()
        {
            PlayerPrefs.SetFloat(Name("r"), color.r);
            PlayerPrefs.SetFloat(Name("g"), color.g);
            PlayerPrefs.SetFloat(Name("b"), color.b);
        }

        private string Name(string c)
        {
            return $"ColorIntenisty_{c}_{name}";
        }
    }
}

