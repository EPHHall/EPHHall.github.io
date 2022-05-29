using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.GameController
{
    public class TurnTaker : MonoBehaviour
    {
        //These damage handling variables and functions should be in Target instead. TODO
        //Also, DamageStuff should probably be in Damage or TargetInterface, that would make more sense.
        public enum TurnTakerState
        {
            NotTurn,
            TurnBeginning,
            TurnBody,
            TurnEnd
        }

        public event System.EventHandler OnTurnStart;
        public event System.EventHandler OnTurnEnd;

        public TurnTakerState State { get; set; }

        public bool dontAutomaticallyAdd = false;

        [Space(10)]
        [Header("Debug")]
        public string stateName;

        public virtual void Awake()
        {
        }
        
        public virtual void Start()
        {
            State = TurnTakerState.NotTurn;
        }

        public virtual void Update()
        {
            HandleDebug();
        }

        public virtual void HandleDebug()
        {
            stateName = State.ToString();
        }

        public virtual void TurnTakerUpdate()
        {
            if (State == TurnTakerState.TurnBeginning)
            {
                TurnBeginning();
            }

            if (State == TurnTakerState.TurnBody)
            {
                TurnBody();
            }

            if (State == TurnTakerState.TurnEnd)
            {
                TurnEnding();
            }
        }

        public virtual void StartTurn()
        {
            State = TurnTakerState.TurnBeginning;
        }

        public virtual void TurnBeginning()
        {
            OnTurnStart?.Invoke(this, System.EventArgs.Empty);
        }

        public virtual void TurnBody()
        {

        }

        public virtual void TurnEnding()
        {
            OnTurnEnd?.Invoke(this, System.EventArgs.Empty);
        }

        public virtual void ResetTurnTaker()
        {
            State = TurnTakerState.NotTurn;
        }

        public virtual void EndTurn()
        {
            State = TurnTakerState.TurnEnd;
        }
    }
}
