using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;
using SS.Character;

namespace SS.Spells
{
    [ExecuteAlways]
    public class ArcaneBolt : Effect_Projection
    {
        public override void Awake()
        {
            base.Awake();

            manaCost = 2;
            spellPointCost = 2;

            originalDamageList.Clear();
            originalDamageList.Add(new Character.Damage(Character.Damage.DamageType.Arcane, 1));
            range = 8;

            ResetMainDamageList();

            actionPointCost = 1;

            duration = 0;
            toInstantiate = null;

            normallyValid = new TargetType(true, true, false, false);

            style = Style.InstantDamage;

            mainDamage = new Character.Damage(Character.Damage.DamageType.Arcane, 1);

            originalDamageList.Clear();
            originalDamageList.Add(mainDamage);
            range = 8;

            ResetMainDamageList();

            originalStatusList.Clear();
            ResetMainStatusList();
        }

        public override void Start()
        {
            base.Start();

            animationToPlay = animationObjectManager.arcaneBoltAnimation;
            soundEffect = resources.GetArcaneBoltAudio();
        }

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            HandleDeliveredAndTargeting(targets);

            //foreach (Effect effect in deliveredEffects)
            //{
            //    if (effect == null) continue;
            //    effect.InvokeEffect(targets);
            //}

            foreach (Target target in targetsCopy)
            {
                DamageTarget(target, damageList);

                foreach (Status status in statusList)
                {
                    target.ApplyStatus(status, this);
                }
            }

            EndInvoke();
        }
    }
}
