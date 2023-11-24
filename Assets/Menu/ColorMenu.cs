using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorMenu : MonoBehaviour
{
    public enum state
    {
        Top, Bot, Left, Right, Center, Buttons, Invalid
    }

    public UnityEvent<Color, state> ColorPickerEvent;

    [SerializeField] private GameObject ColorPalette;
    [SerializeField] private GameObject Buttons;

    public Dictionary<state, Color> ColorList = new Dictionary<state, Color>() {
        { state.Top,    Color.white },
        { state.Bot,    Color.white },
        { state.Left,   Color.white },
        { state.Right,  Color.white },
        { state.Center, Color.white }
    };

    [SerializeField] private state selected;
    [SerializeField] private Image selectedImage;


    public void updateColor(Color color)
    {
        if (color == Color.clear)
            color = Color.white;

        ColorList[selected] = color;
        selectedImage.color = color;

        Debug.Log(color);

        ColorPickerEvent.Invoke(color, selected);
    }

    private state getType(GameObject obj)
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

    public void handleButton(Image obj)
    {
        ColorPalette.SetActive(true);
        Buttons.SetActive(false);

        selectedImage = obj;
        selected = getType(obj.gameObject);
    }

    public void handleColorSelection(Color color)
    {
        ColorPalette.SetActive(false);
        Buttons.SetActive(true);

        updateColor(color);
    }
}
