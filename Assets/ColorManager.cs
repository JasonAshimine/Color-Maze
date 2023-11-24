using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorManager : Singleton<ColorManager>
{
    public enum state
    {
        Top, Bot, Left, Right, Center, Invalid
    }

    public UnityEvent<Color, state> ColorPickerEvent;

    public Dictionary<state, Color> ColorList = new Dictionary<state, Color>() {
        { state.Top,    Color.white },
        { state.Bot,    Color.white },
        { state.Left,   Color.white },
        { state.Right,  Color.white },
        { state.Center, Color.white }
    };

    private state[] directions = new state[] { state.Top, state.Right, state.Bot, state.Left };

    private void Start()
    {
        Instance = this;
    }

    public void UpdateColor(state id, Color color)
    {
        ColorList[id] = color;
        ColorPickerEvent.Invoke(color, id);
    }

    public Color GetColor(state id) => ColorList[id];

    public Color GetColor(int index)
    {
        if (index < 0)
            index = directions.Length - 1;
        else
            index %= directions.Length;

        return GetColor(directions[index]);
    }

    public state getType(GameObject obj)
    {
        switch (obj.name)
        {
            case "Top": return state.Top;
            case "Bot": return state.Bot;
            case "Left": return state.Left;
            case "Right": return state.Right;
            case "Center": return state.Center;
        }

        return state.Invalid;
    }
}
