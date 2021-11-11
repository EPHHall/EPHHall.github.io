using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;

namespace SS.AI
{
    public class AIBehavior
    {
        public Agent agent;

        public virtual void InvokeBehavior(List<Spell> spells)
        {

        }

        public virtual bool WasBehaviorFulfilled()
        {
            return false;
        }
    }
}
