using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Util
{
    public class FindFirstEmptySpots
    {
        /// <summary>
        /// Uses breadth first search to look at all of the positions surrounding the initial positions provided. It then checks to see if any of those positions
        /// are taken up by a solid object. If not, it repeats the search and checks again. Returns the first batch of empty positions it finds, or empty list.
        /// </summary>
        /// <param name="initialPositions"></param>
        public static List<Vector2> FFES(List<Vector2> positionsToCheck, List<Vector2> previousPositions)
        {
            List<Vector2> firstEmptySpots = new List<Vector2>();

            while (firstEmptySpots.Count == 0 && positionsToCheck.Count != 0)
            {
                foreach (Vector2 position in positionsToCheck)
                {
                    Collider2D hit = Physics2D.OverlapCircle(position, .1f, GameController.LayerMaskForObstacleFinding.lmof.layerMask);
                    if (hit == null || (Vector2)GameController.TurnManager.currentTurnTaker.transform.position == position)
                    {
                        firstEmptySpots.Add(position);
                    }

                    if(!previousPositions.Contains(position))
                    {
                        previousPositions.Add(position);
                    }
                }

                positionsToCheck = BreadthFirstSearch.BFS(positionsToCheck, previousPositions);
            }

            return firstEmptySpots;
        }//FFES()
    }
}
