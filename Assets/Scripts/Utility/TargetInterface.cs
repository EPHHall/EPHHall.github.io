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
            if (target == null) return;

            SS.Character.CharacterStats stats;

            if (GameObject.FindObjectOfType<SS.Animation.ScreenShake>() != null && damage.amount > 0)
            {
                GameObject.FindObjectOfType<SS.Animation.ScreenShake>().Run();
            }

            if (target.TryGetComponent(out stats))
            {
                damage.InflictOnTarget(target, stats, inflictor);
                return;
            }
        }

        public static void DamageTargetAtEndOfTurn(Target target, Damage damage, Effect inflictor)
        {
            if (target == null) return;

            target.inflictAtEndOfTur.Add(
                new Target.DamageStuff(target, damage, inflictor));
        }

        public static void DamageTargetWhenAnimationHits(Target target, Damage damage, Effect inflictor)
        {
            if (target == null) return;

            target.inflictWhenAnimationReaches.Add(
                new Target.DamageStuff(target, damage, inflictor));
        }
    }
}
