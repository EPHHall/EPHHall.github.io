using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    List<Vector2> A_Star(List<Vector2> positionsToCheck, List<Vector2> takenPositions)
    {
        List<Vector2> nextPositions = new List<Vector2>();

        foreach (Vector2 position in positionsToCheck)
        {
            Vector2[] directions = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };

            for (int i = 0; i < directions.Length; i++)
            {
                Vector2 potentialPosition = position + directions[i];

                if (takenPositions.Contains(potentialPosition) || nextPositions.Contains(potentialPosition))
                {
                    continue;
                }

                RaycastHit2D ray = Physics2D.Linecast(position, potentialPosition);
                if (ray.collider == null)
                {
                    nextPositions.Add(potentialPosition);
                }
            }
        }

        return nextPositions;
    }
}
