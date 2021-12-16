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

            behaviors.Add(new Behavior_MoveCloserToTarget(attachedAgent));
            behaviors.Add(new Behavior_DoMostDamage(attachedAgent));
        }
    }
}
