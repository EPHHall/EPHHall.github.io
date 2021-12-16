using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;
using SS.Character;

namespace SS.Spells
{
    public class Effect : MonoBehaviour
    {
        public static EffectResources resources;

        public enum EffectType
        {
            Mutation,
            Summoning,
            Enchanting,
            Projection,
            Possession
        }

        public enum Style
        {
            InstantDamage,
            DamageOverTime,
            Utility
        }

        public class BehaviorPackage
        {
            public Effect effect;
            public Damage damage;
            public Status status;
            public List<Target> targets;
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
        public Character.Damage mainDamage;
        public List<SS.Character.Damage> damageList;
        public List<SS.Character.Damage> originalDamageList;
        public Status mainStatus;
        public List<SS.StatusSpace.Status> statusList;
        public List<SS.StatusSpace.Status> originalStatusList;

        public int range;
        public int radius;
        public int duration;//in turns
        public List<string> keywords;

        public Spell spellAttachedTo;

        public string effectName;

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
        public EffectType type;

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

            foreach (Status status in originalStatusList)
            {
                status.ResetStatus();
            }
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

        public void EndInvoke()
        {
            ResetMainStatusList();
            ResetMainDamageList();
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
            if (damage.type == Damage.DamageType.None) return;

            damageList.Add(damage);
        }
        public void AddToOriginalDamageList(Character.Damage damage, Item.Weapon weapon)
        {
            damage.weapon = weapon;
            originalDamageList.Add(damage);
        }
        public void RemoveFromOriginalDamageList(Character.Damage damage)
        {
            originalDamageList.Remove(damage);
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

        public virtual void BehaviorWhenTargeting(BehaviorPackage package)
        {
            Effect.EffectType eType = package.effect.type;
            switch(eType)
            {
                case EffectType.Enchanting:
                    BehaviorWhenTargeting_Enchanting(package);
                    break;
                case EffectType.Mutation:
                    BehaviorWhenTargeting_Mutation(package);
                    break;
                case EffectType.Possession:
                    BehaviorWhenTargeting_Possession(package);
                    break;
                case EffectType.Projection:
                    BehaviorWhenTargeting_Projection(package);
                    break;
                case EffectType.Summoning:
                    BehaviorWhenTargeting_Summoning(package);
                    break;
            }
        }
        public virtual void BehaviorWhenDelivered(BehaviorPackage package)
        {
            Effect.EffectType eType = package.effect.type;
            switch (eType)
            {
                case EffectType.Enchanting:
                    BehaviorWhenDelivered_Enchanting(package);
                    break;
                case EffectType.Mutation:
                    BehaviorWhenDelivered_Mutation(package);
                    break;
                case EffectType.Possession:
                    BehaviorWhenDelivered_Possession(package);
                    break;
                case EffectType.Projection:
                    BehaviorWhenDelivered_Projection(package);
                    break;
                case EffectType.Summoning:
                    BehaviorWhenDelivered_Summoning(package);
                    break;
            }
        }

        public virtual void BehaviorWhenTargeting_Enchanting(BehaviorPackage package){}
        public virtual void BehaviorWhenTargeting_Mutation(BehaviorPackage package){}
        public virtual void BehaviorWhenTargeting_Possession(BehaviorPackage package){}
        public virtual void BehaviorWhenTargeting_Projection(BehaviorPackage package){}
        public virtual void BehaviorWhenTargeting_Summoning(BehaviorPackage package){}

        public virtual void BehaviorWhenDelivered_Enchanting(BehaviorPackage package) { }
        public virtual void BehaviorWhenDelivered_Mutation(BehaviorPackage package) { }
        public virtual void BehaviorWhenDelivered_Possession(BehaviorPackage package) { }
        public virtual void BehaviorWhenDelivered_Projection(BehaviorPackage package) { }
        public virtual void BehaviorWhenDelivered_Summoning(BehaviorPackage package) { }

        //public virtual void HandleDeliveredEffects(Target target, List<Status> statusesMainStatusShouldApply) {}

        //public virtual void HandleTargetingEffects(Target target) {}
    }
}
