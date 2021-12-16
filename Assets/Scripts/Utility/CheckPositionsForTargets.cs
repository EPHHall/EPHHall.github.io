using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Util
{
    public class CheckPositionsForTargets : MonoBehaviour
    {
        public static List<SS.Spells.Target> Check(List<Vector2> toCheck)
        {
            List<SS.Spells.Target> targets = new List<Spells.Target>();
            foreach (Vector2 current in toCheck)
            {
                Collider2D[] colliders = Physics2D.OverlapBoxAll(current, new Vector2(.5f, .5f), 0);

                AddListToOriginal.AddList(targets, GetOnlyTargets.GetTargets(colliders));
            }

            return targets;
        }

        public static List<SS.Spells.Target> CheckRadius(List<Vector2> toCheck, int radius)
        {
            List<Vector2> checkForTargets = SS_AStar.GetPositionsWithinRadius(toCheck, radius);

            List<SS.Spells.Target> targets = new List<Spells.Target>();
            foreach (Vector2 current in checkForTargets)
            {
                Collider2D[] colliders = Physics2D.OverlapBoxAll(current, new Vector2(.5f, .5f), 0);

                AddListToOriginal.AddList(targets, GetOnlyTargets.GetTargets(colliders));
            }

            return targets;
        }
        public static List<Vector2> CheckForEmptySpaces(List<Vector2> toCheck, int radius)
        {
            List<Vector2> checkForEmpty = SS_AStar.GetPositionsWithinRadius(toCheck, radius);

            List<Vector2> emptySpaces = new List<Vector2>();
            foreach (Vector2 current in checkForEmpty)
            {
                if (Physics2D.OverlapBoxAll(current, new Vector2(.5f, .5f), 0).Length == 0)
                {
                    emptySpaces.Add(current);
                }
                else
                {
                    bool isEmpty = true;

                    foreach (Collider2D col in Physics2D.OverlapBoxAll(current, new Vector2(.5f, .5f), 0))
                    {
                        if (!col.GetComponent<BackgroundCollider>())
                        {
                            isEmpty = false;
                        }
                    }

                    if (isEmpty)
                    {
                        emptySpaces.Add(current);
                    }
                }
            }

            return emptySpaces;
        }
    }
}
