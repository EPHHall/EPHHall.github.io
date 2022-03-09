using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Pathfinding
{
    [System.Serializable]
    public class Node
    {
        public int xPos;//X position in grid
        public int yPos;//Y position in grid

        public bool isWall;
        public Vector3 position;//World position

        public int hCost;
        public int gCost;
        public int fCost { get { return gCost + hCost; } }

        public Node previous;

        public Node(bool isWall, Vector3 position, int xPos, int yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.position = position;
            this.isWall = isWall;
        }
    }
}
