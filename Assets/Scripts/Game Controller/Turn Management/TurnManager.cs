using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.GameController
{
    public class TurnManager : MonoBehaviour
    {
        public enum TurnManagerState
        {
            RoundStart,
            RoundMain,
            RoundEnd,
            Standby
        }

        public event System.EventHandler OnRoundStart;
        public event System.EventHandler OnRoundEnd;

        public static TurnManager instance;

        public TurnTaker player;

        public List<TurnTaker> TurnTakers { get; private set; }
        public TurnTaker CurrentTurnTaker { get; private set; }
        public TurnManagerState State { get; private set; }

        private int _turnTakersIndex = 0;
        private int _turnNumber;
        private int _roundNumber;
        private Transform _marker;

        //Flags
        public bool OnlyOneTurnTaker { get; private set; }

        public void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Update()
        {
            if (State == TurnManagerState.RoundStart)
            {
                RoundStart();
            }

            if (State == TurnManagerState.RoundMain)
            {
                RoundMain();
            }

            if (State == TurnManagerState.RoundEnd)
            {
                RoundEnd();
            }

            if(State == TurnManagerState.Standby)
            {
                Standby();
            }
        }

        /// <summary>
        /// Code that runs in-between encounters
        /// </summary>
        private void Standby()
        {
            SetTurnTakerList();

            if (TurnTakers.Count <= 1)
            {
                State = TurnManagerState.Standby;
            }
            else
            {
                State = TurnManagerState.RoundStart;
            }
        }

        private void RoundStart()
        {
            OnRoundStart?.Invoke(this, System.EventArgs.Empty);

            CurrentTurnTaker = TurnTakers[0];
            _turnTakersIndex = 0;

            State = TurnManagerState.RoundMain;
        }

        private void RoundMain()
        {
            CurrentTurnTaker.TurnTakerUpdate();

            if(CurrentTurnTaker.State == TurnTaker.TurnTakerState.TurnEnd)
            {
                CurrentTurnTaker.ResetTurnTaker();

                if (_turnTakersIndex == TurnTakers.Count - 1)
                {
                    State = TurnManagerState.RoundEnd;
                }
                else
                {
                    CurrentTurnTaker = GetNextTurnTaker();
                }
            }
        }

        private void RoundEnd()
        {
            OnRoundEnd?.Invoke(this, System.EventArgs.Empty);

            if(TurnTakers.Count <= 1)
            {
                State = TurnManagerState.Standby;
            }
            else
            {
                State = TurnManagerState.RoundStart;
            }
        }

        public void SetTurnTakerList()
        {
            TurnTakers.Clear();

            foreach (TurnTaker turnTaker in GameObject.FindObjectsOfType<TurnTaker>())
            {
                if (turnTaker == player)
                {
                    continue;
                }

                if (!TurnTakers.Contains(turnTaker) && (turnTaker as TurnTakerControlledObject) == null && !turnTaker.dontAutomaticallyAdd)
                {
                    TurnTakers.Add(turnTaker);
                }
            }

            TurnTakers.Insert(0, player);
            _turnTakersIndex = 0;
        }

        public void AddNextTurnTaker(TurnTaker nextTurnTaker)
        {
            if (TurnTakers.Contains(nextTurnTaker))
            {
                TurnTaker temp = GetNextTurnTaker();
                TurnTakers.Remove(nextTurnTaker);

                TurnTakers.Insert(TurnTakers.IndexOf(temp), nextTurnTaker);
            }
            else
            {
                TurnTakers.Insert(_turnTakersIndex + 1, nextTurnTaker);
            }
        }

        public TurnTaker GetNextTurnTaker()
        {
            TurnTaker toReturn = null;

            if (_turnTakersIndex < TurnTakers.Count - 1)
            {
                toReturn = TurnTakers[_turnTakersIndex + 1];
            }
            else
            {
                toReturn = TurnTakers[0];
            }

            return toReturn;
        }
    }
}
