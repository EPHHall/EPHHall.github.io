using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;
using SS.Animation;

namespace SS.Spells
{
    [ExecuteAlways]
    public class MeleeAttack : Effect
    {
        public override void Awake()
        {
            base.Awake();
            ResetStats();
        }

        public override void Start()
        {
            base.Start();

            animationToPlay = animationObjectManager.attackAnimation;
            soundEffect = resources.GetAttackAudio();
        }

        public override void ResetStats()
        {
            range = 1;

            actionPointCost = 1;

            duration = 0;
            toInstantiate = null;

            normallyValid = new TargetType(true, true, false, false);

            originalDamageList.Clear();
            originalDamageList.Add(new Character.Damage(Character.Damage.DamageType.Physical, 0, false));
            ResetMainDamageList(false);

            originalStatusList.Clear();
            ResetMainDamageList(false);

            style = Style.InstantDamage;
        }

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            foreach (Target target in targets)
            {
                //Debug.Log("Should be damaging. " + damageList.Count);

                DamageTarget(target, damageList);

                foreach (Status s in statusList)
                {
                    target.ApplyStatus(s, this);
                }
            }

            //Debug.Log(deliveredEffects.Count);
            foreach (Effect effect in deliveredEffects)
            {
                Debug.Log(effect.name);
                effect.InvokeEffect(targets);
            }
        }
    }
}
