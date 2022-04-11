using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Pathfinding
{
    public class Grid : MonoBehaviour
    {
        public Transform startPos;
        public LayerMask wallMask;
        public Vector2 gridWorldSize;
        public float nodeRadius;
        public float distance;

        Node[,] grid;
        public List<Node> path = new List<Node>();
        public List<Node> visited = new List<Node>();
        public Node startN = null;
        public Node endN = null;
        private Node firstNode;

        public float nodeDiameter;
        int gridSizeX, gridSizeY;

        private void Start()
        {
            nodeDiameter = nodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

            CreateGrid();
        }

        void CreateGrid()
        {
            grid = new Node[gridSizeX, gridSizeY];
            Vector2 bottomLeft = (Vector2)startPos.position + Vector2.left * gridWorldSize.x / 2 + Vector2.down * gridWorldSize.y / 2;
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector2 worldPoint = bottomLeft + Vector2.right * (x * nodeDiameter + distance) + Vector2.up * (y * nodeDiameter + distance);
                    bool wall = false;

                    if (Physics2D.OverlapCircle(worldPoint, nodeRadius/2f, wallMask))
                    {
                        wall = true;
                    }

                    grid[x, y] = new Node(wall, worldPoint, x, y);
                }
            }

            firstNode = grid[0, 0];
        }

        public Node NodeFromWorldPosition(Vector3 worldPos)
        {
            float xPoint = ((worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x) /*- transform.position.x*/;
            float yPoint = ((worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y) /*- transform.position.y*/;

            xPoint = Mathf.Clamp01(xPoint);
            yPoint = Mathf.Clamp01(yPoint);

            int x = Mathf.RoundToInt((gridSizeX - 1) * xPoint);
            int y = Mathf.RoundToInt((gridSizeY - 1) * yPoint);

            return grid[x, y];
        }

        public List<Node> GetNeighboringNodes(Node currentNode)
        {
            List<Node> neighboringNodes = new List<Node>();
            int xCheck;
            int yCheck;

            //right side
            xCheck = currentNode.xPos + 1;
            yCheck = currentNode.yPos;
            if (xCheck >= 0 && xCheck < gridSizeX)
            {
                if (yCheck >= 0 && yCheck < gridSizeY)
                {
                    neighboringNodes.Add(grid[xCheck, yCheck]);
                }
            }

            //left side
            xCheck = currentNode.xPos - 1;
            yCheck = currentNode.yPos;
            if (xCheck >= 0 && xCheck < gridSizeX)
            {
                if (yCheck >= 0 && yCheck < gridSizeY)
                {
                    neighboringNodes.Add(grid[xCheck, yCheck]);
                }
            }

            //up side
            xCheck = currentNode.xPos;
            yCheck = currentNode.yPos + 1;
            if (xCheck >= 0 && xCheck < gridSizeX)
            {
                if (yCheck >= 0 && yCheck < gridSizeY)
                {
                    neighboringNodes.Add(grid[xCheck, yCheck]);
                }
            }

            //down side
            xCheck = currentNode.xPos;
            yCheck = currentNode.yPos - 1;
            if (xCheck >= 0 && xCheck < gridSizeX)
            {
                if (yCheck >= 0 && yCheck < gridSizeY)
                {
                    neighboringNodes.Add(grid[xCheck, yCheck]);
                }
            }

            return neighboringNodes;
        }

        public void ReevaluateWallStatus(Node node)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(node.position, nodeRadius / 2f, wallMask);

            node.isWall = Util.CheckIfWayIsClear.Check(hits, true);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

            if (grid != null)
            {
                foreach (Node node in grid)
                {
                    if (node.isWall)
                    {
                        Gizmos.color = Color.yellow;
                    }
                    else
                    {
                        Gizmos.color = Color.white;
                    }

                    if (node == firstNode || visited != null && visited.Contains(node))
                    {
                        Gizmos.color = Color.green;
                    }

                    if (path != null && path.Contains(node))
                    {
                        Gizmos.color = Color.red;
                    }

                    Gizmos.DrawSphere(node.position, nodeRadius / 2f);
                }
            }
        }
    }
}
