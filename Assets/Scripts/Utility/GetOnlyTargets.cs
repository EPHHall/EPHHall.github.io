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

        public static List<SS.Spells.Target> GetTargets(List<Vector2> list)
        {
            List<SS.Spells.Target> targets = new List<Spells.Target>();
            List<Collider2D> collider2Ds = new List<Collider2D>();

            foreach (Vector2 v in list)
            {
                foreach (Collider2D c in Physics2D.OverlapCircleAll(v, .4f))
                {
                    collider2Ds.Add(c);
                }
            }

            return GetTargets(collider2Ds);
        }

        public static List<SS.Spells.Target> GetTargets(Collider2D[] list)
        {
            List<SS.Spells.Target> targets = new List<Spells.Target>();
            List<SS.Spells.Target> targetsObjects = new List<Spells.Target>();
            List<SS.Spells.Target> targetsWeapons = new List<Spells.Target>();
            List<SS.Spells.Target> targetsTiles = new List<Spells.Target>();

            foreach (Collider2D c in list)
            {
                Spells.Target target = c.GetComponent<SS.Spells.Target>();
                if (target != null)
                {
                    if (target.targetType.creature)
                    {
                        targets.Add(c.GetComponent<SS.Spells.Target>());
                    }
                    else if (target.targetType.obj)
                    {
                        targetsObjects.Add(c.GetComponent<SS.Spells.Target>());
                    }
                    else if (target.targetType.weapon)
                    {
                        targetsWeapons.Add(c.GetComponent<SS.Spells.Target>());
                    }
                    else
                    {
                        targetsTiles.Add(c.GetComponent<SS.Spells.Target>());
                    }
                }
            }

            foreach (Spells.Target target in targetsObjects)
            {
                targets.Add(target);
            }
            foreach (Spells.Target target in targetsWeapons)
            {
                targets.Add(target);
            }
            foreach (Spells.Target target in targetsTiles)
            {
                targets.Add(target);
            }

            return targets;
        }
    }
}
