namespace SS.NewAI
{
    public class AIState_TempEnd : AIStateBase
    {
        public AIState_TempEnd(AIStateMachine package) : base(package)
        {
            TempEndState = true;
            Name = "Temporary End";
        }

        public override void ExitState()
        {

        }

        public override void StartState()
        {

        }

        public override void UpdateState()
        {

        }
    }
}