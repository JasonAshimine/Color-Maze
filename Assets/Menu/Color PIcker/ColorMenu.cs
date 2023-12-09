using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Variable;

public class ColorMenu : Singleton<ColorMenu>
{
    public UnityEvent<ColorIntensity, Color> ColorMenuEvent;
    [SerializeField] private ColorDirection _colorData;


    [SerializeField] private GameObject ColorPalette;
    [SerializeField] private GameObject Buttons;

    private ColorIntensity selected;

    [SerializeField] private Image Top;
    [SerializeField] private Image Bot;
    [SerializeField] private Image Left;
    [SerializeField] private Image Right;
    [SerializeField] private Image Center;

    [SerializeField] private Image Side_Left;
    [SerializeField] private Image Side_Right;

    public bool isButtons => Buttons.activeSelf;
    public bool isColorPalette => ColorPalette.activeSelf;

    private void Start()
    {
        Instance = this;
        updateColor();
    }

/*    private void OnEnable()
    {
        Side_Left.color = LightController.Instance.Left.color;
        Side_Right.color = LightController.Instance.Right.color;
    }*/

    public Image getImage(colorTypes id)
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

    public void updateColor(Color color)
    {
        if (color == Color.clear)
            color = Color.white;

        ColorMenuEvent.Invoke(selected, color);

        selected.color = color;
        updateColor();
    }

    public void updateSideColor(colorTypes id, Color color)
    {
        if (id == colorTypes.Left)
            Side_Left.color = color;
        else if (id == colorTypes.Right)
            Side_Right.color = color;
    }


    public void updateColor()
    {
        Top.color = _colorData.Top.color;
        Bot.color = _colorData.Bot.color;
        Left.color = _colorData.Left.color;
        Right.color = _colorData.Right.color;
    }

    public void handleButton(Image obj)
    {
        openColorPaletteMenu();

        selected = getColor(obj.gameObject.name);
    }

    public void handleSideButton(GameObject obj)
    {
       // colorTypes type = ColorManager.getType(obj);
        //GameManager.Instance.toggleLight(type);

/*        switch (type)
        {
            case colorTypes.Left:
                GameManager.Instance.toggleLight(type);
                break;
            case colorTypes.Right:
                break;
        }*/
    }


    public ColorIntensity getColor(string name)
    {
        switch (name)
        {
            case "Top": return _colorData.Top;
            case "Bot": return _colorData.Bot;
            case "Left": return _colorData.Left;
            case "Right": return _colorData.Right;
            case "Center": return _colorData.Center;
        }

        return null;
    }


    public void openMenu()
    {
        ColorPalette.SetActive(false);
        Buttons.SetActive(true);
    }

    public void openColorPaletteMenu()
    {
        ColorPalette.SetActive(true);
        Buttons.SetActive(false);
    }


    public void handleColorSelection(Color color)
    {
        openMenu();

        updateColor(color);
    }
}
