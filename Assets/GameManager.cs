using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    private List<Color> Colors = new List<Color>();

    [SerializeField] private int ColorIndex = 0;
    [SerializeField] private float min = 0.5f;

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

    private void handlePlayerMovement(float rotation, float[] distance)
    {
        ColorIndex = (int)rotation / 90;
        updateColor(distance);
    }

    Color getColor(int index)
    {
        if (index == -1)
            return Colors[Colors.Count - 1];
        return Colors[index % Colors.Count];
    }

    private void updateColor(float[] distance)
    {
        //Camera.main.backgroundColor = ColorDirection * (distance / 10 * (1-min) + min);

        //LightController.Instance.Middle.color = ColorDirection;
        Debug.Log(String.Join(", ",distance));
        LightController.Instance.Left.color = getColor(ColorIndex+1) * CalcColorDistance(distance[0]);
        LightController.Instance.Middle.color = getColor(ColorIndex) * CalcColorDistance(distance[1]);
        LightController.Instance.Right.color = getColor(ColorIndex-1) * CalcColorDistance(distance[2]);
    }

    private float CalcColorDistance(float distance) {
        return ((1 - Mathf.Pow(0.7f, distance)) * (1 - min)) + min;
        
        //return (distance / 10 * (1 - min)) + min; 
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
                //MazeGenerator.Instance.clear();
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
        }
    }

    private RaycastHit2D RayCast()
    {
        return Physics2D.Raycast(transform.position, transform.up);
    }

    private Collider2D CheckForward()
    {
        return Physics2D.Raycast(transform.position, transform.up).collider;
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
