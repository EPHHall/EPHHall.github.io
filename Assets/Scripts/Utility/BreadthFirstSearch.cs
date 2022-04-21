using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Util
{
    public class BreadthFirstSearch
    {
        public static List<Vector2> BFS(List<Vector2> initialPositions, List<Vector2> previousPositions)
        {
            List<Vector2> newPositions = new List<Vector2>();
            foreach (Vector2 position in initialPositions)
            {
                if(!previousPositions.Contains(position))
                    previousPositions.Add(position);

                Vector2 potentialPosUp = position + Vector2.up;
                Vector2 potentialPosDown = position + Vector2.down;
                Vector2 potentialPosLeft = position + Vector2.left;
                Vector2 potentialPosRight = position + Vector2.right;

                if (!previousPositions.Contains(potentialPosUp) || newPositions.Contains(potentialPosUp))
                    newPositions.Add(potentialPosUp);
                if (!previousPositions.Contains(potentialPosDown) || newPositions.Contains(potentialPosUp))
                    newPositions.Add(potentialPosDown);
                if (!previousPositions.Contains(potentialPosLeft) || newPositions.Contains(potentialPosUp))
                    newPositions.Add(potentialPosLeft);
                if (!previousPositions.Contains(potentialPosRight) || newPositions.Contains(potentialPosUp))
                    newPositions.Add(potentialPosRight);
            }

            return newPositions;
        }
    }
}
