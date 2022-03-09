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

        public bool foundIt = false;

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
            base.InvokeBehavior(spells);

            foundIt = false;

            spacesMoved = 0;

            foundPath = new List<Pathfinding.Node>();

            Transform workingTarget = target;
            if (workingTarget == null)
            {
                workingTarget = agent.mainTarget.transform;
            }

            foundPath = aStar.FindPath(agent.positionAtStartofTurn, workingTarget.position);
            //aStar.targetPos = agent.mainTarget.transform;
            //aStar.startPos = agent.transform;
            //foundPath = grid.path;

            if (foundPath != null)
            {
                foundIt = true;

                for (int i = 0; i < agent.characterStats.speed; i++)
                {
                    if (foundPath.Count == 0 || i >= foundPath.Count)
                    {
                        spacesMoved = agent.characterStats.speed;
                        break;
                    }

                    agent.transform.position = foundPath[i].position;

                    spacesMoved++;
                    if (spacesMoved >= agent.characterStats.speed)
                    {
                        break;
                    }
                }
            }
        }

        public override bool WasBehaviorFulfilled()
        {
            List<Vector2> positionsToCheck = new List<Vector2>();
            positionsToCheck.Add(agent.mainTarget.transform.position);

            bool result = false;
            foreach (Vector2 position in Util.SS_AStar.GetPositionsWithinRadius(positionsToCheck, withinXUnits))
            {
                if (position.Equals(agent.transform.position))
                {
                    result = true;
                    break;
                }
            }

            if (spacesMoved >= agent.characterStats.speed)
            {
                result = true;
            }

            return result;
        }
    }
}
