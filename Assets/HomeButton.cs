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


    private void OnEnable()
    {
        ChangeColor();
    }

    public void ChangeColor(object obj = null)
    {
        Color color = _colorData.GetColorAll(_stateData.Level).color;
        _button.GetComponent<Image>().color = color;
    }


    public void OnClick()
    {
        _stateData.Raise(_target);
    }
}
