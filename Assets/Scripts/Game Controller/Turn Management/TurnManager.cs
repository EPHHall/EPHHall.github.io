using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.GameController
{
    public class TurnManager : MonoBehaviour
    {
        public static TurnTaker currentTurnTaker;
        public static bool staticPrintTurnTaker;

        public List<TurnTaker> turnTakers = new List<TurnTaker>();
        public int turnTakersIndex = 0;

        public int turnNumber;

        private Transform marker;

        [Space(5)]
        [Header("Controls")]
        public bool printTurnTaker;

        public void Start()
        {
            turnTakers.Add(GameObject.Find("Player").GetComponent<TurnTaker>());

            SetTurnTakerList();

            marker = GameObject.Find("Turn Marker").transform;

            ChangeTurnTaker();
        }

        public void Update()
        {
            if (printTurnTaker || staticPrintTurnTaker)
            {
                PrintCurrentTurnTaker();
                printTurnTaker = false;
                staticPrintTurnTaker = false;
            }
        }

        public void SetTurnTakerList()
        {
            foreach (TurnTaker turnTaker in GameObject.FindObjectsOfType<TurnTaker>())
            {
                if (!turnTakers.Contains(turnTaker))
                {
                    turnTakers.Add(turnTaker);
                }
            }
        }

        private static void PrintCurrentTurnTaker()
        {
            Debug.Log(currentTurnTaker.name);
        }

        public void ChangeTurnTaker()
        {
            if (currentTurnTaker != null)
            {
                EndTurn(currentTurnTaker);
            }

            //if (turnTakersIndex >= turnTakers.Count)
            //{
            //    turnTakersIndex = 0;
            //}

            //if (turnTakers[turnTakersIndex] == null)
            //{
            //    turnTakers.RemoveAt(turnTakersIndex);
            //}

            if (turnTakersIndex >= turnTakers.Count)
            {
                turnTakersIndex = 0;
            }

            foreach (TurnCounter counter in GameObject.FindObjectsOfType<TurnCounter>())
            {
                counter.DecrementCountdowns();
            }

            StartTurn(turnTakers[turnTakersIndex]);
            turnTakersIndex++;
        }

        public void EndTurn(TurnTaker turnTaker)
        {
            switch (turnTaker.tag)
            {
                case "Player":
                    EndTurn(turnTaker.GetComponent<TurnTakerPlayer>());
                    break;

                default:
                    turnTaker.EndTurn();
                    break;
            }
        }

        public void EndTurn(TurnTakerPlayer turnTaker)
        {
            turnTaker.EndTurn();
        }

        public void StartTurn(TurnTaker turnTaker)
        {
            currentTurnTaker = turnTaker;
            marker.parent = currentTurnTaker.transform;
            marker.transform.localPosition = new Vector2(0, .8f);

            switch (turnTaker.tag)
            {
                case "Player":
                    StartTurn(turnTaker.GetComponent<TurnTakerPlayer>());
                    break;

                default:
                    turnTaker.StartTurn();
                    break;
            }
        }

        public void StartTurn(TurnTakerPlayer turnTaker)
        {
            turnTaker.StartTurn();
        }
    }
}
