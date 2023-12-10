using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MazeNode : MonoBehaviour
{
    public enum NodeState
    {
        Available,
        Current,
        Completed
    }

    public enum Walls
    {
        Top,
        Bot,
        Left,
        Right
    }


    [SerializeField] private GameObject[] walls;
    [SerializeField] private SpriteRenderer floor;

    public void SetState(NodeState state){
        switch (state)
        {
            case NodeState.Available:
                floor.color = Color.white;
                break;
            case NodeState.Current:
                floor.color = Color.yellow;
                break;
            case NodeState.Completed: 
                floor.color = Color.black; 
                break;
        }
    }

    public void RemoveWall(Walls wall, bool state = false)
    {
        switch (wall)
        {
            case Walls.Top: 
                walls[0].SetActive(state); 
                break;
            case Walls.Bot:
                walls[1].SetActive(state);
                break;
            case Walls.Left:
                walls[2].SetActive(state);
                break;
            case Walls.Right:
                walls[3].SetActive(state);
                break;
        }
    }

    public void RemoveWallInverse(Walls wall, bool state = false)
    {
        switch (wall)
        {
            case Walls.Top:
                walls[1].SetActive(state);
                break;
            case Walls.Bot:
                walls[0].SetActive(state);
                break;
            case Walls.Left:
                walls[3].SetActive(state);
                break;
            case Walls.Right:
                walls[2].SetActive(state);
                break;
        }
    }
}
