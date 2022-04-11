using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{
    public class Package_PathfindCloser : AIPackage
    {
        public Transform target = null;
        public Animation.MovementAnimation movementAnimation; //Child of teh package object.

        public override void Start()
        {
            base.Start();

            groupsVersion = true;

            BehaviorGroup initialStage = new BehaviorGroup();
            initialStage.behaviors.Add(new Behavior_PathfindToTarget(attachedAgent, FindObjectOfType<Pathfinding.Grid>(), FindObjectOfType<Pathfinding.AStar>(), target));
            BehaviorGroup stage2 = new BehaviorGroup();
            stage2.behaviors.Add(new Behavior_WaitForMovementAnim(attachedAgent, FindObjectOfType<Pathfinding.Grid>()));

            behaviorGroups.Add(initialStage);
            behaviorGroups.Add(stage2);

            //behaviors.Add(new Behavior_PathfindToTarget(attachedAgent, FindObjectOfType<Pathfinding.Grid>(), FindObjectOfType<Pathfinding.AStar>(), target));
        }
    }
}