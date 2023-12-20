using Maze;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variable;

public class FadeOutComponent : MonoBehaviour
{
    [SerializeField] private MazeVariable _mazeData;
    [SerializeField] private FadeDataSet _fadeData;
    [SerializeField] private StateDataSet _stateData;
    [SerializeField] private LightDataSet _lightData;

    [SerializeField] private SpriteRenderer _background;

    private GameObject __movePoint;
    private GameObject _movePoint {
        get
        {
            if(__movePoint == null)
            {
                __movePoint = GameObject.FindGameObjectWithTag("MovePoint");
            }
            return __movePoint;
        }
    }

    private void Awake()
    {
        _fadeData.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            getDistance();
        }

        _fadeData.renderList.ForEach(item => {
            if (item)
            {
                Color color = item.color;
                color.a = Mathf.Lerp(_fadeData.min, _fadeData.max, _fadeData.target);
                item.color = color;
            }            
        });
    }

    private void init()
    {
        List<Stage> LevelList = _stateData.LevelList;
        int Level = _stateData.Level;

        for(int i = 0; i < LevelList.Count; i++)
        {
            Stage stage = LevelList[i];
            if (i < Level)
            {
                Toggle(stage, false);
            }
            else if (i == Level)
            {
                SetFade(stage);
            }
            else
            {
                Toggle(stage, true);
            }
        }
    }

    private void Toggle(Stage stage, bool isShow = true)
    {
        Debug.Log(stage + " " + isShow);
        switch (stage)
        {
            case Stage.Player:
                ToggleSub(_mazeData.Player, isShow);
                break;
            case Stage.Goal:
                ToggleSub(_mazeData.EndGoal, isShow);
                break;
            case Stage.Wall:
                _background.sortingOrder = isShow ? 0 : 2;
                break;
            case Stage.SideLight:
                _lightData.Toggle(isShow, isShow);
                break;
        }
    }

    private void ToggleSub(GameObject obj, bool isShow = true)
    {
        obj.GetComponentInChildren<SpriteRenderer>().enabled = isShow;
    }

    private void SetFade(Stage stage)
    {
        switch (stage)
        {
            case Stage.Player:
                addSub(_mazeData.Player);
                break;
            case Stage.Goal:
                addSub(_mazeData.EndGoal);
                break;
            case Stage.Wall:
                add("Wall");
                _background.sortingOrder = 0;
                break;
            case Stage.SideLight:
                //TODO?
                break;
        }
    }

    private void addSub(GameObject obj)
    {
        _fadeData.renderList.Add(obj.GetComponentInChildren<SpriteRenderer>());
    }

    private void add(string tag)
    {
        GameObject[] arr = GameObject.FindGameObjectsWithTag(tag);

        foreach(GameObject item in arr)
        {
            SpriteRenderer renderer = item.GetComponent<SpriteRenderer>();
            if (renderer)
            {
                _fadeData.renderList.Add(renderer);
            }            
        }
    }

    private void Reset()
    {
        _fadeData.Reset();
    }

    private int getDistance()
    {
        Vector2 pos = _movePoint.transform.position;
        return _mazeData.get(pos).count;
    }

    public void OnNewMaze(object obj)
    {
        MazeEventType type = (MazeEventType)obj;
        switch (type)
        {
            case MazeEventType.Create:
                break;
            case MazeEventType.Start:
                Reset();
                init();
                break;
            case MazeEventType.Clear:
                Reset();
                break;
        }
    }

    public void OnMove(object obj)
    {
        _fadeData.target = (float) getDistance() / (_mazeData.max_length - 1);
    }
}
