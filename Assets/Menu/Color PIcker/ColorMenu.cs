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
    [SerializeField] private LightDataSet _lightData;

    [SerializeField] private GameObject ColorPalette;
    [SerializeField] private GameObject Buttons;

    [Space()]
    [SerializeField] private Image Top;
    [SerializeField] private Image Bot;
    [SerializeField] private Image Left;
    [SerializeField] private Image Right;
    [SerializeField] private Image Center;

    [Space()]
    [SerializeField] private Image Side_Left;
    [SerializeField] private Image Side_Right;
    [SerializeField, Range(0.0f, 1f)] 
    private float _disabledIntensity = 0.5f;

    private ColorIntensity selected;

    public bool isButtons => Buttons.activeSelf;
    public bool isColorPalette => ColorPalette.activeSelf;

    private void Start()
    {
        Instance = this;
        UpdateColor();
    }

    private void OnEnable()
    {
        UpdateSideColor();
    }

    public Image GetImage(colorTypes id)
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

    public void UpdateColor(Color color)
    {
        if (color == Color.clear)
            color = Color.white;

        ColorMenuEvent.Invoke(selected, color);

        
        selected.color = color;
        UpdateColor();
        _lightData.Raise(LightEventType.Color);
    }
    public void UpdateColor()
    {
        Top.color = _colorData.Top.color;
        Bot.color = _colorData.Bot.color;
        Left.color = _colorData.Left.color;
        Right.color = _colorData.Right.color;
        UpdateSideColor();
    }

    public void UpdateSideColor()
    {       
        Side_Left.color = CalcSideColor(_lightData.Left, _lightData.toggleLeft);
        Side_Right.color = CalcSideColor(_lightData.Right, _lightData.toggleRight);
    }

    private Color CalcSideColor(ColorIntensity color, bool isOn)
    {
        float intestity = isOn ? 1f : _disabledIntensity;
        return color.color * intestity;
    }

    public void HandleLightEvent(object data)
    {
        switch ((LightEventType)data)
        {
            case LightEventType.Toggle:
                UpdateSideColor();
                break;
        }
    }    

    public void HandleButton(Image obj)
    {
        OpenColorPaletteMenu();
        selected = GetColor(obj.gameObject.name);
    }

    public void HandleSideButton(GameObject obj)
    {
        switch (obj.name)
        {
            case "Left":
                _lightData.toggleLeft = !_lightData.toggleLeft;
                break;
            case "Right":
                _lightData.toggleRight = !_lightData.toggleRight;
                break;
        }
        _lightData.Raise(LightEventType.Toggle);
    }


    public ColorIntensity GetColor(string name)
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


    public void OpenMenu()
    {
        ColorPalette.SetActive(false);
        Buttons.SetActive(true);
    }

    public void OpenColorPaletteMenu()
    {
        ColorPalette.SetActive(true);
        Buttons.SetActive(false);
    }


    public void HandleColorSelection(Color color)
    {
        OpenMenu();

        UpdateColor(color);
    }
}
