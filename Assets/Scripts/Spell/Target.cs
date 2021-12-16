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

        public TargetType targetType;
        public int hp;
        public string targetName;

        public List<Status> statuses;

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

        public virtual void TakeDamage(int damage)
        {
            hp -= damage;
        }

        public void HandleStatuses(bool newRound, bool preventDecrement)
        {
            if (preventDecrement)
            {
                StatusInterpreter.InterpretStatuses(this, StatusInterpreter.DecrementBehavior.Dont);
                return;
            }

            StatusInterpreter.DecrementBehavior behavior;

            if (targetType.creature)
            {
                if (TurnManager.currentTurnTaker == GetComponent<TurnTaker>())
                    behavior = StatusInterpreter.DecrementBehavior.Do;
                else
                    behavior = StatusInterpreter.DecrementBehavior.Dont;

                StatusInterpreter.InterpretStatuses(this, behavior);
            }
            else if (targetType.obj)
            {
                if (newRound)
                    behavior = StatusInterpreter.DecrementBehavior.Do;
                else
                    behavior = StatusInterpreter.DecrementBehavior.Dont;

                StatusInterpreter.InterpretStatuses(this, StatusInterpreter.DecrementBehavior.CheckApplier);
            }
            else if (targetType.weapon)
            {
                StatusInterpreter.InterpretStatuses(this, StatusInterpreter.DecrementBehavior.CheckApplier);
            }
            else if (targetType.tile)
            {
                if (newRound)
                    behavior = StatusInterpreter.DecrementBehavior.Do;
                else
                    behavior = StatusInterpreter.DecrementBehavior.Dont;

                StatusInterpreter.InterpretStatuses(this, behavior);
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

            selectedTargets.Clear();
        }
    }
}
