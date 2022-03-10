﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;

namespace SS.Spells
{
    [ExecuteAlways]
    public class SummonCreature : Effect_Summoning
    {
        public override void Awake()
        {
            base.Awake();

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

            GameController.TurnManager turnManager = FindObjectOfType<GameController.TurnManager>();

            if (useOverridePosition)
            {
                GameObject newDude = Instantiate(toInstantiate, overridePosition, Quaternion.identity);

                HandleDeliveredAndTargeting(targets, newDude);

                int index = turnManager.turnTakers.IndexOf(GameController.TurnManager.currentTurnTaker) + 1;
                turnManager.turnTakers.Insert(index, newDude.GetComponent<GameController.TurnTaker>());

                EndInvoke();
                return;
            }

            if (target.TryGetComponent<CastingTile>(out tile) && tile.GetObstacles().Count <= 0)
            {

                GameObject newDude = Instantiate(toInstantiate, target.transform.position, Quaternion.identity);

                HandleDeliveredAndTargeting(targets, newDude);

                int index = turnManager.turnTakers.IndexOf(GameController.TurnManager.currentTurnTaker) + 1;
                turnManager.turnTakers.Insert(index, newDude.GetComponent<GameController.TurnTaker>());

                //HandleDeliveredEffects(newDude.GetComponent<Target>(), null);
                //HandleTargetingEffects(newDude.GetComponent<Target>());
            }
            else
            {
                List<Vector2> toCheck = new List<Vector2>();
                toCheck.Add(target.transform.position);
                List<Vector2> emptySpaces = SS.Util.CheckPositionsForTargets.CheckForEmptySpaces(toCheck, 1);

                Vector2 closestSpace = new Vector2(int.MaxValue, int.MaxValue);
                if (emptySpaces.Count > 0)
                {
                    if (spellAttachedTo.caster != null)
                    {
                        foreach (Vector2 space in emptySpaces)
                        {
                            if (Vector2.Distance(space, spellAttachedTo.caster.transform.position) <
                               Vector2.Distance(closestSpace, spellAttachedTo.caster.transform.position))
                            {
                                closestSpace = space;
                            }
                        }
                    }
                    else 
                    {
                        closestSpace = emptySpaces[0];
                    }

                    GameObject newDude = Instantiate(toInstantiate, closestSpace, Quaternion.identity);

                    HandleDeliveredAndTargeting(targets, newDude);

                    int index = turnManager.turnTakers.IndexOf(GameController.TurnManager.currentTurnTaker) + 1;
                    turnManager.turnTakers.Insert(index, newDude.GetComponent<GameController.TurnTaker>());
                }
                else
                {
                    GameObject.FindObjectOfType<UI.UpdateText>().SetMessage("No empty space nearby!", Color.red);
                }
            }

            EndInvoke();
        }

        /*public override void HandleDeliveredEffects(Target target, List<Status> statusesMainStatusShouldApply)
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
        }*/
    }
}
