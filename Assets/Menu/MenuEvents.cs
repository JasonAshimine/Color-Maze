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

    [SerializeField]
    private SoundDataSet _soundData;

    [SerializeField]
    private float _pitchHoverValue = 1.05f;

    [SerializeField]
    private float _pitchHardModeHoverValue = 0.95f;

    private void Start()
    {
        SetUpButtonImage();
        SetUpButtons();
    }

    private void OnEnable()
    {
        SetUpButtons();
    }

    private void SetUpButtonImage()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].GetComponent<Image>().sprite = _stateData.LevelList[i].Sprite;
        }

        LevelData HardModeData = _stateData.LevelList.Find(i => i.Stage == Stage.HardMode);

        if(HardModeData != null)
        {
            _hardModeButton.GetComponent<Image>().sprite = HardModeData.Sprite;
        }        
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
            HandleEnter(data);
        }
        else
        {
            HandleExit(data);
        }
        
    }

    private void HandleEnter(HoverData data)
    {
        bool isHardLevel = _stateData.LevelList[data.id].Stage == Stage.HardMode;

        if(isHardLevel)
        {
            _soundData.SetMusicPitch(_pitchHardModeHoverValue);
            _lightData.Toggle(false, false);
        }
        else
        {
            _soundData.SetMusicPitch(_pitchHoverValue);
            ColorIntensity color = _colorData.GetColorAll(data.id);
            _lightData.SetLights(color, _colorData.Center, color);
        }        
    }

    private void HandleExit(HoverData data)
    {
        _soundData.SetMusicPitch(1f);
        _lightData.SetLights(_colorData.Default);

        if (_stateData.LevelList[data.id].Stage == Stage.HardMode)
        {
            _lightData.Toggle(true, true);
        }
    }
}
