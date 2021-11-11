using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    [ExecuteAlways]
    public class MeleeAttack : Effect
    {
        public override void Awake()
        {
            base.Awake();
            ResetStats();
        }

        public override void ResetStats()
        {
            speed = 1;

            damage = 2;
            range = 1;

            actionPointCost = 1;

            duration = -1;
            toInstantiate = null;

            normallyValid = new TargetType(true, true, true, true, true, true, false, false);
        }

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            foreach (Target target in targets)
            {
                DamageTarget(target, damage);
            }

            //Debug.Log(deliveredEffects.Count);
            foreach (Effect effect in deliveredEffects)
            {
                Debug.Log(effect.name);
                effect.InvokeEffect(targets);
            }
        }
    }
}
