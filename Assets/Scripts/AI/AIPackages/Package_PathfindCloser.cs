using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{
    public class Package_PathfindCloser : AIPackage
    {
        public Transform target = null;

        public override void Awake()
        {
            base.Awake();

            groupsVersion = true;

            BehaviorGroup initialStage = new BehaviorGroup();
            initialStage.behaviors.Add(new Behavior_PathfindToTarget(attachedAgent, FindObjectOfType<Pathfinding.Grid>(), FindObjectOfType<Pathfinding.AStar>(), target));

            behaviorGroups.Add(initialStage);

            //behaviors.Add(new Behavior_PathfindToTarget(attachedAgent, FindObjectOfType<Pathfinding.Grid>(), FindObjectOfType<Pathfinding.AStar>(), target));
        }
    }
}