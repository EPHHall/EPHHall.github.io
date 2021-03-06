using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;

namespace SS.AI
{
    public class AIBehavior
    {
        public Agent agent;
        public int timesCompleted = 0;
        public AIPackage package;

        public AIBehavior(Agent agent)
        {
            this.agent = agent;
        }

        public virtual void InvokeBehavior(List<Spell> spells)
        {
        }
        public virtual void InvokeBehavior(List<Spell> spells, Target toTarget)
        {
        }

        public virtual bool WasBehaviorFulfilled()
        {
            return false;
        }
    }
}
