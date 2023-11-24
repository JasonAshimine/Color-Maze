using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGB_Script : MonoBehaviour
{
    public Color Color;

    public Image box;
    public GameObject Red;
    public GameObject Green;
    public GameObject Blue;
    public GameObject Alpha;

    private void Start()
    {
        Color = Color.white;
    }

    private void OnEnable()
    {
        ColorController.OnChange += handleColorChange;
    }
    private void OnDisable()
    {
        ColorController.OnChange -= handleColorChange;
    }

    private void handleColorChange(float value, string name)
    {
        switch (name)
        {
            case "Red":
                Color.r = value;
                break;
            case "Blue":
                Color.b = value;
                break;
            case "Green":
                Color.g = value;
                break;
            case "Alpha":
                Color.a = value;
                break;
        }

        UpdateColor();
    }

    public void UpdateColor()
    {
        box.color = Color;
    }
}
