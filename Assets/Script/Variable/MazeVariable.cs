using System;
using UnityEngine;
using Game.Events;

namespace Variable
{
    [CreateAssetMenu(fileName = "Maze", menuName = "Variable/Maze")]
    public class MazeVariable : ScriptableObject
    {
        public Vector2Int MapSize;
        public GameEventData MazeEvent;

        public MazeNode Start { get; set; }
        public MazeNode End { get; set; }

        public void Raise(Maze.MazeEventType data)
        {
            Debug.Log(data);
            MazeEvent.Raise(data);
        }
    }
}