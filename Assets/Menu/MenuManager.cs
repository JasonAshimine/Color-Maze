using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variable;

public enum Menu
{
    Main,
    Setting,
    Color,
    Sound,
    End,
    Closed
}

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MenuDataSet _menuData;
    [SerializeField] private StateDataSet _stateData;

    [SerializeField] private GameObject EndMenu;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SettingMenu;
    [SerializeField] private GameObject colorMenu;


    private void Start()
    {
        _menuData.Reset();
    }

    private void toggle(Menu type, bool toggle)
    {
        get(type).SetActive(toggle);
    }

    public void handleMenuEvent(object data)
    {
        //Debug.Log("MenuToggle " + _menuData.state + _menuData.toggle);
        if (_menuData.toggle)
        {
            open(_menuData.state);
        }
        else
        {
            close(_menuData.state);
        }        
    }


    private void open(Menu type)
    {
        switch (type)
        {
            case Menu.End:
                EndMenu.SetActive(true);
                break;

            case Menu.Main:
                MainMenu.SetActive(true);
                break;
            case Menu.Setting:
                SettingMenu.SetActive(true);
                colorMenu.GetComponent<ColorMenu>().OpenMenu();
                break;
        }
    }


    private void close(Menu type)
    {
        switch (type)
        {
            case Menu.End:
                EndMenu.SetActive(false);
                break;

            case Menu.Main:
                MainMenu.SetActive(false);
                break;
            case Menu.Setting:
                SettingMenu.SetActive(false);
                break;
        }
    }

    private GameObject get(Menu type)
    {
        switch (type)
        {
            case Menu.End: return EndMenu;
            case Menu.Main: return MainMenu;
            case Menu.Setting: return SettingMenu;
        }

        return null;
    }

    /// <summary>
    /// OnMenu called from PlayerInput component
    /// </summary>
    public void OnMenu()
    {
        if (SettingMenu.activeSelf)
        {
            _stateData.Cancel(GameStage.Menu);
        }
        else
        {
            _stateData.Raise(GameStage.Menu);
        }
        
    }
}
