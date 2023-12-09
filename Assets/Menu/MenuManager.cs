using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Menu
{
    Main,
    Setting,
    Color,
    Sound,
    End,
    Closed
}

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private GameObject EndMenu;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SettingMenu;
    [SerializeField] private GameObject colorMenu;


    private Menu _state;


    void Start()
    { 
        Instance = this;
    }

    public void toggle(Menu type, bool toggle)
    {
        get(type).SetActive(toggle);
    }

    public void open(Menu type)
    {
        Debug.Log("Open Menu " + type);

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

/*    public void OnMove(InputValue value)
    {
        if (!SettingMenu.activeSelf || !ColorMenu.Instance.isButtons)
            return;

        Vector2 dir = value.Get<Vector2>();
        colorTypes id = colorTypes.Invalid;

        if (dir.x == 1)
            id = colorTypes.Right;
        else if (dir.x == -1)
            id = colorTypes.Left;
        else if (dir.y == 1)
            id = colorTypes.Top;
        else if (dir.y == -1)
            id = colorTypes.Bot;

        ColorMenu.Instance.handleButton(id);
    }*/

    public void OnMenu()
    {
        if(SettingMenu.activeSelf)
            GameManager.Instance.ExitGameStage(GameStage.Menu);
        else
            GameManager.Instance.SetGameStage(GameStage.Menu);
    }
}
