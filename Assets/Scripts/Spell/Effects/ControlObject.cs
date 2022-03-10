using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    [ExecuteAlways]
    public class ControlObject : Effect_Possession
    {
        [Space(10)]
        [Header("Control Object Specific")]
        public bool enemyVersion;

        public override void Awake()
        {
            base.Awake();

            manaCost = 9;
            spellPointCost = 6;

            range = 1;
            originalDamageList.Clear();
            ResetMainDamageList();

            actionPointCost = 2;

            //duration = 2;

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
                    StatusSpace.Status newStatus;
                    if (enemyVersion)
                    {
                        Debug.Log("enemy version");
                        newStatus = new StatusSpace.Status(StatusSpace.Status.StatusName.ControlledByEnemy, 1, duration, SS.GameController.TurnManager.currentTurnTaker);
                    }
                    else
                    {
                        Debug.Log("regular version");
                        newStatus = new StatusSpace.Status(StatusSpace.Status.StatusName.Controlled, 1, duration, SS.GameController.TurnManager.currentTurnTaker);
                    }

                    target.ApplyStatus(newStatus, this);
                    target.HandleStatus(false, true, newStatus);
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
