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
    EndGame,
    Cancel
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private MazeVariable _MazeData;

    [SerializeField]
    private ColorDirection _colorData;

    [SerializeField]
    private LightDataSet _lightData;

    [SerializeField]
    private MenuDataSet _menuData;

    [SerializeField]
    private StateDataSet _stateData;

    [SerializeField]
    private LayerMask _GoalLayer;

    //private GameStage _gameStage = GameStage.Invalid;
    //private GameStage _previousStage = GameStage.Invalid;

    //public GameStage CurrentStage => _gameStage;

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
        _stateData.Reset();
        _colorData.Reset();

        GameStage InitialStage = GameStage.Gameplay;

        #if UNITY_EDITOR
                InitialStage = EditorDefaultStage;
        #endif

        _stateData.Raise(InitialStage);     
    }


    public void handlePlayerMovement(object data)
    {
        Direction dir = (Direction)data;
        int index = (int) (dir.direction / 90);

        _lightData.Left     = GetColor(dir.left, index + 1);
        _lightData.Middle   = GetColor(dir.forward, index);
        _lightData.Right    = GetColor(dir.right, index - 1);

        _lightData.Raise(LightEventType.Color);

        //Debug.Log(dir.forward.collider.name + " " + Middle);
    }

    private ColorIntensity GetColor(RaycastHit2D hit, int index)
    {
        ColorIntensity color =  hit.collider.gameObject.tag == EndPrefab.tag
            ? _colorData.Center : _colorData.GetColor(index);


        color.intensity = hit.distance;
        return color;
    }

    public void handleStateChange(object _data)
    {
        if(_stateData.isCancel)
        {
            CancelGameStage();
        }
        else
        {
            SetGameStage(_stateData.state);
        }        
    }

    public void CancelGameStage()
    {
        OnExitStage(_stateData.state);
        GameStage state = _stateData.state;
        _stateData.state = _stateData.previous;
        _stateData.previous = state;
    }

    public void SetGameStage(GameStage newGameStage)
    {
        if (newGameStage != _stateData.previous)
        {
            OnExitStage(_stateData.previous);
            OnEnterStage(newGameStage);
        }
    } 

    public void OnExitStage(GameStage oldGameStage)
    {
        //Debug.Log("Exit Stage: " + oldGameStage);
        switch (oldGameStage)
        {
            case GameStage.Gameplay:
                break;

            case GameStage.EndGame:
                _menuData.Raise(Menu.End, false);
                break;
            case GameStage.Menu:
                _menuData.Raise(Menu.Setting, false);
                ResumeGame();
                break;
        }
    }

    public void OnEnterStage(GameStage newGameStage)
    {
        //Debug.Log("Enter Stage: " + newGameStage);

        switch (newGameStage)
        {
            case GameStage.Gameplay:
                InitStage();
                break;

            case GameStage.EndGame:
                ClearStage();
                _menuData.Raise(Menu.End, true);
                break;
            case GameStage.Menu:
                _menuData.Raise(Menu.Setting, true);
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