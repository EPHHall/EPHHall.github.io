namespace SS.NewAI
{
    public abstract class AIStateBase
    {
        public string Name { get; protected set; }

        public bool TempEndState { get; protected set; }
        public bool PermEndState { get; protected set; }

        public AIStateMachine Package { get; private set; }

        public abstract void StartState();
        public abstract void UpdateState();
        public abstract void ExitState();

        public AIStateBase(AIStateMachine package)
        {
            Package = package;
        }

        public virtual void EvaluateTransitions()
        {

        }

        public void Transition(AIStateBase to)
        {
            Package.CurrentState.ExitState();
            Package.CurrentState = to;
            Package.CurrentState.StartState();
        }
    }
}
