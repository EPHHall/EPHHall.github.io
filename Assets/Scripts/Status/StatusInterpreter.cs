using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;
using SS.Character;
using SS.GameController;
using SS.AI;

namespace SS.StatusSpace
{
    public class StatusInterpreter
    {
        public enum DecrementBehavior
        {
            Do,
            Dont,
            CheckApplier,
            Delete
        }

        private static int controlledObjectIndex = -1;

        public static void InterpretStatuses(Target target, DecrementBehavior decrementBehavior, List<Status> statusesToInterpret)
        {
            if (statusesToInterpret.Count > 0)
            {
                //Debug.Log("Interpreting Statuses");
            }

            TargetType type = target.targetType;

            CharacterStats stats = null;
            bool characterStatsPresent = target.TryGetComponent<CharacterStats>(out stats);

            GameController.TurnTaker turnTaker = null;
            bool turnTakerPresent = target.TryGetComponent(out turnTaker);

            Item.Weapon weapon = null;
            bool weaponPresent = target.TryGetComponent<Item.Weapon>(out weapon);
            if (weaponPresent)
            {
                weapon.attack.main.ResetMainDamageList(true);
            }

            Agent agent = null;
            if (target.TryGetComponent<Agent>(out agent))
            {
                //Debug.Log("BuhBuh", target.gameObject);
                agent.ResetFactionsAndTargets();
            }

            controlledObjectIndex = -1;
            if (target.targetType.obj)
            {
                if (target.GetComponent<TurnTakerControlledObject>() != null /*&& TurnManager.instance.CurrentTurnTaker.Contains(target.GetComponent<TurnTakerControlledObject>())*/)
                {
                    //Debug.Log(1);

                    controlledObjectIndex = target.GetComponent<TurnTakerControlledObject>().EndControl();
                    //TurnManager.instance.CurrentTurnTaker.Remove(target.GetComponent<TurnTakerControlledObject>());
                }
            }

            List<Status> toRemove = new List<Status>();

            DecrementBehavior defaultBehavior = decrementBehavior;
            for (int i = 0; i < statusesToInterpret.Count; i++)
            {
                Status status = statusesToInterpret[i];
                decrementBehavior = defaultBehavior;

                if (decrementBehavior == DecrementBehavior.CheckApplier)
                {
                    TurnTaker tt = null;
                    if (status.applier != null && status.applier.TryGetComponent<TurnTaker>(out tt) && tt == TurnManager.instance.CurrentTurnTaker)
                    {
                        decrementBehavior = DecrementBehavior.Do;
                    }
                    else
                    {
                        decrementBehavior = DecrementBehavior.Dont;
                    }
                }

                if (decrementBehavior == DecrementBehavior.Do)
                {
                    status.duration--;
                }

                if (type.creature)
                {
                    InterpretStatus_Creature(statusesToInterpret[i], type, target, characterStatsPresent, turnTakerPresent, turnTaker);
                }
                else if (type.obj)
                {
                    InterpretStatus_Object(statusesToInterpret[i], type, target);
                }
                else if (type.weapon)
                {
                    InterpretStatus_Weapon(statusesToInterpret[i], type, target, weaponPresent, weapon);
                }
                else if (type.tile)
                {
                    InterpretStatus_Tile(statusesToInterpret[i], type, target);
                }

                foreach (Status s in status.statusesToApply)
                {
                    target.ApplyStatus(s, s.applyingEffect);
                }

                if (status.duration == 0)
                {
                    toRemove.Add(status);
                    //continue;
                }
            }

            if (weapon != null)
            {
                List<Status> toRemove2 = new List<Status>();

                foreach (Status status in weapon.statusesToInflict)
                {
                    if (decrementBehavior == DecrementBehavior.CheckApplier)
                    {
                        TurnTaker tt = null;
                        if (status.applier.TryGetComponent<TurnTaker>(out tt) && tt == TurnManager.instance.CurrentTurnTaker)
                        {
                            decrementBehavior = DecrementBehavior.Do;
                            status.duration--;
                        }
                        else
                        {
                            decrementBehavior = DecrementBehavior.Dont;
                        }

                        if (status.duration == 0)
                        {
                            toRemove2.Add(status);
                            //continue;
                        }
                    }
                }

                while (toRemove2.Count > 0)
                {
                    weapon.statusesToInflict.Remove(toRemove2[0]);
                    toRemove2.RemoveAt(0);
                }

                weapon.UpdateToInflict();
            }

            while (toRemove.Count > 0)
            {
                statusesToInterpret.Remove(toRemove[0]);
                toRemove.RemoveAt(0);
            }
        }

        private static void InterpretStatus_Creature(Status status, TargetType type, Target target, bool characterStatsPresent, bool turnTakerPresent, GameController.TurnTaker turnTaker)
        {
            if (status.statusName == Status.StatusName.FireDamage)
            {
                //Debug.Log("Ow ", target.gameObject);
                //Debug.Log(characterStatsPresent);
                //Debug.Log(turnTakerPresent);
                //Debug.Log(GameController.TurnManager.instance.CurrentTurnTaker == turnTaker);
                if (characterStatsPresent && turnTakerPresent && GameController.TurnManager.instance.CurrentTurnTaker == turnTaker)
                {
                    //TODO: I think these damages should probably be flagged as temporary but im not sure. If it gets to be an issue ill come back to it.
                    SS.Util.TargetInterface.DamageTargetAtEndOfTurn(target, new Damage(Damage.DamageType.Fire, status.magnitude, false), status.applyingEffect);
                }
            }
            else if (status.statusName == Status.StatusName.Possessed)
            {
                Agent agent = null;
                if (target.TryGetComponent<Agent>(out agent))
                {
                    agent.ResetFactionsAndTargets();

                    agent.targetFaction = Faction.FactionName.PlayerEnemyFaction;
                    agent.mainTarget = null;
                }
            }
            else if (status.statusName == Status.StatusName.ArcaneDamage)
            {
                if (characterStatsPresent && turnTakerPresent && GameController.TurnManager.instance.CurrentTurnTaker == turnTaker)
                {
                    SS.Util.TargetInterface.DamageTargetAtEndOfTurn(target, new Damage(Damage.DamageType.Arcane, status.magnitude, false), status.applyingEffect);
                }
            }
        }

