using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;

namespace SS.Spells
{
    public class Effect : MonoBehaviour
    {
        public static EffectResources resources;

        public enum Style
        {
            InstantDamage,
            DamageOverTime,
            Utility
        }

        [Space(5)]
        [Header("Behavior if Main")]
        public bool targetable;

        [Space(5)]
        [Header("Stats")]
        public int speed;
        public int manaCost;
        public int spellPointCost;
        public int actionPointCost;

        public int baseDamage;
        public List<SS.Character.Damage> damageList;
        public List<SS.Character.Damage> originalDamageList;
        public List<SS.StatusSpace.Status> statusList;
        public List<SS.StatusSpace.Status> originalStatusList;

        public int range;
        public int radius;
        public int duration;//in turns
        public List<string> keywords;

        public Spell spellAttachedTo;

        [Space(5)]
        [Header("Casting Vars")]
        public GameObject toInstantiate;

        public TargetType normallyValid = new TargetType(true);
        public List<Effect> deliveredEffects = new List<Effect>();
        public List<Effect> targetMeEffects = new List<Effect>();

        [Space(5)]
        [Header("If Being Delivered")]
        public Effect attachedEffect;

        [Space(5)]
        [Header("Misc")]
        public Sprite icon;
        public Style style;
        public Status mainStatus;
        public Character.Damage mainDamage;

        public virtual void Awake()
        {
            if (resources == null)
            {
                resources = GameObject.Find("Effect Resources").GetComponent<EffectResources>();
            }
        }

        public virtual void ResetStats()
        {
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {
            if (Application.isEditor)
            {
                if (normallyValid.toggleAll)
                {
                    normallyValid = new TargetType(true);
                }
                else if (normallyValid.toggleNone)
                {
                    normallyValid = new TargetType(false);
                }
            }
        }

        public void ResetMainDamageList()
        {
            damageList = new List<Character.Damage>(originalDamageList);
        }
        public void ResetMainStatusList()
        {
            statusList = new List<StatusSpace.Status>(originalStatusList);
        }

        public int GetTotalDamage()
        {
            int totalDamage = 0;
            foreach (Character.Damage damage in damageList)
            {
                totalDamage += damage.amount;
            }

            return totalDamage;
        }

        public virtual void InvokeEffect(List<Target> targets, TargetType validTargets)
        {
            InvokeEffect(targets);
        }

        public virtual void InvokeEffect(List<Target> targets)
        {
        }

        public virtual bool CanDeliverThisEffect(Effect otherEffect)
        {
            return true;
        }
        public virtual bool CanBeTargetedBy(Effect otherEffect)
        {
            return true;
        }

        public virtual void ModifyViaEffect(Effect effect)
        {

        }

        public virtual void EndEffect()
        {
            if (spellAttachedTo != null && spellAttachedTo.deliveredByMain.Contains(this))
            {
                spellAttachedTo.deliveredByMain.Remove(this);
            }

            if (attachedEffect != null && attachedEffect.deliveredEffects.Contains(this))
            {
                attachedEffect.deliveredEffects.Remove(this);
            }
        }

        public virtual void IfTargetIsCreature(Target target)
        {

        }
        public virtual void IfTargetIsObject(Target target)
        {

        }
        public virtual void IfTargetIsWeapon(Target target)
        {

        }
        public virtual void IfTargetIsTile(Target target)
        {

        }

        public void AllTargets(TargetType targetType, Target target)
        {
            if (targetType.weapon)
            {
                IfTargetIsWeapon(target);
            }
            else if (targetType.tile)
            {
                IfTargetIsTile(target);
            }
            else if (targetType.creature)
            {
                IfTargetIsCreature(target);
            }
            else if (targetType.obj)
            {
                IfTargetIsObject(target);
            }
        }

        public void DamageTarget(Target target, Character.Damage damage)
        {
            SS.Util.TargetInterface.DamageTarget(target, damage, this);
        }
        public void DamageTarget(Target target, List<Character.Damage> damages)
        {
            foreach (Character.Damage damage in damages)
            {
                SS.Util.TargetInterface.DamageTarget(target, damage, this);
            }
        }

        public void AddToMainDamageList(Character.Damage damage)
        {
            damageList.Add(damage);
        }
        public void AddToOriginalDamageList(Character.Damage damage)
        {
            originalDamageList.Add(damage);
        }

        public void AddToMainStatusList(StatusSpace.Status status)
        {
            statusList.Add(status);
            status.applyingEffect = this;
        }
        public void AddToOriginalStatusList(StatusSpace.Status status)
        {
            originalStatusList.Add(status);
            status.applyingEffect = this;
        }

        public void RemoveStatusesFromList(List<Status> list, Effect basedOn)
        {
            List<Status> toRemove = new List<Status>();
            foreach (Status s in list)
            {
                if (s.applyingEffect == basedOn)
                {
                    toRemove.Add(s);
                }
            }
            foreach (Status s in toRemove)
            {
                list.Remove(s);
            }
        }

        public virtual void BehaviorWhenTargeting(Effect e) {}

        public virtual void HandleDeliveredEffects(Target target, List<Status> statusesMainStatusShouldApply) {}

        public virtual void HandleTargetingEffects(Target target) {}
    }
}
