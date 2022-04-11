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
        public bool run;
        public BehaviorGroup currentGroup;
        public int index = 0;
        public bool mainBehaviors;
        public BehaviorGroup waitForAnimations;

        //[SerializeField]
        //private List<BehaviorToReachAndReturnTo> reachThenReturnTo = new List<BehaviorToReachAndReturnTo>();

        public virtual void Awake()
        {
        }

        public virtual void Start()
        {
            waitForAnimations = new BehaviorGroup();
            waitForAnimations.behaviors.Add(new Behavior_WaitForAnimations(attachedAgent));
        }

        private void Update()
        {
            if (run)
            {
                if (mainBehaviors)
                {
                    RunAI();
                    mainBehaviors = false;
                }
                else
                {
                    if (RunCleanup())
                    {
                        mainBehaviors = true;
                    }
                }
            }
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
            run = true;
            mainBehaviors = true;

            index = 0;
            currentGroup = behaviorGroups[index];
            while (currentGroup == null || currentGroup.EvaluateCompletion())
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

            //int breakIfTooHigh = 0;
            //while (currentGroup != null)
            //{
            //    foreach (AIBehavior currentBehavior in currentGroup.behaviors)
            //    {
            //        currentBehavior.InvokeBehavior(attachedAgent.spells);
            //    }

            //    if (currentGroup.EvaluateGroup())
            //    {
            //        index++;
            //        foreach (AIBehavior currentBehavior in currentGroup.behaviors)
            //        {
            //            currentBehavior.timesCompleted++;
            //        }

            //        if (index < behaviorGroups.Count)
            //        {
            //            currentGroup = behaviorGroups[index];
            //        }
            //        else
            //        {
            //            currentGroup = null;
            //        }
            //    }

            //    if (breakIfTooHigh > 199)
            //    {
            //        Debug.Log("Had to break - Groups Version");

            //        break;
            //    }

            //    breakIfTooHigh++;
            //}
        }

        public virtual void RunAI()
        {
            //if tempBehavior != null, then run temp behavior instead of the behavior group
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

                currentGroup.EvaluateCompletion();
            }

            if (currentGroup.goTo >= 0 && !currentGroup.fullyCompleted)
            {
                index = currentGroup.goTo;

                if (index < behaviorGroups.Count)
                {
                    currentGroup = behaviorGroups[index];
                }
            }

            if (index < behaviorGroups.Count)
            {
                currentGroup = behaviorGroups[index];
            }
            else
            {
                currentGroup = null;
                run = false;
            }
        }

        public virtual bool RunCleanup()
        {
            foreach (AIBehavior currentBehavior in waitForAnimations.behaviors)
            {
                currentBehavior.InvokeBehavior(attachedAgent.spells);
            }

            if (waitForAnimations.EvaluateGroup())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
