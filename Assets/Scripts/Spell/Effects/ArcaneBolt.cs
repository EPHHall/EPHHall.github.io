using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;
using SS.Character;

namespace SS.Spells
{
    [ExecuteAlways]
    public class ArcaneBolt : Effect
    {
        public override void Awake()
        {
            base.Awake();

            speed = 1;
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

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            List<Damage> toRemove = new List<Damage>();

            foreach (Effect effect in deliveredEffects)
            {
                if (effect == null) continue;
                effect.InvokeEffect(targets);
            }
            foreach (Effect effect in targetMeEffects)
            {
                if (effect == null) continue;

                if (effect.style == Style.DamageOverTime)
                {
                    Damage damage = new Damage(effect.mainDamage.type, effect.mainStatus.magnitude);
                    AddToMainDamageList(damage);
                    toRemove.Add(damage);
                }
                else if (effect.style == Style.InstantDamage)
                {
                    AddToMainDamageList(effect.mainDamage);
                    toRemove.Add(effect.mainDamage);
                }
            }

            foreach (Target target in targets)
            {
                DamageTarget(target, damageList);
            }

            foreach (Damage d in toRemove)
            {
                if (damageList.Contains(d))
                {
                    damageList.Remove(d);
                }
            }
        }
    }
}
