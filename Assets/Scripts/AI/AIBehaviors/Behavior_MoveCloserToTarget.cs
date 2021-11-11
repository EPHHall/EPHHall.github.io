using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;

namespace SS.AI
{
    public class Behavior_MoveCloserToTarget : AIBehavior
    {
        public override void InvokeBehavior(List<Spell> spells)
        {
            base.InvokeBehavior(spells);

            agent.closest = Vector2.positiveInfinity;
           
            //Find the positions close to the agent's target
            agent.movementPositions = AI_Utility.GetPotentialMovementPositions(agent.transform.position, agent.characterStats.speed);

            //Find which position is closest
            AI_Utility.GetCloserPositions(agent.positionsCloserToTarget, agent.movementPositions, agent.transform.position, agent.mainTarget.transform.position);
            Vector2 potentialClosest = AI_Utility.FindClosestPosition(agent.positionsCloserToTarget, agent.mainTarget.transform.position);
            float distance = Vector2.Distance(potentialClosest, agent.mainTarget.transform.position);
            if (distance < Vector2.Distance(agent.closest, agent.mainTarget.transform.position))
            {
                agent.closest = potentialClosest;
            }

            //Move to that position
            if(agent.closest != Vector2.positiveInfinity)
                agent.SetPosition(agent.closest);
        }

        public override bool WasBehaviorFulfilled()
        {
            return Mathf.Abs(agent.transform.position.x - agent.closest.x) <= .001f && Mathf.Abs(agent.transform.position.x - agent.closest.x) <= .001f;
        }
    }
}
