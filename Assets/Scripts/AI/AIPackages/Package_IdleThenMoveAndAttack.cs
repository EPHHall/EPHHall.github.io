using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{
    public class Package_IdleThenMoveAndAttack : AIPackage
    {
        [Space(5)]
        [Header("Can Modify")]
        public int radius = 1;
        public Transform target;
        public Animation.MovementAnimation movementAnimation; //Child of teh package object.

        public override void Start()
        {
            base.Start();

            groupsVersion = true;

            BehaviorGroup initialStage = new BehaviorGroup();
            initialStage.andOr = BehaviorGroup.AndOr.Or;
            initialStage.behaviors.Add(new Behavior_IdleUntilTargetEntersRadius(attachedAgent, radius));
            initialStage.behaviors.Add(new Behavior_IdleUntilAttacked(attachedAgent));

            BehaviorGroup stage2 = new BehaviorGroup();
            stage2.behaviors.Add(new Behavior_PathfindToTarget(attachedAgent, FindObjectOfType<Pathfinding.Grid>(), FindObjectOfType<Pathfinding.AStar>(), target));

            BehaviorGroup stage3 = new BehaviorGroup();
            stage3.behaviors.Add(new Behavior_DoMostDamage(attachedAgent));

            behaviorGroups.Add(initialStage);
            behaviorGroups.Add(stage2);
            behaviorGroups.Add(stage3);

            //behaviors.Add(new Behavior_IdleUntilTargetEntersRadius(attachedAgent, radius));
            //behaviors.Add(new Behavior_PathfindToTarget(attachedAgent, FindObjectOfType<Pathfinding.Grid>(), FindObjectOfType<Pathfinding.AStar>(), target));
            //behaviors.Add(new Behavior_DoMostDamage(attachedAgent));
        }
    }
}