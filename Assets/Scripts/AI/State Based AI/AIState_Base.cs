using System.Collections;
using System.Collections.Generic;

namespace SS.AI
{
    public abstract class AIState_Base
    {
        public AIStateManager Manager { get; private set; }
        public string Name { get; set; }
        public List<AIStateTransition> Transitions { get; set; }

        public AIState_Base(AIStateManager manager)
        {
            Manager = manager;
        }

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();

        public virtual void EvaluateTransitions()
        {
            
        }

        public virtual void TransitionStates(AIState_Base transitionTo)
        {
            Manager.CurrentState.ExitState();
            Manager.CurrentState = transitionTo;
            Manager.CurrentState.EnterState();
        }
    }
}
