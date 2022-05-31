namespace SS.AI
{
    public class AIStateFactory
    {
        public AIState_Base State_MoveToTarget(AIStateManager manager)
        {
            return new AIState_MoveToTarget(manager);
        }
    }
}
