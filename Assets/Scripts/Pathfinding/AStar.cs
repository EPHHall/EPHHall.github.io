using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Pathfinding
{
    public class AStar : MonoBehaviour
    {
        Grid grid;
        public Transform startPos;
        public Transform targetPos;

        public bool findPath;
        public bool alwaysFindPath;

        private void Awake()
        {
            grid = GetComponent<Grid>();
        }

        private void Update()
        {
            if (findPath || alwaysFindPath)
            {
                findPath = false;
                FindPath(startPos.position, targetPos.position);
            }
        }

        public List<Node> FindPath(Vector2 start, Vector2 end)
        {
            grid.visited.Clear();

            start -= (Vector2)transform.position;
            end -= (Vector2)transform.position;

            Node startNode = grid.NodeFromWorldPosition(start);
            Node endNode = grid.NodeFromWorldPosition(end);

            List < Node > openList = new List<Node>();
            HashSet<Node> closedList = new HashSet<Node>();

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                Node currentNode = openList[0];

                grid.visited.Add(currentNode);

                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].fCost < currentNode.fCost && openList[i].hCost < currentNode.hCost)
                    {
                        currentNode = openList[i];
                    }
                }
             
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode == endNode)
                {
                    //Debug.Log("Did reach end node");
                    return GetFinalPath(startNode, endNode, end);
                }

                foreach (Node neighborNode in grid.GetNeighboringNodes(currentNode))
                {
                    grid.ReevaluateWallStatus(neighborNode);

                    if (neighborNode.isWall)
                    {
                        if (neighborNode == endNode)
                        {
                            //Debug.Log("Did reach end node 2");
                            neighborNode.previous = currentNode;

                            return GetFinalPath(startNode, endNode, end);
                        }

                        continue;
                    }

                    if (closedList.Contains(neighborNode))
                    {
                        continue;
                    }

                    int moveCost = currentNode.gCost + GetManhattenDistance(currentNode, neighborNode);
                    if (moveCost < neighborNode.gCost || !openList.Contains(neighborNode))
                    {
                        neighborNode.gCost = moveCost;
                        neighborNode.hCost = GetManhattenDistance(neighborNode, endNode);
                        neighborNode.previous = currentNode;

                        if (!openList.Contains(neighborNode))
                        {
                            openList.Add(neighborNode);
                        }
                    }
                }
            }

            return null;
        }

        private List<Node> GetFinalPath(Node startNode, Node endNode, Vector2 end)
        {
            List<Node> finalPath = new List<Node>();
            Node currentNode = endNode;
            finalPath.Add(currentNode);

            while (currentNode != startNode)
            {
                if(!finalPath.Contains(currentNode))
                    finalPath.Add(currentNode);

                currentNode = currentNode.previous;
            }

            //remove the target's node, we dont want things moving into eachother
            finalPath.RemoveAt(0);

            finalPath.Reverse();

            grid.path = finalPath;

            return finalPath;
        }

        int GetManhattenDistance(Node nodeA, Node nodeB)
        {
            int manX = Mathf.Abs(nodeA.xPos - nodeB.xPos);
            int manY = Mathf.Abs(nodeA.yPos - nodeB.yPos);

            return manX + manY;
        }
    }
}
