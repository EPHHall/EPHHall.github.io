using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{ 
    [System.Serializable]
    public class Behavior_PathfindToTarget : AIBehavior
    {
        Pathfinding.Grid grid;
        Pathfinding.AStar aStar;

        public int withinXUnits;
        public List<Pathfinding.Node> foundPath = new List<Pathfinding.Node>();

        public int spacesMoved;

        private Transform target;


        public bool movementHappening;
        public bool movementComplete;

        public Behavior_PathfindToTarget(Agent agent, Pathfinding.Grid grid, Pathfinding.AStar aStar) : base(agent)
        {
            this.grid = grid;
            this.aStar = aStar;
            this.agent = agent;
            target = null;
        }
        public Behavior_PathfindToTarget(Agent agent, Pathfinding.Grid grid, Pathfinding.AStar aStar, Transform target) : base(agent)
        {
            this.grid = grid;
            this.aStar = aStar;
            this.agent = agent;
            this.target = target;
        }

        public override void InvokeBehavior(List<Spells.Spell> spells)
        {
            if (movementHappening || movementComplete) return;

            base.InvokeBehavior(spells);

            spacesMoved = 0;

            foundPath = new List<Pathfinding.Node>();

            Transform workingTarget = target;
            if (workingTarget == null && agent != null)
            {
                workingTarget = agent.mainTarget.transform;
            }
            if (workingTarget == null) return;

            foundPath = aStar.FindPath(agent.positionAtStartofTurn, workingTarget.position);

            if (foundPath != null)
            {
                //movementHappening = true;
                //agent.GetComponent<Character.NPCMovement>().StartMovement(foundPath, this, agent.characterStats.speed);
                
            
            
                //for (int i = 0; i < agent.characterStats.speed; i++)
                //{
                //    if (foundPath.Count == 0 || i >= foundPath.Count)
                //    {
                //        spacesMoved = agent.characterStats.speed;
                //        break;
                //    }

                //    agent.transform.position = foundPath[i].position;

                //    spacesMoved++;
                //    if (spacesMoved >= agent.characterStats.speed)
                //    {
                //        break;
                //    }
                //}
            }
        }

        public override bool WasBehaviorFulfilled()
        {
            //bool result = movementComplete;

            //if (result)
            //{
            //    movementComplete = false;
            //}

            //return result;






            //List<Vector2> positionsToCheck = new List<Vector2>();
            //positionsToCheck.Add(agent.mainTarget.transform.position);

            //bool result = false;
            //foreach (Vector2 position in Util.SS_AStar.GetPositionsWithinRadius(positionsToCheck, withinXUnits))
            //{
            //    if (position.Equals(agent.transform.position))
            //    {
            //        result = true;
            //        break;
            //    }
            //}

            //if (spacesMoved >= agent.characterStats.speed)
            //{
            //    result = true;
            //}

            //return result;



            return foundPath != null;
        }
    }
}
