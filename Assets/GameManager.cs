using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public GameStage CurrentStage => _gameStage;

    public GameStage EditorDefaultStage = GameStage.Gameplay;
    public GameObject PlayerPrefab;
    public GameObject EndPrefab;

    [SerializeField] Vector2Int MazeSize;

    [SerializeField] private Color Top;
    [SerializeField] private Color Bot;
    [SerializeField] private Color Left;
    [SerializeField] private Color Right;
    [SerializeField] private Color EndGoal;

    private List<Color> Colors = new List<Color>();
    private int ColorIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Colors = new List<Color>() { Top, Right, Bot, Left };

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
            UpdateColor(distance, EndGoal);
        else
            UpdateColor(distance, GetColor(ColorIndex));
    }

    Color GetColor(int index)
    {
        if (index == -1)
            return Colors[Colors.Count - 1];
        return Colors[index % Colors.Count];
    }

    private void UpdateColor(float[] distance, Color main)
    {
        LightController.Instance.updateLeft(GetColor(ColorIndex + 1), distance[0]);
        LightController.Instance.updateMiddle(main, distance[1]);
        LightController.Instance.updateRight(GetColor(ColorIndex - 1), distance[2]);
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

    public void OnExitStage(GameStage oldGameStage, GameStage newGameStage)
    {
        switch (oldGameStage)
        {
            case GameStage.Gameplay:
                MazeGenerator.Instance.clear();
                break;

            case GameStage.EndGame:
                MenuManager.Instance.EndMenu.SetActive(false);
                break;
            case GameStage.Menu:
                break;
        }
    }

    public void OnEnterStage(GameStage newGameStage)
    {
        GameStateChangeEvent?.Invoke();

        switch (newGameStage)
        {
            case GameStage.Gameplay:
                initStage();
                break;

            case GameStage.EndGame:
                MenuManager.Instance.EndMenu.SetActive(true);
                break;
            case GameStage.Menu: 
                break;
        }
    }

    private void initStage()
    {
        MazeGenerator.Instance.clear();
        MazeGenerator.Instance.Generator(MazeSize);
        
        MazeNode node = MazeGenerator.Instance.RandomNode();

        Instantiate(PlayerPrefab, MazeGenerator.Instance.start.transform.position, Quaternion.identity, MazeGenerator.Instance.transform);
        Instantiate(EndPrefab, MazeGenerator.Instance.end.transform.position, Quaternion.identity, MazeGenerator.Instance.transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            initStage();
        } 
    }
}