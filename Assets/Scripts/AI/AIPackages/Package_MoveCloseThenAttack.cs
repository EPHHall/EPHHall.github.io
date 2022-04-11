using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{
    public class Package_MoveCloseThenAttack : AIPackage
    {
        public Animation.MovementAnimation movementAnimation; //Child of teh package object.

        public override void Start()
        {
            base.Start();

            Pathfinding.Grid grid = FindObjectOfType<Pathfinding.Grid>();

            groupsVersion = true;

            BehaviorGroup initialStage = new BehaviorGroup();
            initialStage.behaviors.Add(new Behavior_PathfindToTarget(attachedAgent, grid, FindObjectOfType<Pathfinding.AStar>()));

            BehaviorGroup stage2 = new BehaviorGroup();
            stage2.behaviors.Add(new Behavior_WaitForMovementAnim(attachedAgent, grid));
            
            BehaviorGroup stage2andAHalf = new BehaviorGroup();
            stage2andAHalf.behaviors.Add(new Behavior_WaitForAnimations(attachedAgent));

            BehaviorGroup stage3 = new BehaviorGroup();
            stage3.behaviors.Add(new Behavior_DoMostDamage(attachedAgent));

            BehaviorGroup stage4 = new BehaviorGroup();
            stage4.behaviors.Add(new Behavior_WaitForAnimations(attachedAgent));

            behaviorGroups.Add(initialStage);
            behaviorGroups.Add(stage2);
            behaviorGroups.Add(stage2andAHalf);
            behaviorGroups.Add(stage3);
            behaviorGroups.Add(stage4);

            //behaviors.Add(new Behavior_PathfindToTarget(attachedAgent, FindObjectOfType<Pathfinding.Grid>(), FindObjectOfType<Pathfinding.AStar>()));

            //behaviors.Add(new Behavior_DoMostDamage(attachedAgent));
        }
    }
}
