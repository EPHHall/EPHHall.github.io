using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    [ExecuteAlways]
    public class ArcaneBolt : Effect
    {
        public override void Awake()
        {
            base.Awake();

            speed = 1;
            manaCost = 2;
            spellPointCost = 2;

            damage = 1;
            range = 3;

            actionPointCost = 1;

            duration = 0;
            toInstantiate = null;

            normallyValid = new TargetType(true, true, true, false, false, false, false, false);
        }

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            foreach (Target target in targets)
            {
                DamageTarget(target, damage);
            }

            foreach (Effect effect in deliveredEffects)
            {
                effect.InvokeEffect(targets);
            }
        }
    }
}
