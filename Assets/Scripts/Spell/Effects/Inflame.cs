using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;

namespace SS.Spells
{
    [ExecuteAlways]
    public class Inflame : Effect_Enchanting
    {
        public override void Awake()
        {
            base.Awake();

            speed = 1;
            manaCost = 5;
            spellPointCost = 4;
            baseDamage = 6;

            range = 1;
            radius = 1;

            actionPointCost = 1;

            duration = 1;

            normallyValid = new TargetType(true, true, true, true);

            mainDamage = new Character.Damage(Character.Damage.DamageType.Inflame, baseDamage);
            mainStatus = new Status(Status.StatusName.FireDamage, baseDamage, duration, SS.GameController.TurnManager.currentTurnTaker, radius);

            originalDamageList.Clear();
            //originalDamageList.Add(mainDamage);
            ResetMainDamageList();

            originalStatusList.Clear();
            originalStatusList.Add(mainStatus);
            ResetMainStatusList();

            style = Style.DamageOverTime;

            base.Awake();
        }

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            Target target = targets[0];

            HandleDeliveredAndTargeting(targets);

            foreach (Status status in statusList)
            {
                target.ApplyStatus(status, this);
            }
            foreach (Character.Damage damage in damageList)
            {
                DamageTarget(target, damage);
            }

            if (target.targetType.weapon)
            {
                target.HandleStatuses(false, true);
            }

            EndInvoke();
        }
    }
}
