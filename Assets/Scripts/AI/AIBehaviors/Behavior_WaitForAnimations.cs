using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SS.AI
{
    [System.Serializable]
    public class Behavior_WaitForAnimations : AIBehavior
    {
        bool animationsComplete = false;
        bool ranOnce = false;

        Animation.AnimationManager manager;

        public Behavior_WaitForAnimations(Agent agent) : base(agent)
        {
            //Debug.Log("WaitForAnimations.WaitForAnimations");


            manager = GameObject.FindObjectOfType<Animation.AnimationManager>();

            manager.AddWaitFor(this);
        }

        public override void InvokeBehavior(List<Spells.Spell> spells)
        {
            if (ranOnce) return;

            ranOnce = true;

            manager.RunAnimations();
        }

        public void FinishWaiting()
        {
            //Debug.Log("WaitForAnimations.FinishWaiting");

            animationsComplete = true;
        }

        public override bool WasBehaviorFulfilled()
        {
            bool result = animationsComplete;

            if (result)
            {
                animationsComplete = false;
                ranOnce = false;
            }

            return result;
        }
    }
}