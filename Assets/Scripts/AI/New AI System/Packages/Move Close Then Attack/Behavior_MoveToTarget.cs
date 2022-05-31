using System.Collections;
using System.Collections.Generic;
using SS.Pathfinding;
using UnityEngine;

namespace SS.NewAI.MoveCloseThenAttack
{
    public class Behavior_MoveToTarget : PackageStateBase
    {
        private Stack<Node> _path = new Stack<Node>();
        private float _timer;
        private bool _movementHalted;

        public Behavior_MoveToTarget(AIStateMachine package) : base(package)
        {
            Name = "MoveCloseThenAttack Move State";
        }
        
        public override void StartState()
        {
            List<Node> temp = Pathfinding.AStar.instance.FindPath(Package.Agent.transform.position, Package.Agent.Targets.Peek().transform.position);
            temp.Reverse();
            _path = new Stack<Node>(temp);

            _timer = 0;
        }

        public override void UpdateState()
        {
            if(_timer <= 0 && Package.Agent.AgentMovement.MovementUsed < Package.Agent.AgentMovement.speed && _path.Count > 0)
            {
                Package.Agent.AgentMovement.Move(_path.Pop().position - Package.Agent.transform.position);
                _timer = Package.Agent.MovementDelay;
            }

            _timer -= Package.Agent.DeltaTime;

            EvaluateTransitions();
        }

        public override void ExitState()
        {
            
        }

        public override void EvaluateTransitions()
        {
            base.EvaluateTransitions();

            if (_path.Count == 0 || Package.Agent.AgentMovement.MovementUsed >= Package.Agent.AgentMovement.speed)
            {
                Transition((Package.Factory as BehaviorFactory_MoveCloseThenAttack).Attack);
            }
        }
    }
}
