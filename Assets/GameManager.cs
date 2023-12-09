using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;
using Maze;
using Variable;

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

    [SerializeField]
    private Variable.MazeVariable _MazeData;

    [SerializeField]
    private ColorDirection _colorData;

    [SerializeField]
    private GameEventData _lightEvent;

    [SerializeField]
    private LayerMask _GoalLayer;

    private GameStage _gameStage = GameStage.Invalid;
    private GameStage _previousStage = GameStage.Invalid;

    public GameStage CurrentStage => _gameStage;

    public GameStage EditorDefaultStage = GameStage.Gameplay;
    public GameObject PlayerPrefab;
    public GameObject EndPrefab;

    [SerializeField] Vector2Int MazeSize;

    private GameObject Player;
    private GameObject EndGoal;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        _MazeData.MapSize = MazeSize;

        GameStage InitialStage = GameStage.Gameplay;

        #if UNITY_EDITOR
                InitialStage = EditorDefaultStage;
        #endif

        SetGameStage(InitialStage);        
    }

    public void handlePlayerMovement(object data)
    {
        Direction dir = (Direction)data;
        int index = (int) (dir.direction / 90);

        ColorIntensity Left     = GetColor(dir.left, index + 1);
        ColorIntensity Middle   = GetColor(dir.forward, index);
        ColorIntensity Right    = GetColor(dir.right, index - 1);

        _lightEvent.Raise(new LightEventData(Left, Middle, Right));
    }

    private ColorIntensity GetColor(RaycastHit2D hit, int index)
    {
        ColorIntensity color =  hit.collider.gameObject.layer == EndPrefab.layer
            ? _colorData.Center : _colorData.GetColor(index);

        color.intensity = hit.distance;
        return color;
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
                InitStage();
                break;

            case GameStage.EndGame:
                ClearStage();
                MenuManager.Instance.open(Menu.End);
                break;
            case GameStage.Menu:
                MenuManager.Instance.open(Menu.Setting);
                PauseGame();
                break;
        }
    }

    private void InitStage()
    {
        ClearStage();
        _MazeData.Raise(MazeEventType.Create);
        
        Player = Instantiate(PlayerPrefab, _MazeData.Start.transform.position, Quaternion.identity, gameObject.transform);
        EndGoal = Instantiate(EndPrefab, _MazeData.End.transform.position, Quaternion.identity, gameObject.transform);
    }

    private void ClearStage()
    {
        _MazeData.Raise(MazeEventType.Clear);

        foreach (Transform node in transform)
            GameObject.Destroy(node.gameObject);
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
            InitStage();
        }

/*        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SpriteRenderer renderer = LightController.Instance.GetComponent<SpriteRenderer>();

            if (renderer.sortingOrder == 0)
                renderer.sortingOrder = 1;
            else
                renderer.sortingOrder = 0;
        }*/
    }
}