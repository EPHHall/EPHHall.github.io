using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;

namespace SS.Spells
{
    [ExecuteAlways]
    public class Possess : Effect_Possession
    {
        public override void Awake()
        {
            base.Awake();

            manaCost = 5;
            spellPointCost = 4;
            baseDamage = 0;

            range = 1;
            radius = 1;

            actionPointCost = 1;

            duration = 1;

            normallyValid = new TargetType(true, true, true, true);

            mainDamage = new Character.Damage(Character.Damage.DamageType.Arcane, 0);
            mainStatus = new Status(Status.StatusName.Possessed, 1, duration, SS.GameController.TurnManager.instance.CurrentTurnTaker, radius);

            originalDamageList.Clear();
            AddToOriginalDamageList(mainDamage, null);
            ResetMainDamageList();

            originalStatusList.Clear();
            AddToOriginalStatusList(mainStatus);
            ResetMainStatusList();

            style = Style.Utility;
        }

        public override void Start()
        {
            base.Start();

            animationToPlay = animationObjectManager.hypnotizeAnimation;
            soundEffect = resources.GetPossessAudio();
        }

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            HandleDeliveredAndTargeting(targets);
            //Use targetsCopy form now on. Sometimes the original list gets reset after HandleDeliveredAndTargeting. Using the copy is the workaround.

            foreach (Target target in targetsCopy)
            {
                foreach (Status status in statusList)
                {
                    target.ApplyStatus(mainStatus, this);
                }

                if (target.targetType.weapon)
                {
                    target.HandleStatuses(false, true);
                }
            }

            EndInvoke();
        }
    }
}
