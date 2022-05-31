namespace SS.NewAI.MoveCloseThenAttack
{
    public class BehaviorFactory_MoveCloseThenAttack : BehaviorFactoryBase
    {
        public AIStateBase MoveToTarget { get; private set; }
        public AIStateBase Attack { get; private set; }

        public BehaviorFactory_MoveCloseThenAttack(AIStateMachine package) : base(package)
        {
            MoveToTarget = new Behavior_MoveToTarget(package);
            Attack = new Behavior_Attack(package);
        }
    }
}
