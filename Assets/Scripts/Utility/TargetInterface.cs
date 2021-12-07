using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;
using SS.Character;

namespace SS.Util
{
    public class TargetInterface
    {
        public static void DamageTarget(Target target, Damage damage, Effect inflictor)
        {
            SS.Character.CharacterStats stats;

            if (target.TryGetComponent(out stats))
            {
                damage.InflictOnTarget(target, stats, inflictor);
                return;
            }
        }
    }
}
