using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    [ExecuteAlways]
    public class ControlObject : Effect_Possession
    {
        public override void Awake()
        {
            base.Awake();

            speed = 2;
            manaCost = 9;
            spellPointCost = 6;

            range = 1;
            originalDamageList.Clear();
            ResetMainDamageList();

            actionPointCost = 2;

            duration = 2;

            normallyValid = new TargetType(false, true, true, false);
        }

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            HandleDeliveredAndTargeting(targets);

            foreach (Target target in targets)
            {
                if (target.targetType.obj)
                {
                    target.ApplyStatus(StatusSpace.Status.StatusName.Controlled, 1, duration, SS.GameController.TurnManager.currentTurnTaker, this);
                }
            }

            EndInvoke();
        }

        public override void IfTargetIsWeapon(Target target)
        {
            base.IfTargetIsWeapon(target);

            target.ApplyStatus(StatusSpace.Status.StatusName.Controlled, 1, duration, SS.GameController.TurnManager.currentTurnTaker, this);
        }
    }
}
