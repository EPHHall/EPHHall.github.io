using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{
    public class Behavior_IdleUntilAttacked : AIBehavior
    {
        int prevHP;
        int currentHP;

        int maxUses = 1;

        public Behavior_IdleUntilAttacked(Agent agent) : base(agent)
        {
            this.agent = agent;

            //Debug.Log(agent);

            prevHP = agent.characterStats.hp;
        }

        public override void InvokeBehavior(List<Spells.Spell> spells)
        {
            base.InvokeBehavior(spells);

            currentHP = agent.characterStats.hp;
        }

        public override bool WasBehaviorFulfilled()
        {
            bool result = (currentHP != prevHP) || (timesCompleted >= maxUses);



            prevHP = currentHP;

            return result;
        }
    }
}
