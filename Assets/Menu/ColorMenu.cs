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

    private Image selectedImage;

    [SerializeField] private Image Top;
    [SerializeField] private Image Bot;
    [SerializeField] private Image Left;
    [SerializeField] private Image Right;
    [SerializeField] private Image Center;

    private Dictionary<colorTypes, Image> ImageList;

    public bool isButtons => Buttons.activeSelf;
    public bool isColorPalette => ColorPalette.activeSelf;

    private void Start()
    {
        Instance = this;

        ImageList = new Dictionary<colorTypes, Image>() {
            { colorTypes.Top,    Top },
            { colorTypes.Bot,    Bot },
            { colorTypes.Left,   Left },
            { colorTypes.Right,  Right },
            { colorTypes.Center, Center }
        };
    }

    public void updateColor(Color color)
    {
        if (color == Color.clear)
            color = Color.white;

        ColorMenuEvent.Invoke(selected, color);
    }

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


    public void updateColor(colorTypes id, Color color)
    {
        Image image = getImage(id);
        image.color = color;
    }

    public void handleButton(Image obj)
    {
        ColorPalette.SetActive(true);
        Buttons.SetActive(false);

        selectedImage = obj;
        selected = ColorManager.getType(obj.gameObject);
    }

    public void handleButton(colorTypes id)
    {
        handleButton(getImage(id));
    }


    public void handleColorSelection(Color color)
    {
        ColorPalette.SetActive(false);
        Buttons.SetActive(true);

        updateColor(color);
    }
}
