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
        }

        public override bool WasBehaviorFulfilled()
        {
            return true;
        }
    }
}