        private static void InterpretStatus_Object(Status status, TargetType type, Target target)
        {
            if (status.statusName == Status.StatusName.FireDamage)
            {
                List<Vector2> positionsToCheck = new List<Vector2>();
                positionsToCheck.Add(target.transform.position);

                List<Target> targets = Util.CheckPositionsForTargets.CheckRadius(positionsToCheck, status.radius);

                foreach (Target t in targets)
                {
                    TurnTaker turnTaker = null;
                    //if (t.TryGetComponent<TurnTaker>(out turnTaker) && turnTaker == TurnManager.)
                    //{
                    //    Util.TargetInterface.DamageTarget(t, new Damage(Damage.DamageType.Fire, status.magnitude, false), status.applyingEffect);
                    //}
                }
            }
            else if (status.statusName == Status.StatusName.Possessed)
            {

            }
            else if (status.statusName == Status.StatusName.ArcaneDamage)
            {
                List<Vector2> positionsToCheck = new List<Vector2>();
                positionsToCheck.Add(target.transform.position);

                List<Target> targets = Util.CheckPositionsForTargets.CheckRadius(positionsToCheck, status.radius);

                foreach (Target t in targets)
                {
                    TurnTaker turnTaker = null;
                    //if (t.TryGetComponent<TurnTaker>(out turnTaker) && turnTaker == TurnManager.previousTurnTaker)
                    //{
                    //    Util.TargetInterface.DamageTarget(t, new Damage(Damage.DamageType.Arcane, status.magnitude, false), status.applyingEffect);
                    //}
                }
            }
            else if (status.statusName == Status.StatusName.Controlled)
            {

                if (target.GetComponent<TurnTakerControlledObject>() == null)
                {
                    target.gameObject.AddComponent<TurnTakerControlledObject>();
                }

                target.GetComponent<TurnTakerControlledObject>().Initialize(false);

                //if (!TurnManager.instance.CurrentTurnTaker.Contains(target.GetComponent<GameController.TurnTakerControlledObject>()))
                //{
                //    if (controlledObjectIndex != -1)
                //    {
                //        TurnManager.instance.CurrentTurnTaker.Insert(controlledObjectIndex, target.GetComponent<GameController.TurnTakerControlledObject>());
                //        Debug.Log(TurnManager.instance.CurrentTurnTaker.IndexOf(target.GetComponent<GameController.TurnTakerControlledObject>()), target.gameObject);
                //    }
                //    else
                //    {
                //        TurnManager.tm.AddNextTurnTaker(target.GetComponent<GameController.TurnTakerControlledObject>());
                //        Debug.Log(TurnManager.instance.CurrentTurnTaker.IndexOf(target.GetComponent<GameController.TurnTakerControlledObject>()), target.gameObject);
                //    }
                //}
            }
            else if (status.statusName == Status.StatusName.ControlledByEnemy)
            {
                if (target.GetComponent<TurnTakerControlledObject>() == null)
                {
                    target.gameObject.AddComponent<TurnTakerControlledObject>();
                }

                target.GetComponent<TurnTakerControlledObject>().Initialize(true);

                //if (!TurnManager.instance.CurrentTurnTaker.Contains(target.GetComponent<GameController.TurnTakerControlledObject>()))
                //{
                //    if (controlledObjectIndex != -1)
                //    {
                //        TurnManager.instance.CurrentTurnTaker.Insert(controlledObjectIndex, target.GetComponent<GameController.TurnTakerControlledObject>());
                //        //Debug.Log(TurnManager.instance.CurrentTurnTaker.IndexOf(target.GetComponent<GameController.TurnTakerControlledObject>()), target.gameObject);
                //    }
                //    else
                //    {
                //        TurnManager.tm.AddNextTurnTaker(target.GetComponent<GameController.TurnTakerControlledObject>());
                //        //Debug.Log(TurnManager.instance.CurrentTurnTaker.IndexOf(target.GetComponent<GameController.TurnTakerControlledObject>()), target.gameObject);
                //    }
                //}
            }
        }

        private static void InterpretStatus_Weapon(Status status, TargetType type, Target target, bool weaponPresent, Item.Weapon weapon)
        {
            if (status.statusName == Status.StatusName.FireDamage)
            {
                if (weaponPresent)
                {
                    weapon.attack.main.AddToMainDamageList(new Damage(Damage.DamageType.Fire, status.magnitude, true));
                }
            }
            else if (status.statusName == Status.StatusName.Possessed)
            {

            }
            else if(status.statusName == Status.StatusName.ArcaneDamage)
            {
                if (weaponPresent)
                {
                    weapon.attack.main.AddToMainDamageList(new Damage(Damage.DamageType.Arcane, status.magnitude, true));
                }
            }

        }

        private static void InterpretStatus_Tile(Status status, TargetType type, Target target)
        {
            if (status.statusName == Status.StatusName.FireDamage)
            {

            }
            else if (status.statusName == Status.StatusName.Possessed)
            {

            }
            else if (status.statusName == Status.StatusName.ArcaneDamage)
            {

            }
        }
    }
}
