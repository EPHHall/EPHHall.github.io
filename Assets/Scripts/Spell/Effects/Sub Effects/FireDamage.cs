using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    [ExecuteAlways]
    //added to targets by other effects
    public class FireDamage : Effect
    {
        public int counterIndex;

        public override void Awake()
        {
            base.Awake();

            damage = 2;

            duration = 0;
            toInstantiate = null;

            normallyValid = new TargetType(true, true, true, true, true, true, true, true);
        }

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            foreach (Target target in targets)
            {
                DamageTarget(target, damage);
            }
        }
    }
}
