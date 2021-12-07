using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;

namespace SS.Spells
{
    [ExecuteAlways]
    public class Possess : Effect
    {
        public override void Awake()
        {
            base.Awake();

            speed = 1;
            manaCost = 5;
            spellPointCost = 4;
            baseDamage = 0;

            range = 1;
            radius = 1;

            actionPointCost = 1;

            duration = 2;

            normallyValid = new TargetType(true, true, true, true);

            mainDamage = new Character.Damage(Character.Damage.DamageType.Arcane, 0);
            mainStatus = new Status(Status.StatusName.Possessed, 1, duration, SS.GameController.TurnManager.currentTurnTaker, radius);

            originalDamageList.Clear();
            AddToOriginalDamageList(mainDamage);
            ResetMainDamageList();

            originalStatusList.Clear();
            AddToOriginalStatusList(mainStatus);
            ResetMainStatusList();

            style = Style.Utility;
        }

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            //just looking at the first target for testing purposes, in the final cut this should apply to every target
            List<Status> statusesMainStatusShouldApply = new List<Status>();
            HandleDeliveredEffects(targets[0], statusesMainStatusShouldApply);

            foreach (Target target in targets)
            {
                foreach (Status status in statusList)
                {
                    if (status.unarmedOnly && spellAttachedTo.activeWeapons.Count > 0)
                        continue;

                    Status newStatus = new Status(status);
                    foreach (Status s in statusesMainStatusShouldApply)
                    {
                        newStatus.AddStatusToApply(s);
                    }
                    target.ApplyStatus(newStatus, this);
                }

                if (target.targetType.weapon)
                {
                    target.HandleStatuses(false, true);
                }
            }
        }

        public override void HandleDeliveredEffects(Target target, List<Status> statusesMainStatusShouldApply)
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
        }
    }
}
