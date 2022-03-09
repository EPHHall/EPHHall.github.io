using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{
    public class Package_MoveCloseThenAttack : AIPackage
    {
        public override void Awake()
        {
            base.Awake();

            groupsVersion = true;

            BehaviorGroup initialStage = new BehaviorGroup();
            initialStage.behaviors.Add(new Behavior_PathfindToTarget(attachedAgent, FindObjectOfType<Pathfinding.Grid>(), FindObjectOfType<Pathfinding.AStar>()));

            BehaviorGroup stage2 = new BehaviorGroup();
            stage2.behaviors.Add(new Behavior_DoMostDamage(attachedAgent));
            
            behaviorGroups.Add(initialStage);
            behaviorGroups.Add(stage2);




            //behaviors.Add(new Behavior_PathfindToTarget(attachedAgent, FindObjectOfType<Pathfinding.Grid>(), FindObjectOfType<Pathfinding.AStar>()));

            //behaviors.Add(new Behavior_DoMostDamage(attachedAgent));
        }
    }
}
