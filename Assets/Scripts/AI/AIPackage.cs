using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{
    public class AIPackage : MonoBehaviour
    {
        //[System.Serializable]
        //private class BehaviorToReachAndReturnTo
        //{
        //    public int behaviorToReach = -1;
        //    public int behaviorToReturnTo = -1;
        //}
        [Space(5)]
        [Header("Can Modify")]
        public bool groupsVersion;

        [Space(5)]
        [Header("Don't Touch")]
        public Agent attachedAgent;
        public List<AIBehavior> behaviors = new List<AIBehavior>();
        public List<BehaviorGroup> behaviorGroups = new List<BehaviorGroup>();
        public Vector2 position = Vector2.negativeInfinity;

        //[SerializeField]
        //private List<BehaviorToReachAndReturnTo> reachThenReturnTo = new List<BehaviorToReachAndReturnTo>();

        public virtual void Awake()
        {
            
        }

        public virtual void Start()
        {
            
        }

        public void SetAttachedAgent(Agent agent)
        {
            attachedAgent = agent;
        }

        public virtual void InvokeAI()
        {
            int index = 0;
            AIBehavior currentBehavior = behaviors[index];

            int breakIfTooHigh = 0;
            while (currentBehavior != null)
            {
                currentBehavior.InvokeBehavior(attachedAgent.spells);

                if (currentBehavior.WasBehaviorFulfilled())
                {
                    index++;
                    currentBehavior.timesCompleted++;

                    if (index < behaviors.Count)
                    {
                        currentBehavior = behaviors[index];
                    }
                    else
                    {
                        currentBehavior = null;
                    }
                }

                if (breakIfTooHigh > 999)
                {
                    Debug.Log("Had to break");

                    break;
                }

                breakIfTooHigh++;
            }
        }

        public virtual void InvokeAI(bool TESTING)
        {
            int index = 0;
            BehaviorGroup currentGroup = behaviorGroups[index];
            while (currentGroup == null || currentGroup.turnOffGroupWhenCompleted && currentGroup.timesCompleted > 0)
            {
                if (index < behaviorGroups.Count)
                {
                    index++;
                }
                else
                {
                    currentGroup = null;
                    break;
                }

                currentGroup = behaviorGroups[index];
            }

            int breakIfTooHigh = 0;
            while (currentGroup != null)
            {
                foreach (AIBehavior currentBehavior in currentGroup.behaviors)
                {
                    currentBehavior.InvokeBehavior(attachedAgent.spells);
                }

                if (currentGroup.EvaluateGroup())
                {
                    index++;
                    foreach (AIBehavior currentBehavior in currentGroup.behaviors)
                    {
                        currentBehavior.timesCompleted++;
                    }

                    if (index < behaviorGroups.Count)
                    {
                        currentGroup = behaviorGroups[index];
                    }
                    else
                    {
                        currentGroup = null;
                    }
                }

                if (breakIfTooHigh > 199)
                {
                    Debug.Log("Had to break - Groups Version");

                    break;
                }

                breakIfTooHigh++;
            }
        }
    }
}
