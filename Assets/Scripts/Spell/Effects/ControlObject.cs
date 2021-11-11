using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    [ExecuteAlways]
    public class ControlObject : Effect
    {
        public override void Awake()
        {
            base.Awake();

            speed = 2;
            manaCost = 9;
            spellPointCost = 6;

            range = 1;
            damage = 0;

            actionPointCost = 2;

            duration = 2;

            normallyValid = new TargetType(false, false, true, true, false, false, true, false);
        }

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            foreach (Target target in targets)
            {
                AllTargets(target.targetType, target);
            }
        }

        public override void IfTargetIsStructure(Target target)
        {
            base.IfTargetIsStructure(target);

            target.gameObject.AddComponent<ControllableByPlayer>();
            target.GetComponent<ControllableByPlayer>().Initialize(duration);
        }

        public override void IfTargetIsItem(Target target)
        {
            base.IfTargetIsItem(target);

            target.ApplyStatus("Controlled by Player", -1);
        }

        public override void IfTargetIsWeapon(Target target)
        {
            base.IfTargetIsStructure(target);

            target.ApplyStatus("Controlled by Player", -1);
        }
    }
}
