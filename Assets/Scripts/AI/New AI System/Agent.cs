using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.GameController;
using SS.Spells;
using SS.Character;

namespace SS.NewAI
{
    public class Agent : TurnTaker
    {
        [SerializeField] private List<AIStateMachine> packages;
        [SerializeField] private Target mainTarget;
        [SerializeField] [Tooltip("Speed of the movement animation. Lower is faster.")] private float movementDelay = .2f;

        public Stack<Target> Targets { get; set; }
        public Target MainTarget { get { return mainTarget; } set { mainTarget = value; } }
        public AIStateMachine ActivePackage { get; set; }
        public CharacterMovement.CharacterMovement AgentMovement { get; private set; }
        public float MovementDelay { get { return movementDelay; } }
        public CharacterStats Stats { get { return _stats; } }

        private int _packageIndex;
        private CharacterStats _stats;

        [Space(10)]
        [Header("Agent: Debug")]
        public string packageName;
        public string packageStateName;

        public override void Awake()
        {
            base.Awake();

            AgentMovement = GetComponent<CharacterMovement.CharacterMovement>();
            if (AgentMovement == null)
            {
                Debug.LogError("Object of type Agent does not have a CharacterMovement Component");
            }

            Targets = new Stack<Spells.Target>();
            Targets.Push(mainTarget);

            foreach(AIStateMachine package in packages)
            {
                package.InitializePackage(this);
            }
        }

        public override void Start()
        {
            base.Start();
        }

        public override void TurnBeginning()
        {
            base.TurnBeginning();

            AgentMovement.ResetMovement();

            _packageIndex = 0;
            ActivePackage = packages[_packageIndex];
            ActivePackage.StartMachine();

            State = TurnTakerState.TurnBody;
        }

        public override void TurnBody()
        {
            base.TurnBody();
            ActivePackage.UpdateMachine();

            if(ActivePackage.CurrentState.PermEndState || ActivePackage.CurrentState.TempEndState)
            {
                Debug.Log("End State Detected");

                ActivePackage.ExitMachine();
                _packageIndex++;
                if(_packageIndex < packages.Count)
                {
                    Debug.Log("More Packages Detected");

                    ActivePackage = packages[_packageIndex];
                    ActivePackage.StartMachine();
                }
                else
                {
                    Debug.Log("No More Packages Detected");

                    State = TurnTakerState.TurnEnd;
                }
            }
        }

        public override void HandleDebug()
        {
            base.HandleDebug();

            if (ActivePackage != null)
            {
                packageName = ActivePackage.Name;
                packageStateName = ActivePackage.CurrentState.Name;
            }
        }

        public override void TurnEnding()
        {
            base.TurnEnding();
            Debug.Log("Turn Should End");
        }
    }
}