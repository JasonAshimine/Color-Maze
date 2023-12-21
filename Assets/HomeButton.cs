using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Variable;

public class HomeButton : MonoBehaviour
{
    [SerializeField] private ColorDirection _colorData;
    [SerializeField] private StateDataSet _stateData;

    [SerializeField] private GameObject _button;
    [SerializeField] private GameStage _target = GameStage.MainMenu;

    private Image _buttonImage;
    private int _level => _stateData.previous == GameStage.MainMenu ? _stateData.UnlockedLevel : _stateData.Level;

    private void Awake()
    {
        _buttonImage = _button.GetComponent<Image>();
    }

    private void OnEnable()
    {
        ChangeColor();
        changeImage();
    }


    private void changeImage()
    {
        _buttonImage.sprite = _stateData.LevelList[_level].Sprite;
    }

    public void ChangeColor(object obj = null)
    {
        Color color = _colorData.GetColorAll(_level).color;
        _buttonImage.color = color;
    }


    public void OnClick()
    {
        _stateData.Raise(_target);
    }
}
