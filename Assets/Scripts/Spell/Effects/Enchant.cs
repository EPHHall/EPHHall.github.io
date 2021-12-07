using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;

namespace SS.Spells
{
    [ExecuteAlways]
    public class Enchant : Effect
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

            int fullDamage = 0;
            foreach (SS.Character.Damage damage in damageList)
            {
                if (damage.type == Character.Damage.DamageType.Enchantment)
                    fullDamage += damage.amount;
            }

            //just looking at the first target for testing purposes, in the final cut this should apply to every target
            List<Status> statusesMainStatusShouldApply = new List<Status>();
            HandleDeliveredEffects(targets[0], statusesMainStatusShouldApply);
            HandleTargetingEffects(targets[0]);

            foreach (Target target in targets)
            {
                foreach (Status status in statusList)
                {
                    if (status.unarmedOnly && spellAttachedTo.activeWeapons.Count > 0)
                    {
                        continue;
                    }

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

                foreach (Character.Damage d in damageList)
                {
                    Util.TargetInterface.DamageTarget(target, d, this);
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

                if (weapon != null)
                {
                    //RemoveStatusesFromList(weapon.statusesToInflict, effect);

                    foreach (Status status in effect.statusList)
                    {
                        weapon.AddStatusToInflict(status);
                    }
                }
                else if (creature != null)
                {
                    //RemoveStatusesFromList(target.statuses, effect);
                    Effect melee = null;
                    if (creature.transform.Find("Melee Attack") != null && creature.transform.Find("Melee Attack").GetComponent<Spell>() != null)
                    {
                        melee = creature.transform.Find("Melee Attack").GetComponent<Spell>().main;
                    }

                    foreach (Status status in effect.statusList)
                    {
                        Status newStatus = new Status(status);
                        newStatus.unarmedOnly = true;

                        if (melee != null)
                        {
                            Debug.Log("melee wasn't null");
                            melee.AddToMainStatusList(newStatus);
                        }
                    }
                }
                else if (target.targetType.obj)
                {
                    foreach (Status status in effect.statusList)
                    {
                        statusesMainStatusShouldApply.Add(status);
                    }
                }
            }
        }

        public override void HandleTargetingEffects(Target target)
        {
            foreach (Effect effect in targetMeEffects)
            {
                if (effect == null) continue;

                if (effect.style == Style.InstantDamage)
                {
                    AddToMainDamageList(effect.mainDamage);
                }
                else if (effect.style == Style.DamageOverTime)
                {
                    AddToMainStatusList(effect.mainStatus);
                }
            }
        }
    }
}

