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

        public GameObject Player;
        public GameObject EndGoal;

        public MazeNode Start { get; set; }
        public MazeNode End { get; set; }
        public List<MazeNode> nodes;

        public int max_length;

        public ContactFilter2D wallFilter;
        public ContactFilter2D floorFilter;
        private void Clear()
        {
            if (nodes == null || nodes.Count <= 0)
                return;

            foreach (MazeNode node in nodes)
            {
                if(node != null)
                    GameObject.Destroy(node.gameObject);
            }               

            nodes.Clear();
        }


        public MazeNode get(Vector2 position)
        {
            Vector2Int pos = Vector2Int.RoundToInt(position);
            int x = pos.x + MapSize.x / 2;
            int y = pos.y + MapSize.y / 2;   

            int index = y + x * MapSize.y;

            return nodes[index];
        }

        public void Raise(Maze.MazeEventType data)
        {
            MazeEvent.Raise(data);
        }

        public void Reset()
        {
            Clear();
            max_length = 0;
            Start = null;
            End = null;

            if (Player != null)
                GameObject.Destroy(Player);

            if (EndGoal != null)
                GameObject.Destroy(EndGoal);
        }
    }
}