namespace SS.NewAI
{
    public abstract class BehaviorFactoryBase
    {
        public AIStateBase TempEnd { get; private set; }
        public AIStateBase PermEnd { get; private set; }

        public BehaviorFactoryBase(AIStateMachine package)
        {
            TempEnd = new AIState_TempEnd(package);
            PermEnd = new AIState_PermEnd(package);
        }
    }
}
