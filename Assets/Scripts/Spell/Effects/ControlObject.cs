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

            mainStatus = new StatusSpace.Status(StatusSpace.Status.StatusName.Controlled, 1, duration, SS.GameController.TurnManager.currentTurnTaker);
            originalStatusList.Clear();
            AddToOriginalStatusList(mainStatus);
            ResetMainStatusList();

            //duration = 2;

            normallyValid = new TargetType(false, true, true, false);
        }

        public override void Start()
        {
            base.Start();

            animationToPlay = animationObjectManager.controlObjectAnimation;
            soundEffect = resources.GetControlObjectAudio();
        }

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            HandleDeliveredAndTargeting(targets);

            //use targetsCopy from now on
            foreach (Target target in targetsCopy)
            {
                if (target.targetType.obj)
                {
                    StatusSpace.Status newStatus;
                    if (enemyVersion)
                    {
                        //Debug.Log("enemy version");
                        newStatus = new StatusSpace.Status(StatusSpace.Status.StatusName.ControlledByEnemy, 1, duration, SS.GameController.TurnManager.currentTurnTaker);
                    }
                    else
                    {
                        //Debug.Log("regular version");
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
