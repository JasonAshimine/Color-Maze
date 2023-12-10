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

    public void toggle(Menu type, bool toggle)
    {
        get(type).SetActive(toggle);
    }

    public void handleMenuEvent(object data)
    {
        if (_menuData.toggle)
        {
            open(_menuData.state);
        }
        else
        {
            close(_menuData.state);
        }        
    }


    public void open(Menu type)
    {
        switch (type)
        {
            case Menu.End:
                EndMenu.SetActive(true);
                break;

            case Menu.Main:
                break;
            case Menu.Setting:
                SettingMenu.SetActive(true);
                ColorMenu.Instance.OpenMenu();
                break;
        }
    }


    public void close(Menu type)
    {
        switch (type)
        {
            case Menu.End:
                EndMenu.SetActive(false);
                break;

            case Menu.Main:
                break;
            case Menu.Setting:
                SettingMenu.SetActive(false);
                break;
        }
    }

    public GameObject get(Menu type)
    {
        switch (type)
        {
            case Menu.End: return EndMenu;
            case Menu.Main: return MainMenu;
            case Menu.Setting: return SettingMenu;
        }

        return null;
    }

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
