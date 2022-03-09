using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{
    [System.Serializable]
    public class Behavior_IdleUntilTargetEntersRadius : AIBehavior
    {
        int radius = 1;
        List<Vector2> positions = new List<Vector2>();

        int maxTimesCompleted = 1;

        public Behavior_IdleUntilTargetEntersRadius(Agent agent, int radius) : base(agent)
        {
            this.radius = radius;
            this.agent = agent;
        }

        public override void InvokeBehavior(List<Spells.Spell> spells)
        {
            if (timesCompleted >= maxTimesCompleted)
                return;

            base.InvokeBehavior(spells);

            positions = new List<Vector2>();
            positions.Add(agent.transform.position);

            positions = SS.Util.SS_AStar.GetAllPositionsWithinRadius(positions, radius);
        }

        public override bool WasBehaviorFulfilled()
        {
            if (timesCompleted >= maxTimesCompleted)
                return true;

            bool result = false;

            foreach (Vector2 position in positions)
            {
                if (position.Equals(agent.mainTarget.transform.position))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}