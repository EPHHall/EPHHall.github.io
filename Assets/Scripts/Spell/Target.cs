using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public virtual void ApplyStatus(string status, int magnitude)
        {
            statuses.Add(new Status(status, magnitude));
        }

        public virtual void ApplyStatus(Status status)
        {
            statuses.Add(status);
        }

        public void SelectThisButton()
        {
            if (!selectedTargets.Contains(this))
            {
            Debug.Log("Should Select buitton");
                selectedTargets.Add(this);
            }
            else
            {
            Debug.Log("Should Deselect buitton");
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

        public static void ClearSelectedTargets()
        {
            Debug.Log("In Clear");

            selectedTargets.Clear();
        }
    }
}
