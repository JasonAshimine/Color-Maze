using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public enum GameStage
{
    Invalid,
    Menu,
    Gameplay,
    EndGame
}

public class GameManager : Singleton<GameManager>
{
    public static event System.Action GameStateChangeEvent;

    private GameStage _gameStage = GameStage.Invalid;
    private GameStage _previousStage = GameStage.Invalid;

    public GameStage CurrentStage => _gameStage;

    public GameStage EditorDefaultStage = GameStage.Gameplay;
    public GameObject PlayerPrefab;
    public GameObject EndPrefab;

    [SerializeField] Vector2Int MazeSize;

    private int ColorIndex = 0;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        GameStage InitialStage = GameStage.Menu;

        #if UNITY_EDITOR
                InitialStage = EditorDefaultStage;
        #endif

        SetGameStage(InitialStage);        
    }

    private void OnEnable()
    {
        movement.OnUpdate += handlePlayerMovement;
    }

    private void OnDisable()
    {
        movement.OnUpdate -= handlePlayerMovement;
    }

    private void handlePlayerMovement(float rotation, float[] distance, GameObject FirstHit)
    {
        ColorIndex = (int)rotation / 90;

        if (FirstHit.tag == "Finish")
            RenderColor(distance, ColorManager.Instance.GetColor(colorTypes.Center));
        else
            RenderColor(distance, ColorManager.Instance.GetColor(ColorIndex));
    }


    public void RenderColor()
    {
        Player?.GetComponent<movement>().trigger();
    }

    private void RenderColor(float[] distance, Color forwardColor)
    {
        LightController.Instance.updateMiddle(forwardColor, distance[1]);

        Color left = ColorManager.Instance.GetColor(ColorIndex + 1);
        Color right = ColorManager.Instance.GetColor(ColorIndex - 1);

        LightController.Instance.updateLeft(left, distance[0]);
        LightController.Instance.updateRight(right, distance[2]);
    }

    public void SetGameStage(GameStage newGameStage)
    {
        if (newGameStage != _gameStage)
        {
            OnExitStage(_gameStage, newGameStage);
            OnEnterStage(newGameStage);
            _gameStage = newGameStage;
        }
    } 

    public void ExitGameStage(GameStage currentStage)
    {
        if(currentStage == _gameStage)
        {
            switch (_gameStage)
            {
                case GameStage.Menu:
                    SetGameStage(_previousStage);
                    break;
            }
        }        
    }

    public void OnExitStage(GameStage oldGameStage, GameStage newGameStage)
    {
        switch (oldGameStage)
        {
            case GameStage.Gameplay:
                
                break;

            case GameStage.EndGame:
                MenuManager.Instance.close(Menu.End);
                break;
            case GameStage.Menu:
                MenuManager.Instance.close(Menu.Setting);
                ResumeGame();
                break;
        }
    }

    public void OnEnterStage(GameStage newGameStage)
    {
        Debug.Log(newGameStage);

        GameStateChangeEvent?.Invoke();

        switch (newGameStage)
        {
            case GameStage.Gameplay:
                initStage();
                break;

            case GameStage.EndGame:
                MazeGenerator.Instance.clear();
                MenuManager.Instance.open(Menu.End);
                break;
            case GameStage.Menu:
                MenuManager.Instance.open(Menu.Setting);
                PauseGame();
                break;
        }
    }

    private void initStage()
    {
        MazeGenerator.Instance.clear();
        MazeGenerator.Instance.Generator(MazeSize);

        Player = Instantiate(PlayerPrefab, MazeGenerator.Instance.start.transform.position, Quaternion.identity, MazeGenerator.Instance.transform);
        Instantiate(EndPrefab, MazeGenerator.Instance.end.transform.position, Quaternion.identity, MazeGenerator.Instance.transform);
    }

    public Light2D getLight(colorTypes id)
    {
        switch (id)
        {
            case colorTypes.Left: return LightController.Instance.Left;
            case colorTypes.Right: return LightController.Instance.Right;
            case colorTypes.Center: return LightController.Instance.Middle;
        }
        return null;
    }

    public bool toggleLight(colorTypes id)
    {
        GameObject obj = getLight(id)?.gameObject;

        if (obj == null)
            return true;

        obj.SetActive(!obj.activeSelf);
        return obj.activeSelf;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            initStage();
        } 
    }
}