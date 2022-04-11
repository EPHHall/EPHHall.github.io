using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{
    [System.Serializable]
    public class BehaviorGroup
    {
        //public bool turnOffGroupWhenCompleted = false;
        public BehaviorGroup turnOffWhenGroupCompleted = null;
        public int timesCompleted = 0;
        public int timesTillFullyCompleted = -1;

        public bool fullyCompleted = false;
        public int goTo = -1;

        public enum AndOr
        {
            And,
            Or
        }

        public AndOr andOr = AndOr.Or;
        public List<AIBehavior> behaviors = new List<AIBehavior>();

        public bool EvaluateGroup()
        {
            bool result = behaviors[0].WasBehaviorFulfilled();

            if (andOr == AndOr.Or)
            {
                //I don;t think it should matter that the first behavior will be counted twice.
                foreach (AIBehavior behavior in behaviors)
                {
                    result = result || behavior.WasBehaviorFulfilled();
                }
            }
            else
            {
                foreach (AIBehavior behavior in behaviors)
                {
                    result = result && behavior.WasBehaviorFulfilled();
                }
            }

            if (result)
            {
                timesCompleted++;
            }

            return result;
        }

        public bool EvaluateCompletion()
        {
            if (timesTillFullyCompleted >= 0 && timesCompleted >= timesTillFullyCompleted)
            {
                fullyCompleted = true;
            }

            if (turnOffWhenGroupCompleted != null && turnOffWhenGroupCompleted.fullyCompleted)
            {
                fullyCompleted = true;
            }

            return fullyCompleted;
        }
    }
}
