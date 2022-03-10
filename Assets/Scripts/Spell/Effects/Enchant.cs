using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;

namespace SS.Spells
{
    [ExecuteAlways]
    public class Enchant : Effect_Enchanting
    {
        public override void Awake()
        {
            base.Awake();

            manaCost = 5;
            spellPointCost = 4;
            baseDamage = 6;

            range = 1;
            radius = 1;

            actionPointCost = 1;

            duration = 1;

            normallyValid = new TargetType(true, true, true, true);

            mainStatus = new Status(Status.StatusName.ArcaneDamage, baseDamage, duration, SS.GameController.TurnManager.currentTurnTaker, radius);
            mainDamage = new Character.Damage(Character.Damage.DamageType.Enchantment, baseDamage);

            originalDamageList.Clear();
            //AddToOriginalDamageList(mainDamage);
            ResetMainDamageList();

            originalStatusList.Clear();
            originalStatusList.Add(mainStatus);
            ResetMainStatusList();

            style = Style.DamageOverTime;
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

            ResetMainStatusList();
            ResetMainDamageList();

            if (target.targetType.weapon)
            {
                target.HandleStatuses(false, true);
            }

            EndInvoke();
        }
    }
}

