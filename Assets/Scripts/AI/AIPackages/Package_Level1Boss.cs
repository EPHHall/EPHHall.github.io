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

        public override void Awake()
        {
            base.Awake();

            groupsVersion = true;

            BehaviorGroup initialStage = new BehaviorGroup();
            initialStage.behaviors.Add(new Behavior_AnimateObjects(attachedAgent, objectsToControl));
            initialStage.behaviors.Add(new Behavior_IdleUntilAttacked(attachedAgent));
            initialStage.behaviors.Add(new Behavior_IdleUntilTargetEntersRadius(attachedAgent, radius));
            initialStage.turnOffGroupWhenCompleted = true;
            initialStage.andOr = BehaviorGroup.AndOr.Or;

            BehaviorGroup stage2 = new BehaviorGroup();
            stage2.behaviors.Add(new Behavior_PathfindToTarget(attachedAgent, FindObjectOfType<Pathfinding.Grid>(), FindObjectOfType<Pathfinding.AStar>(), target));

            BehaviorGroup stage3 = new BehaviorGroup();
            stage3.behaviors.Add(new Behavior_DoMostDamage(attachedAgent));

            behaviorGroups.Add(initialStage);
            behaviorGroups.Add(stage2);
            behaviorGroups.Add(stage3);
        }

        public override void InvokeAI(bool TESTING)
        {


            base.InvokeAI(TESTING);
        }
    }
}