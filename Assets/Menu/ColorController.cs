using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    public float value = 0;

    [SerializeField] private TMP_InputField text;
    [SerializeField] private Slider slider;

    public static System.Action<float, string> OnChange;

    public void handleChange(string text)
    {
        value = float.Parse(text)/255;
        UpdateValue(value);
    }

    public void handleChange(float state)
    {
        value = state;
        UpdateValue(value);
    }

    public void UpdateValue(float value)
    {
        slider.value = value;
        text.text = "" + (int)Mathf.Lerp(0f, 255f, value);

        OnChange?.Invoke(value, gameObject.name);
    }
}
