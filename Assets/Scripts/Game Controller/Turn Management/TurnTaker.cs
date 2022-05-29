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

        public virtual void Awake()
        {
        }
        
        public virtual void Start()
        {
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
                TurnEnd();
            }
        }

        public virtual void StartTurn()
        {
            State = TurnTakerState.TurnBeginning;
        }

        public virtual void TurnBeginning()
        {
            OnTurnStart.Invoke(this, System.EventArgs.Empty);
        }

        public virtual void TurnBody()
        {

        }

        public virtual void TurnEnd()
        {
            OnTurnEnd.Invoke(this, System.EventArgs.Empty);
        }

        public virtual void ResetTurnTaker()
        {
            State = TurnTakerState.NotTurn;
        }
    }
}
