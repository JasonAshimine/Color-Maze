using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum colorTypes
{
    Top, Bot, Left, Right, Center, Invalid
}

public class ColorManager : Singleton<ColorManager>
{
    public Color Top;
    public Color Bot;
    public Color Left;
    public Color Right;
    public Color Center;


    public UnityEvent<colorTypes, Color> ColorPickerEvent;

    private colorTypes[] directions = new colorTypes[] { colorTypes.Top, colorTypes.Right, colorTypes.Bot, colorTypes.Left };

    private void Start()
    {
        Instance = this;
    }

    public Color GetColor(colorTypes id)
    {
        switch (id)
        {
            case colorTypes.Top: return Top;
            case colorTypes.Bot: return Bot;
            case colorTypes.Left: return Left;
            case colorTypes.Right: return Right;
            case colorTypes.Center: return Center;
            default:
                Debug.Log(string.Format("Invalid id {0}", id));
                return null;
        }
    }

    public void UpdateColor(colorTypes id, Color color)
    {
        GetColor(id). = color;
        ColorPickerEvent.Invoke(id, color);
    }

    public Color GetColor(int index)
    {
        if (index < 0)
            index = directions.Length - 1;
        else
            index %= directions.Length;

        return GetColor(directions[index]);
    }

    public static colorTypes getType(GameObject obj)
    {
        switch (obj.name)
        {
            case "Top": return colorTypes.Top;
            case "Bot": return colorTypes.Bot;
            case "Left": return colorTypes.Left;
            case "Right": return colorTypes.Right;
            case "Center": return colorTypes.Center;
        }

        return colorTypes.Invalid;
    }
}
