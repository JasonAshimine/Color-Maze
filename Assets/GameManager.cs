using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;
using Maze;
using Variable;
using System;

public enum GameStage
{
    Invalid,
    MainMenu,
    Menu,
    LoadLevel,
    Gameplay,
    EndGame,
    Cancel
}

public enum Stage 
{
    Tutorial,
    Wall,
    Goal,
    Player,
    SideLight,
    HardMode
}


public class GameManager : MonoBehaviour
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
    private FadeDataSet _fadeData;

    [SerializeField]
    private StateDataSet _stateData;

    [SerializeField]
    private LayerMask _GoalLayer;

    public GameStage EditorDefaultStage = GameStage.Gameplay;
    public GameObject PlayerPrefab;
    public GameObject EndPrefab;

    [SerializeField] 
    private Vector2Int MazeSize;

    [SerializeField]
    private List<Stage> _levelList;

    // Start is called before the first frame update
    void Start()
    {
        GameStage InitialStage = GameStage.MainMenu;
#if UNITY_EDITOR
        InitialStage = EditorDefaultStage;
        //AudioSource audio = GameObject.FindAnyObjectByType<AudioSource>();
        //audio.enabled = false;
#endif

        _MazeData.MapSize = MazeSize;
        _stateData.Reset();
        _colorData.Reset();
        
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
        GameStage stage = _stateData.previous;
        _stateData.previous = _stateData.state;
        _stateData.state = stage;

        SetGameStage(stage);
    }

    public void SetGameStage(GameStage newGameStage)
    {
        OnExitStage(_stateData.previous);
        OnEnterStage(newGameStage);
    } 

    public void OnExitStage(GameStage oldGameStage)
    {
        Debug.Log("Exit Stage: " + oldGameStage);
        switch (oldGameStage)
        {
            case GameStage.MainMenu:
                _menuData.Raise(Menu.Main, false);
                ResumeGame();
                break;

            case GameStage.LoadLevel:
                break;
            case GameStage.Gameplay:
                break;

            case GameStage.EndGame:
                _stateData.Raise(GameStage.MainMenu);
                break;
            case GameStage.Menu:
                _menuData.Raise(Menu.Setting, false);
                ResumeGame();
                break;
        }
    }

    public void OnEnterStage(GameStage newGameStage)
    {
        Debug.Log("Enter Stage: " + newGameStage);

        switch (newGameStage)
        {
            case GameStage.MainMenu:
                CleanUpGameStage();
                _menuData.Raise(Menu.Main, true);
                PauseGame();
                break;

            case GameStage.LoadLevel:
                InitStage();
                _stateData.Raise(GameStage.Gameplay);
                break;

            case GameStage.Gameplay:                
                break;

            case GameStage.EndGame:
                EndGameStage();
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

        _MazeData.Player = Instantiate(PlayerPrefab, _MazeData.Start.transform.position, Quaternion.identity, gameObject.transform);
        _MazeData.EndGoal = Instantiate(EndPrefab, _MazeData.End.transform.position, Quaternion.identity, gameObject.transform);

        _MazeData.Raise(MazeEventType.Start);
    }

    #region
    private void EndGameStage()
    {
        if (_stateData.Level == _stateData.UnlockedLevel)
            _stateData.UnlockedLevel++;

        _stateData.Raise(GameStage.MainMenu);
    }

    private void CleanUpGameStage()
    {
        ClearStage();
        ClearLight();
    }

    private void ClearStage()
    {
        foreach (Transform node in transform)
            GameObject.Destroy(node.gameObject);

        _MazeData.Raise(MazeEventType.Clear);
        _fadeData.Reset();
    }

    private void ClearLight()
    {
        ColorIntensity color = _colorData.GetColorAll(_stateData.UnlockedLevel);
        _lightData.SetLights(_colorData.Default);
    }
    #endregion

    #region
    /// <summary>
    /// Pause Game via time scale
    /// </summary>

    private void PauseGame()
    {
        Time.timeScale = 0;
    }
    private void ResumeGame()
    {
        Time.timeScale = 1;
    }
    #endregion
    /// <summary>
    /// Debugging keys commands
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _stateData.state == GameStage.Gameplay)
        {
            InitStage();
        }

        #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Y))
            {
                _stateData.Raise(GameStage.EndGame);
            }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
        }
        #endif
    }
}