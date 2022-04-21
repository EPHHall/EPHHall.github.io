using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{
    [System.Serializable]
    public class Behavior_WaitForMovementAnim : AIBehavior
    {
        Pathfinding.Grid grid;

        bool movementComplete = false;
        bool ranOnce = false;

        public Behavior_WaitForMovementAnim(Agent agent, Pathfinding.Grid grid) : base(agent)
        {
            this.grid = grid;
        }

        public override void InvokeBehavior(List<Spells.Spell> spells)
        {
            if (!ranOnce)
            {
                //Debug.Log("");
            }

            if (ranOnce)
            {
                return;
            }
            
            ranOnce = true;

            agent.GetComponent<Character.NPCMovement>().StartMovement(grid.path, this, agent.characterStats.speed);
        }

        public void FinishMovement()
        {
            movementComplete = true;
        }

        public override bool WasBehaviorFulfilled()
        {
            bool result = movementComplete;

            if (result)
            {
                //Debug.Log("");

                movementComplete = false;
                ranOnce = false;
                grid.path = null;
            }

            return result;
        }
    }
}