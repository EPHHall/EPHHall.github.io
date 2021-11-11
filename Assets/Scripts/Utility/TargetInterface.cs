using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;

namespace SS.Util
{
    public class TargetInterface
    {
        public static void DamageTarget(Target target, int damage)
        {
            SS.Character.CharacterStats stats;
            if (target.TryGetComponent(out stats))
            {
                CharacterStatsInterface.DamageHP(stats, damage);
                return;
            }
        }
    }
}
