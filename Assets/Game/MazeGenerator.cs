using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : Singleton<MazeGenerator>
{
    [SerializeField] MazeNode nodePrefab;

    private List<int> currentPath;
    private List<int> completedNodes;
    private List<MazeNode> nodes;

    public int count = 0;
    public MazeNode start;
    public MazeNode end;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void clear()
    {
        foreach (Transform node in transform)
            GameObject.Destroy(node.gameObject);
    }


    private void markAsCurrent(int index)
    {
        currentPath.Add(index);
        nodes[index].SetState(MazeNode.NodeState.Current);
    }

    private void markAsComplete(int index)
    {
        updateCount();
        completedNodes.Add(index);
        nodes[index].SetState(MazeNode.NodeState.Completed);
        currentPath.RemoveAt(currentPath.Count - 1);
    }

    private void updateCount()
    {
        if(currentPath.Count > count)
        {
            count = currentPath.Count;
            end = nodes[lastCurrent];
        }
    }

    private int lastCurrent { get => currentPath[currentPath.Count - 1]; }

    public MazeNode RandomNode()
    {
        return nodes[Random.Range(0, nodes.Count)];
    }

    public void Generator(Vector2Int size)
    {
        nodes = generateNodeList(size);
        currentPath = new List<int>();
        completedNodes = new List<int>();

        markAsCurrent(Random.Range(0, nodes.Count));

        count = 0;
        start = nodes[lastCurrent];


        while (completedNodes.Count < nodes.Count)
        {
            List<int> possibleNextNodes = new List<int>();
            List<MazeNode.Walls> possibleDirections = new List<MazeNode.Walls>();

            int currentNodeIndex = lastCurrent;

            Vector2Int pos = new Vector2Int(currentNodeIndex / size.y, currentNodeIndex % size.y);


            void checkNode(bool wallCheck, int index, MazeNode.Walls side)
            {
                if (wallCheck && !completedNodes.Contains(index) && !currentPath.Contains(index))
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

                nodes[currentNodeIndex].RemoveWallInverse(wallSide);
                nodes[currentPath[currentPath.Count - 1]].RemoveWall(wallSide);

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
