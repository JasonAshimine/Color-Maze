using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Menu
{
    Main,
    Setting,
    Color,
    Sound,
    End
}

public class MenuManager : Singleton<MenuManager>
{
    public GameObject EndMenu;
    public GameObject MainMenu;
    public GameObject SettingMenu;
    public GameObject colorMenu;


    void Start()
    {
        Instance = this;
    }

    public void toggle(Menu type, bool toggle)
    {
        get(type).SetActive(toggle);
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
        if(gameObject.activeSelf)
            GameManager.Instance.ExitGameStage(GameStage.Menu);
        else
            GameManager.Instance.SetGameStage(GameStage.Menu);
    }
}
