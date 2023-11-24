using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorMenu : Singleton<ColorMenu>
{
    public UnityEvent<colorTypes, Color> ColorMenuEvent;

    [SerializeField] private GameObject ColorPalette;
    [SerializeField] private GameObject Buttons;

    private colorTypes selected;

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
    }

 /*   private void OnEnable()
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
    }

    public void updateSideColor(colorTypes id, Color color)
    {
        if (id == colorTypes.Left)
            Side_Left.color = color;
        else if (id == colorTypes.Right)
            Side_Right.color = color;
    }


    public void updateColor(colorTypes id, Color color)
    {
        Image image = getImage(id);
        image.color = color;
    }

    public void handleButton(Image obj)
    {
        openColorPaletteMenu();

        selected = ColorManager.getType(obj.gameObject);
    }


    public void handleSideButton(GameObject obj)
    {
        colorTypes type = ColorManager.getType(obj);
        GameManager.Instance.toggleLight(type);

/*        switch (type)
        {
            case colorTypes.Left:
                GameManager.Instance.toggleLight(type);
                break;
            case colorTypes.Right:
                break;
        }*/

        
    }

    public void handleButton(colorTypes id)
    {
        handleButton(getImage(id));
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
