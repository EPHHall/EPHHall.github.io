using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.NewAI.MoveCloseThenAttack
{
    public class MoveCloseThenAttack : AIStateMachine
    {
        public override void Start()
        {
            base.Start();

            Name = "Move Close Then Attack";

            Factory = new BehaviorFactory_MoveCloseThenAttack(this);
            _initialState = (Factory as BehaviorFactory_MoveCloseThenAttack).MoveToTarget;
        }

        public override void StartMachine()
        {
            base.StartMachine();
        }

        public override void UpdateMachine()
        {
            base.UpdateMachine();
        }

        public override void ExitMachine()
        {
            
        }
    }
}
