using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.NewAI
{
    public abstract class AIStateMachine : MonoBehaviour
    {
        public string Name { get; protected set; }
        public Agent Agent { get; private set; }
        public AIStateBase CurrentState { get; set; }
        public BehaviorFactoryBase Factory { get; protected set; }

        protected AIStateBase _initialState;

        public virtual void InitializePackage(Agent agent)
        {
            Agent = agent;
        }

        public virtual void Start()
        {

        }

        public virtual void StartMachine()
        {
            if (CurrentState == null) CurrentState = _initialState;

            if(!CurrentState.PermEndState)
            {
                CurrentState.ExitState();
                CurrentState = _initialState;
                CurrentState.StartState();
            }
            else
            {
                ExitMachine();
            }
        }

        public virtual void UpdateMachine()
        {
            CurrentState.UpdateState();
        }

        public abstract void ExitMachine();
    }
}
