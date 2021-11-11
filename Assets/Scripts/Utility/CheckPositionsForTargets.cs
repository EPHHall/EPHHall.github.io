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
    }
}
