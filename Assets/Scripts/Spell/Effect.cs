using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    public class Effect : MonoBehaviour
    {
        [Space(5)]
        [Header("Behavior if Main")]
        public bool targetable;

        [Space(5)]
        [Header("Stats")]
        public int speed;
        public int manaCost;
        public int spellPointCost;
        public int actionPointCost;

        public int damage;
        public int range;
        public int duration;//in turns
        public List<string> keywords;

        [Space(5)]
        [Header("Casting Vars")]
        public GameObject toInstantiate;

        public TargetType normallyValid = new TargetType(true);
        public List<Effect> deliveredEffects = new List<Effect>();

        [Space(5)]
        [Header("If Being Delivered")]
        public Spell attachedSpell;
        public Effect attachedEffect;

        [Space(5)]
        [Header("Misc")]
        public Sprite icon;




        public virtual void Awake()
        {

        }

        public virtual void ResetStats()
        {
        }

        public virtual void Start()
        {

        }

        private void Update()
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

        public virtual void InvokeEffect(List<Target> targets, TargetType validTargets)
        {
            InvokeEffect(targets);
        }

        public virtual void InvokeEffect(List<Target> targets)
        {

        }

        public virtual void EndEffect()
        {
            if (attachedSpell != null && attachedSpell.deliveredByMain.Contains(this))
            {
                attachedSpell.deliveredByMain.Remove(this);
            }

            if (attachedEffect != null && attachedEffect.deliveredEffects.Contains(this))
            {
                attachedEffect.deliveredEffects.Remove(this);
            }
        }

        public virtual void IfTargetIsWeapon(Target target)
        {
        }
        public virtual void IfTargetIsEnemy(Target target)
        {
        }
        public virtual void IfTargetIsAlly(Target target)
        {
        }
        public virtual void IfTargetIsStructure(Target target)
        {
        }
        public virtual void IfTargetIsSpell(Target target)
        {
        }
        public virtual void IfTargetIsTile(Target target)
        {
        }
        public virtual void IfTargetIsItem(Target target)
        {
        }
        public virtual void TargetGivenEffect(Effect target)
        {
        }

        public void AllTargets(TargetType targetType, Target target)
        {
            if (targetType.weapon)
            {
                IfTargetIsWeapon(target);
            }
            else if (targetType.ally)
            {
                IfTargetIsAlly(target);
            }
            else if (targetType.enemy)
            {
                IfTargetIsEnemy(target);
            }
            else if (targetType.structure)
            {
                IfTargetIsStructure(target);
            }
            else if (targetType.item)
            {
                IfTargetIsItem(target);
            }
            else if (targetType.spell)
            {
                IfTargetIsSpell(target);
            }
            else if (targetType.tile)
            {
                IfTargetIsTile(target);
            }
        }

        public void DamageTarget(Target target, int damage)
        {
            SS.Util.TargetInterface.DamageTarget(target, damage);
        }
    }
}
