using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variable;

namespace Maze
{
    public enum MazeEventType
    {
        Clear,
        Create
    }
    public class MazeGenerator : MonoBehaviour
    {
        [SerializeField] private MazeNode nodePrefab;
        [SerializeField] private MazeVariable _MazeData;

        private List<int> _currentPath;
        private List<int> _completedNodes;

        private int count = 0;

        public void Start()
        {
            _MazeData.nodes.Clear();
            _MazeData.Start = null;
            _MazeData.End = null;
        }

        public void handleEvent(object data)
        {
            switch ((MazeEventType) data)
            {
                case MazeEventType.Create:
                    Generator(_MazeData.MapSize);
                    break;
                case MazeEventType.Clear:
                    _MazeData.clear();
                    break;
            }
        }


        private void markAsCurrent(int index)
        {
            _currentPath.Add(index);
            _MazeData.nodes[index].SetState(MazeNode.NodeState.Current);
        }

        private void markAsComplete(int index)
        {
            updateCount();
            _completedNodes.Add(index);
            _MazeData.nodes[index].SetState(MazeNode.NodeState.Completed);
            _currentPath.RemoveAt(_currentPath.Count - 1);
        }

        private void updateCount()
        {
            if (_currentPath.Count > count)
            {
                count = _currentPath.Count;
                _MazeData.End = _MazeData.nodes[lastCurrent];
            }
        }

        private int lastCurrent { get => _currentPath[_currentPath.Count - 1]; }

        public MazeNode RandomNode()
        {
            return _MazeData.nodes[Random.Range(0, _MazeData.nodes.Count)];
        }

        public void Generator(Vector2Int size)
        {
            _MazeData.nodes = generateNodeList(size);
            _currentPath = new List<int>();
            _completedNodes = new List<int>();

            markAsCurrent(Random.Range(0, _MazeData.nodes.Count));

            count = 0;
            _MazeData.Start = _MazeData.nodes[lastCurrent];


            while (_completedNodes.Count < _MazeData.nodes.Count)
            {
                List<int> possibleNextNodes = new List<int>();
                List<MazeNode.Walls> possibleDirections = new List<MazeNode.Walls>();

                int currentNodeIndex = lastCurrent;

                Vector2Int pos = new Vector2Int(currentNodeIndex / size.y, currentNodeIndex % size.y);


                void checkNode(bool wallCheck, int index, MazeNode.Walls side)
                {
                    if (wallCheck && !_completedNodes.Contains(index) && !_currentPath.Contains(index))
                    {
                        possibleDirections.Add(side);
                        possibleNextNodes.Add(index);
                    }
                }

                // Check node to the right of the current node
                checkNode(pos.x < size.x - 1, currentNodeIndex + size.y, MazeNode.Walls.Right);
                // Check node to the left of the current node
                checkNode(pos.x > 0, currentNodeIndex - size.y, MazeNode.Walls.Left);
                // Check node above the current node
                checkNode(pos.y < size.y - 1, currentNodeIndex + 1, MazeNode.Walls.Top);
                // Check node below the current node
                checkNode(pos.y > 0, currentNodeIndex - 1, MazeNode.Walls.Bot);


                if (possibleDirections.Count > 0)
                {
                    int chosenDirection = Random.Range(0, possibleDirections.Count);
                    currentNodeIndex = possibleNextNodes[chosenDirection];

                    MazeNode.Walls wallSide = possibleDirections[chosenDirection];

                    _MazeData.nodes[currentNodeIndex].RemoveWallInverse(wallSide);
                    _MazeData.nodes[_currentPath[_currentPath.Count - 1]].RemoveWall(wallSide);

                    markAsCurrent(currentNodeIndex);
                }
                else
                {
                    markAsComplete(currentNodeIndex);
                }
            }

        }

        private List<MazeNode> generateNodeList(Vector2Int size)
        {
            List<MazeNode> nodes = new List<MazeNode>();

            Vector3 dim = nodePrefab.transform.localScale;

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {

                    Vector3 nodePos = new Vector3(x - (size.x / 2), y - (size.y / 2), 0);
                    MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                    nodes.Add(newNode);
                }
            }

            return nodes;
        }
    }

}

