using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Variable;
public enum colorTypes
{
    Top, Bot, Left, Right, Center, Invalid
}

/// <summary>
/// Holds all Color references
/// </summary>

public class ColorManager : Singleton<ColorManager>
{
    [SerializeField] private ColorDirection _colorData;

    [SerializeField] private ColorIntensity _lightLeft;
    [SerializeField] private ColorIntensity _lightMiddle;
    [SerializeField] private ColorIntensity _lightRight;


    public UnityEvent<colorTypes, Color> ColorPickerEvent;
    private colorTypes[] directions = new colorTypes[] { colorTypes.Top, colorTypes.Left, colorTypes.Bot, colorTypes.Right };

    private void Start()
    {
        Instance = this;
    }
}
