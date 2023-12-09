using System;
using UnityEngine;
using Game.Events;
using System.Collections.Generic;

namespace Variable
{
    [CreateAssetMenu(fileName = "Maze", menuName = "Variable/Maze")]
    public class MazeVariable : ScriptableObject
    {
        public Vector2Int MapSize;
        public GameEventData MazeEvent;

        public MazeNode Start { get; set; }
        public MazeNode End { get; set; }
        public List<MazeNode> nodes { get; set; }

        public void clear()
        {
            if (nodes.Count <= 0)
                return;

            foreach (MazeNode node in nodes)
                GameObject.Destroy(node.gameObject);

            nodes.Clear();
        }

        public void Raise(Maze.MazeEventType data)
        {
            Debug.Log(data);
            MazeEvent.Raise(data);
        }
    }
}