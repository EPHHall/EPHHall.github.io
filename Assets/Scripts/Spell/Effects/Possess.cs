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
            mainStatus = new Status(Status.StatusName.Possessed, 1, duration, SS.GameController.TurnManager.currentTurnTaker, radius);

            originalDamageList.Clear();
            AddToOriginalDamageList(mainDamage, null);
            ResetMainDamageList();

            originalStatusList.Clear();
            AddToOriginalStatusList(mainStatus);
            ResetMainStatusList();

            style = Style.Utility;
        }

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            HandleDeliveredAndTargeting(targets);

            foreach (Target target in targets)
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

        /*public override void HandleDeliveredEffects(Target target, List<Status> statusesMainStatusShouldApply)
        {
            Item.Weapon weapon = null;
            Character.CharacterStats creature = null;

            if (target.targetType.weapon)
            {
                //If the target is a weapon it should have the weapon script
                weapon = target.GetComponent<Item.Weapon>();
            }
            //currently multiple target types is not supported
            else if (target.targetType.creature)
            {
                creature = target.GetComponent<Character.CharacterStats>();
            }

            foreach (Effect effect in deliveredEffects)
            {
                if (effect == null) continue;

                foreach (Status status in effect.statusList)
                {
                    target.ApplyStatus(status, this);
                }

                if (weapon != null)
                {
                    foreach (Status status in effect.statusList)
                    {
                        weapon.AddStatusToInflict(status);
                    }
                }
            }
        }

        public override void HandleTargetingEffects(Target target)
        {
            base.HandleTargetingEffects(target);

            foreach (Effect effect in deliveredEffects)
            {
                foreach (Status status in effect.statusList)
                {
                    target.ApplyStatus(status, this);
                }
            }
        }*/
    }
}
