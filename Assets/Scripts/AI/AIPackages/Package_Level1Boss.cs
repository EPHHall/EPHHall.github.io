using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{
    public class Package_Level1Boss : AIPackage
    {
        [Space(5)]
        [Header("Can Modify")]
        public int radius = 3;
        public Transform target;
        public List<SS.Spells.Target> objectsToControl;
        public Animation.MovementAnimation movementAnimation; //Child of teh package object.
        public Pathfinding.Grid grid;

        public override void Start()
        {
            base.Start();

            grid = FindObjectOfType<Pathfinding.Grid>();

            groupsVersion = true;

            BehaviorGroup initialStage = new BehaviorGroup();
            BehaviorGroup initialInBetween1 = new BehaviorGroup();
            BehaviorGroup stage1 = new BehaviorGroup();
            //BehaviorGroup stage1InBetween1 = new BehaviorGroup();
            BehaviorGroup stage2 = new BehaviorGroup();
            BehaviorGroup stage2AndAQuarter = new BehaviorGroup();
            BehaviorGroup stage2AndAHalf = new BehaviorGroup();
            BehaviorGroup stage3 = new BehaviorGroup();
            BehaviorGroup stage3AndAHalf = new BehaviorGroup();

            initialStage.behaviors.Add(new Behavior_AnimateObjects(attachedAgent, objectsToControl, true));
            initialStage.behaviors.Add(new Behavior_IdleUntilAttacked(attachedAgent));
            initialStage.behaviors.Add(new Behavior_IdleUntilTargetEntersRadius(attachedAgent, radius));
            initialStage.turnOffWhenGroupCompleted = stage1;
            initialStage.andOr = BehaviorGroup.AndOr.Or;

            initialInBetween1.behaviors.Add(new Behavior_WaitForMovementAnim(attachedAgent, grid));

            stage1.behaviors.Add(new Behavior_AnimateObjects(attachedAgent, objectsToControl, false));
            stage1.behaviors.Add(new Behavior_IdleUntilAttacked(attachedAgent));
            stage1.behaviors.Add(new Behavior_IdleUntilTargetEntersRadius(attachedAgent, radius));
            stage1.timesTillFullyCompleted = 1;
            stage1.andOr = BehaviorGroup.AndOr.Or;
            stage1.goTo = 0;

            //stage1InBetween1.behaviors.Add(new Behavior_WaitForAnimations(attachedAgent));
            //stage1InBetween1.goTo = 0;
            //stage1InBetween1.turnOffWhenGroupCompleted = stage1;

            stage2.behaviors.Add(new Behavior_PathfindToTarget(attachedAgent, FindObjectOfType<Pathfinding.Grid>(), FindObjectOfType<Pathfinding.AStar>(), target));

            stage2AndAQuarter.behaviors.Add(new Behavior_WaitForMovementAnim(attachedAgent, grid));

            stage2AndAHalf.behaviors.Add(new Behavior_WaitForAnimations(attachedAgent));

            stage3.behaviors.Add(new Behavior_DoMostDamage(attachedAgent));

            stage3AndAHalf.behaviors.Add(new Behavior_WaitForAnimations(attachedAgent));

            behaviorGroups.Add(initialStage);
            behaviorGroups.Add(initialInBetween1);
            behaviorGroups.Add(stage1);
            //behaviorGroups.Add(stage1InBetween1);
            behaviorGroups.Add(stage2);
            behaviorGroups.Add(stage2AndAQuarter);
            behaviorGroups.Add(stage2AndAHalf);
            behaviorGroups.Add(stage3);
            behaviorGroups.Add(stage3AndAHalf);
        }

        public override void InvokeAI(bool TESTING)
        {


            base.InvokeAI(TESTING);
        }
    }
}