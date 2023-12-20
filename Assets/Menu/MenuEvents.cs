using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Variable;
public class MenuEvents : MonoBehaviour
{
    [SerializeField]
    private StateDataSet _stateData;

    [SerializeField]
    private List<GameObject> _buttons = new List<GameObject>();

    [SerializeField]
    private GameObject _hardModeButton;

    [SerializeField]
    private ColorDirection _colorData;

    [SerializeField]
    private LightDataSet _lightData;

    private void Start()
    {
        SetUpButtons();
    }

    private void OnEnable()
    {
        SetUpButtons();
    }

    private void SetUpButtons()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].GetComponent<Button>().interactable = _stateData.UnlockedLevel >= i;
            _buttons[i].GetComponent<Image>().color = _colorData.GetColorAll(i).color;
        }

        _hardModeButton.SetActive(_stateData.UnlockedLevel >= _buttons.Count);
    }

    public void OnClick(int Level)
    {
        _stateData.Level = Level;
        _stateData.Raise(GameStage.LoadLevel);
    }


    public void HandleHover(object Obj)
    {
        HoverData data = (HoverData)Obj;

        if (data.toggle)
        {
            ColorIntensity color = _colorData.GetColorAll(data.id);

            _lightData.SetLights(color, _colorData.Center, color);
        }
        else
        {
            _lightData.SetLights(_colorData.Default);
        }
        
    }
}
