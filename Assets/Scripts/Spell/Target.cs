using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;
using SS.GameController;

namespace SS.Spells
{
    [ExecuteAlways]
    public class Target : MonoBehaviour
    {
        //SS.Util.TargetInterface TargetInterface;
        public static bool preventClearingTargetsOnce;

        [System.Serializable]
        public class DamageStuff
        {
            public Spells.Target target;
            public Character.Damage damage;
            public Spells.Effect inflictor;

            public DamageStuff(Spells.Target target, Character.Damage damage, Spells.Effect inflictor)
            {
                this.target = target;
                this.damage = damage;
                this.inflictor = inflictor;
            }
        }

        public List<DamageStuff> inflictAtEndOfTur = new List<DamageStuff>();
        public List<DamageStuff> inflictWhenAnimationReaches = new List<DamageStuff>();

        public TargetType targetType;
        public int hp;
        public string targetName;
        public string id;

        public List<Status> statuses = new List<Status>();

        public static List<Target> selectedTargets = new List<Target>();
        public bool displaySelectedTargets;


        private void Update()
        {
            if (Application.isEditor)
            {
                if (targetType.toggleAll)
                {
                    targetType = new TargetType(true);
                }
                else if (targetType.toggleNone)
                {
                    targetType = new TargetType(false);
                }
            }

            if (displaySelectedTargets)
            {
                displaySelectedTargets = false;

                foreach (Target target in selectedTargets)
                {
                    Debug.Log(target.name);
                }
            }
        }

        public void InflictEndOfTurnDamage()
        {
            foreach (DamageStuff damageStuff in inflictAtEndOfTur)
            {
                Util.TargetInterface.DamageTarget(damageStuff.target, damageStuff.damage, damageStuff.inflictor);
            }

            inflictAtEndOfTur.Clear();
        }

        public void InflictOnAnimationHitDamage()
        {
            Debug.Log("In AnimationReached");
            foreach (DamageStuff damageStuff in inflictWhenAnimationReaches)
            {
                Debug.Log("In AnimationReached Loop");
                Util.TargetInterface.DamageTarget(damageStuff.target, damageStuff.damage, damageStuff.inflictor);
            }

            inflictWhenAnimationReaches.Clear();
        }

        public virtual void TakeDamage(int damage)
        {
            hp -= damage;
        }

        public void HandleStatuses(bool newRound, bool preventDecrement)
        {
            if (preventDecrement)
            {
                StatusInterpreter.InterpretStatuses(this, StatusInterpreter.DecrementBehavior.Dont, statuses);
                return;
            }

            StatusInterpreter.DecrementBehavior behavior;

            if (targetType.creature)
            {
                if (TurnManager.currentTurnTaker == GetComponent<TurnTaker>())
                    behavior = StatusInterpreter.DecrementBehavior.Do;
                else
                    behavior = StatusInterpreter.DecrementBehavior.Dont;

                StatusInterpreter.InterpretStatuses(this, behavior, statuses);
            }
            else if (targetType.obj)
            {
                if (newRound)
                    behavior = StatusInterpreter.DecrementBehavior.Do;
                else
                    behavior = StatusInterpreter.DecrementBehavior.Dont;

                StatusInterpreter.InterpretStatuses(this, StatusInterpreter.DecrementBehavior.CheckApplier, statuses);
            }
            else if (targetType.weapon)
            {
                StatusInterpreter.InterpretStatuses(this, StatusInterpreter.DecrementBehavior.CheckApplier, statuses);
            }
            else if (targetType.tile)
            {
                if (newRound)
                    behavior = StatusInterpreter.DecrementBehavior.Do;
                else
                    behavior = StatusInterpreter.DecrementBehavior.Dont;

                StatusInterpreter.InterpretStatuses(this, behavior, statuses);
            }
        }

        public void HandleStatus(bool newRound, bool preventDecrement, Status status)
        {
            if (preventDecrement)
            {
                StatusInterpreter.InterpretStatuses(this, StatusInterpreter.DecrementBehavior.Dont, statuses);
                return;
            }

            List<Status> statusToInterpret = new List<Status>();
            statusToInterpret.Add(status);

            StatusInterpreter.DecrementBehavior behavior;

            if (targetType.creature)
            {
                if (TurnManager.currentTurnTaker == GetComponent<TurnTaker>())
                    behavior = StatusInterpreter.DecrementBehavior.Do;
                else
                    behavior = StatusInterpreter.DecrementBehavior.Dont;

                StatusInterpreter.InterpretStatuses(this, behavior, statusToInterpret);
            }
            else if (targetType.obj)
            {
                if (newRound)
                    behavior = StatusInterpreter.DecrementBehavior.Do;
                else
                    behavior = StatusInterpreter.DecrementBehavior.Dont;

                StatusInterpreter.InterpretStatuses(this, StatusInterpreter.DecrementBehavior.CheckApplier, statusToInterpret);
            }
            else if (targetType.weapon)
            {
                StatusInterpreter.InterpretStatuses(this, StatusInterpreter.DecrementBehavior.CheckApplier, statusToInterpret);
            }
            else if (targetType.tile)
            {
                if (newRound)
                    behavior = StatusInterpreter.DecrementBehavior.Do;
                else
                    behavior = StatusInterpreter.DecrementBehavior.Dont;

                StatusInterpreter.InterpretStatuses(this, behavior, statusToInterpret);
            }
        }

        public virtual void ApplyStatus(Status.StatusName statusName, int magnitude, int duration, TurnTaker applier, Effect applyingEffect)
        {
            Status newStatus = new Status(statusName, magnitude, duration, applier);
            newStatus.applyingEffect = applyingEffect;
            statuses.Add(newStatus);
        }

        public virtual void ApplyStatus(Status status, Effect applyingEffect)
        {
            //make a copy so that the original status isn't affected
            Status newStatus = new Status(status);

            statuses.Add(newStatus);
            newStatus.applyingEffect = applyingEffect;
        }

        public void SelectThisButton()
        {
            if (!selectedTargets.Contains(this))
            {
                //Debug.Log("Should Select buitton");
                selectedTargets.Add(this);
            }
            else
            {
                //Debug.Log("Should Deselect buitton");
                selectedTargets.Remove(this);
            }
        }

        public void SelectTarget()
        {
            if (!selectedTargets.Contains(this))
            {
                selectedTargets.Add(this);
            }
        }
        public void SelectTarget(Target justThis)
        {
            ClearSelectedTargets();

            if (!selectedTargets.Contains(justThis))
            {
                selectedTargets.Add(justThis);
            }
        }

        public static void ClearSelectedTargets()
        {
            //Debug.Log("In Clear");

            if (preventClearingTargetsOnce)
            {
                preventClearingTargetsOnce = false;
            }
            else
            {
                selectedTargets.Clear();
            }
        }
    }
}
