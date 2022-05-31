using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{
    public abstract class AIStateManager : MonoBehaviour
    {
        private static AIStateFactory _factory;

        public AIState_Base CurrentState { get; set; }
        public Spells.Target target;

        private AIState_Base _defaultState;

        [Space(10)]
        [Header("Debug")]
        public string currentStateName;

        public virtual void Awake()
        {
            if (_factory == null)
            {
                _factory = new AIStateFactory();
            }
        }

        public virtual void Start()
        {
            CurrentState = _defaultState;
            CurrentState.EnterState();
        }

        public virtual void Update()
        {
            CurrentState.UpdateState();

            HandleDebug();
        }

        public virtual void HandleDebug()
        {
            currentStateName = CurrentState.Name;
        }
    }
}
