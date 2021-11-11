using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Util
{
    public class SS_AStar
    {        
        public List<Vector2> AStar(List<Vector2> positionsToCheck, List<Vector2> takenPositions)
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
                    if (ray.collider == null || ray.collider.GetComponent<NotObstacle>())
                    {
                        nextPositions.Add(potentialPosition);
                        Debug.DrawLine(position, position + (Vector2.up * .1f), Color.red, 10);
                        Debug.DrawLine(position, position + (Vector2.down * .1f), Color.red, 10);
                        Debug.DrawLine(position, position + (Vector2.left * .1f), Color.red, 10);
                        Debug.DrawLine(position, position + (Vector2.right * .1f), Color.red, 10);
                    }
                }
            }

            return nextPositions;
        }

        public List<Vector2> AStar_ForMovement(List<Vector2> positionsToCheck, List<Vector2> takenPositions)
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

                    //This if statement is the main bit that's different between the 2 AStar methods
                    RaycastHit2D ray = Physics2D.Linecast(position, potentialPosition);
                    if (ray.collider == null)
                    {
                        nextPositions.Add(potentialPosition);
                        Debug.DrawLine(position, position + (Vector2.up * .1f), Color.green, 10);
                        Debug.DrawLine(position, position + (Vector2.down * .1f), Color.green, 10);
                        Debug.DrawLine(position, position + (Vector2.left * .1f), Color.green, 10);
                        Debug.DrawLine(position, position + (Vector2.right * .1f), Color.green, 10);
                    }
                    else
                    {

                    }
                }
            }

            return nextPositions;
        }
    }
}
