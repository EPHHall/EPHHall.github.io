using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    [ExecuteAlways]
    public class Inflame : Effect
    {
        public GameObject fireDamageEffect;
        public GameObject fireDamage;

        public override void Awake()
        {
            base.Awake();

            speed = 1;
            manaCost = 5;
            spellPointCost = 4;

            damage = 2;
            range = 1;

            actionPointCost = 1;

            duration = 1;
            toInstantiate = fireDamageEffect;

            normallyValid = new TargetType(true, true, true, true, false, true, true, true);
        }

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            fireDamage = Instantiate(toInstantiate);

            foreach (Target target in targets)
            {
                TargetType targetType = target.targetType;

                AllTargets(targetType, target);
            }

            foreach (Effect effect in deliveredEffects)
            {
                effect.InvokeEffect(targets);
            }
        }

        public override void IfTargetIsWeapon(Target target)
        {
            base.IfTargetIsWeapon(target);

            target.GetComponent<SS.Item.Weapon>().attack.deliveredByMain.Add(fireDamage.GetComponent<FireDamage>());
            fireDamage.GetComponent<FireDamage>().attachedSpell = target.GetComponent<SS.Item.Weapon>().attack;

            if (target.GetComponent<SS.GameController.TurnCounter>() == null)
            {
                target.gameObject.AddComponent<SS.GameController.TurnCounter>();
            }

            SS.GameController.TurnCounter.CountdownAndEffect cAndE = new GameController.TurnCounter.CountdownAndEffect
                (duration, fireDamage.GetComponent<Effect>());

            target.GetComponent<SS.GameController.TurnCounter>().countDowns.Add(cAndE);
        }

        public override void IfTargetIsAlly(Target target)
        {
            base.IfTargetIsAlly(target);
            target.ApplyStatus("Damage: Fire", damage);
        }

        public override void IfTargetIsEnemy(Target target)
        {
            base.IfTargetIsEnemy(target);
            target.ApplyStatus("Damage: Fire", damage);
        }

        public override void IfTargetIsStructure(Target target)
        {
            base.IfTargetIsStructure(target);
            target.ApplyStatus("Damage: Fire", damage);
        }

        public override void IfTargetIsTile(Target target)
        {
            base.IfTargetIsTile(target);
            target.ApplyStatus("Damage: Fire", damage);
        }

        public override void TargetGivenEffect(Effect target)
        {
            base.TargetGivenEffect(target);

            fireDamage = Instantiate(toInstantiate);

            target.deliveredEffects.Add(fireDamage.GetComponent<FireDamage>());
            fireDamage.GetComponent<FireDamage>().attachedEffect = target;

            if (target.GetComponent<SS.GameController.TurnCounter>() == null)
            {
                target.gameObject.AddComponent<SS.GameController.TurnCounter>();
            }

            SS.GameController.TurnCounter.CountdownAndEffect cAndE = new GameController.TurnCounter.CountdownAndEffect
                (duration, fireDamage.GetComponent<Effect>());

            target.GetComponent<SS.GameController.TurnCounter>().countDowns.Add(cAndE);
        }

        //public override void IfTargetIsEffect(Target target)
        //{
        //    base.IfTargetIsEffect(target);
        //}
    }
}
