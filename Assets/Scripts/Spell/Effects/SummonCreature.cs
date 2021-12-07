using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;

namespace SS.Spells
{
    [ExecuteAlways]
    public class SummonCreature : Effect
    {
        public override void Awake()
        {
            base.Awake();

            speed = 1;
            manaCost = 5;
            spellPointCost = 4;
            baseDamage = 0;

            originalDamageList.Clear();
            ResetMainDamageList();

            range = 1;
            radius = 1;

            actionPointCost = 1;

            duration = 2;

            normallyValid = new TargetType(true, true, true, true);

            style = Style.Utility;
        }

        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            Target target = targets[0];
            CastingTile tile = null;
            if (target.TryGetComponent<CastingTile>(out tile) && tile.targets.Count <= 0)
            {
                GameController.TurnManager turnManager = FindObjectOfType<GameController.TurnManager>();

                GameObject newDude = Instantiate(toInstantiate, target.transform.position, Quaternion.identity);

                int index = turnManager.turnTakers.IndexOf(GameController.TurnManager.currentTurnTaker) + 1;
                turnManager.turnTakers.Insert(index, newDude.GetComponent<GameController.TurnTaker>());

                HandleDeliveredEffects(newDude.GetComponent<Target>(), null);
                HandleTargetingEffects(newDude.GetComponent<Target>());
            }
            else
            {
                GameObject.FindObjectOfType<UI.UpdateText>().SetMessage("Summon on empty space!", Color.red);
            }
        }

        public override void HandleDeliveredEffects(Target target, List<Status> statusesMainStatusShouldApply)
        {
            foreach (Effect effect in deliveredEffects)
            {
                if (effect == null) continue;

                Effect melee = null;
                if (target.transform.Find("Melee Attack") != null && target.transform.Find("Melee Attack").GetComponent<Spell>() != null)
                {
                    melee = target.transform.Find("Melee Attack").GetComponent<Spell>().main;
                }

                foreach (Status status in effect.statusList)
                {
                    Status newStatus = new Status(status);
                    newStatus.unarmedOnly = true;

                    if (melee != null)
                        melee.AddToMainStatusList(newStatus);
                }
            }
        }

        public override void HandleTargetingEffects(Target target)
        {
            base.HandleTargetingEffects(target);

            foreach (Effect effect in targetMeEffects)
            {
                if (effect == null) continue;

                foreach (Status status in effect.statusList)
                {
                    Status newStatus = new Status(status);

                    target.ApplyStatus(newStatus, this);
                }
            }
        }
    }
}
