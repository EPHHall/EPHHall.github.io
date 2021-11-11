using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Util
{
    public class GetOnlyTargets
    {
        public static List<SS.Spells.Target> GetTargets(List<Collider2D> list)
        {
            List<SS.Spells.Target> targets = new List<Spells.Target>();

            foreach (Collider2D c in list)
            {
                if (c.GetComponent<SS.Spells.Target>())
                {
                    targets.Add(c.GetComponent<SS.Spells.Target>());
                }
            }

            return targets;
        }

        public static List<SS.Spells.Target> GetTargets(Collider2D[] list)
        {
            List<SS.Spells.Target> targets = new List<Spells.Target>();

            foreach (Collider2D c in list)
            {
                if (c.GetComponent<SS.Spells.Target>())
                {
                    //Debug.Log("In Thing");
                    targets.Add(c.GetComponent<SS.Spells.Target>());
                }
            }

            return targets;
        }
    }
}
