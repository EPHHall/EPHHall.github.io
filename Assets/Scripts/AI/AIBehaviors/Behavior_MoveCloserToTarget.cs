using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;

namespace SS.AI
{
    public class Behavior_MoveCloserToTarget : AIBehavior
    {
        public Behavior_MoveCloserToTarget(Agent agent) : base(agent)
        {

        }

        public override void InvokeBehavior(List<Spell> spells)
        {
            base.InvokeBehavior(spells);

            agent.closest = Vector2.positiveInfinity;
           
            //Find the positions close to the agent's target
            agent.movementPositions = AI_Utility.GetPotentialMovementPositions(agent.transform.position, agent.characterStats.speed);

            //First, try to move closer to the main target, if possible and if it exists. If not, then try to move to the closest target that is a member of the targetFaction

            //Find which position is closest to the main target
            if (agent.mainTarget != null)
            {
                AI_Utility.GetCloserPositions(agent.positionsCloserToTarget, agent.movementPositions, agent.transform.position, agent.mainTarget.transform.position);
                Vector2 potentialClosest = AI_Utility.FindClosestPosition(agent.positionsCloserToTarget, agent.mainTarget.transform.position);
                float distance = Vector2.Distance(potentialClosest, agent.mainTarget.transform.position);
                if (distance < Vector2.Distance(agent.closest, agent.mainTarget.transform.position))
                {
                    agent.closest = potentialClosest;
                }
            }
            else
            {
                //Find what targets are currently present in the scene; objects with faction scripts should also have target scripts
                Faction[] allTargets = GameObject.FindObjectsOfType<Faction>();
                Faction closest = null;

                foreach (Faction faction in allTargets)
                {
                    if (faction.factionName == agent.targetFaction && faction.gameObject != agent.gameObject)
                    {
                        if (closest == null)
                        {
                            closest = faction;
                        }
                        else
                        {
                            bool distanceSmaller = Vector2.Distance(agent.transform.position, faction.transform.position) < Vector2.Distance(agent.transform.position, closest.transform.position);
                            closest = distanceSmaller ? faction : closest;
                        }
                    }
                }

                Target target = null;
                if (closest != null) target = closest.GetComponent<Target>();

                //TODO
                if (target != null)
                {
                    AI_Utility.GetCloserPositions(agent.positionsCloserToTarget, agent.movementPositions, agent.transform.position, target.transform.position);
                    Vector2 potentialClosest = AI_Utility.FindClosestPosition(agent.positionsCloserToTarget, target.transform.position);
                    float distance = Vector2.Distance(potentialClosest, target.transform.position);
                    if (distance < Vector2.Distance(agent.closest, target.transform.position))
                    {
                        agent.closest = potentialClosest;
                    }
                }
            }

            //Move to that position
            //Using .Equals because != did not return false when closest = Vector2.positiveInfinity
            if (!agent.closest.Equals(Vector2.positiveInfinity))
                agent.SetPosition(agent.closest);
            else
                agent.closest = agent.transform.position;
        }

        public override bool WasBehaviorFulfilled()
        {
            return Mathf.Abs(agent.transform.position.x - agent.closest.x) <= .001f && Mathf.Abs(agent.transform.position.x - agent.closest.x) <= .001f;
        }
    }
}
