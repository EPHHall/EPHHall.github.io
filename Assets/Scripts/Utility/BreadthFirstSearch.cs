using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SS.Util
{
    public class BreadthFirstSearch
    {
        static List<Vector2> debugList = new List<Vector2>();

        public static List<Vector2> BFS(List<Vector2> initialPositions, List<Vector2> previousPositions, LayerMask whatIsWall)
        {
            previousPositions.AddRange(initialPositions);
            List<Vector2> emptyPositions = new List<Vector2>();

            while(initialPositions.Count > 0)
            {
                Vector2 currentPosition = initialPositions[0];
                initialPositions.RemoveAt(0);

                Vector2[] positionsToCheck = { currentPosition + Vector2.up, currentPosition + Vector2.down, currentPosition + Vector2.left, currentPosition + Vector2.right };

                foreach(Vector2 position in positionsToCheck)
                {
                    if (previousPositions.Contains(position)) continue;

                    Collider2D collider2D = Physics2D.OverlapCircle(position, .1f, whatIsWall);
                    if(collider2D == null)
                    {
                        emptyPositions.Add(position);
                    }
                }
            }

            debugList = previousPositions.Concat(emptyPositions).ToList();

            return emptyPositions;
        }

        public static List<Vector2> BFS(List<Vector2> initialPositions, List<Vector2> previousPositions, LayerMask whatIsWall, int iterations)
        {
            for(int i = 0; i < iterations; i++)
            {
                initialPositions = BFS(initialPositions, previousPositions, whatIsWall);
            }

            return initialPositions;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            foreach (Vector2 position in debugList)
            {
                Gizmos.DrawSphere(position, .5f);
            }
        }
    }
}
