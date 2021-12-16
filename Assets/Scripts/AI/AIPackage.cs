using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{
    public class AIPackage : MonoBehaviour
    {
        public Agent attachedAgent;
        public List<AIBehavior> behaviors = new List<AIBehavior>();

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
                    if (index < behaviors.Count)
                    {
                        currentBehavior = behaviors[index];
                    }
                    else
                    {
                        currentBehavior = null;
                    }
                }

                if (breakIfTooHigh > 99)
                {
                    Debug.Log("Had to break");
                    
                    break;
                }

                breakIfTooHigh++;
            }
        }
    }
}
