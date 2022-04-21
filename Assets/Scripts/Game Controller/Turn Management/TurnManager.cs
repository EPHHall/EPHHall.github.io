using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.GameController
{
    public class TurnManager : MonoBehaviour
    {
        public static TurnTaker currentTurnTaker;
        public static TurnTaker previousTurnTaker;
        public static bool staticPrintTurnTaker;
        public static TurnManager tm;

        public List<TurnTaker> turnTakers = new List<TurnTaker>();
        public int turnTakersIndex = 0;

        public int turnNumber;
        public int roundNumber;

        public bool newRound;

        private Transform marker;

        private TurnTakerPlayer player;

        private bool animateTurnIndicator;
        private float t;
        private Vector2 markerOGPos;
        private Vector2 markerOffset;
        public float turnIndicatorSpeed;
        public float pauseAfterMarkerAnimation;

        [Space(5)]
        [Header("Controls")]
        public bool printTurnTaker;

        public void Start()
        {
            if (tm == null)
            {
                tm = this;
            }

            player = FindObjectOfType<TurnTakerPlayer>();

            turnTakers.Add(GameObject.Find("Player").GetComponent<TurnTaker>());

            SetTurnTakerList();

            marker = GameObject.Find("Turn Marker").transform;

            ChangeTurnTaker();

            markerOffset.y = marker.localPosition.y;
            markerOffset.x = marker.localPosition.x;
        }

        public void Update()
        {
            if (printTurnTaker || staticPrintTurnTaker)
            {
                PrintCurrentTurnTaker();
                printTurnTaker = false;
                staticPrintTurnTaker = false;
            }

            if(animateTurnIndicator && t < 1 + pauseAfterMarkerAnimation)
            {
                t += Time.deltaTime * turnIndicatorSpeed;

                marker.transform.position = Vector2.Lerp(markerOGPos, (Vector2)turnTakers[turnTakersIndex].transform.position + markerOffset, t);
            }
            else if(animateTurnIndicator)
            {
                animateTurnIndicator = false;
                marker.position = (Vector2)turnTakers[turnTakersIndex].transform.position + markerOffset;
                FinishChangingTurnTaker();
            }
        }

        public void SetTurnTakerList()
        {
            turnTakers.Clear();

            foreach (TurnTaker turnTaker in GameObject.FindObjectsOfType<TurnTaker>())
            {
                if (turnTaker == player as TurnTaker)
                {
                    continue;
                }

                if (!turnTakers.Contains(turnTaker) && (turnTaker as TurnTakerControlledObject) == null && !turnTaker.dontAutomaticallyAdd)
                {
                    turnTakers.Add(turnTaker);
                }
            }

            turnTakers.Insert(0, player);
            turnTakersIndex = 0;
        }

        public void AddNextTurnTaker(TurnTaker nextTurnTaker)
        {
            if (turnTakers.Contains(nextTurnTaker))
            {
                TurnTaker temp = GetNextTurnTaker();
                turnTakers.Remove(nextTurnTaker);

                //turnTakers.Insert(turnTakersIndex, nextTurnTaker);

                turnTakers.Insert(turnTakers.IndexOf(temp), nextTurnTaker);
            }
            else
            {
                //TurnTaker temp = GetNextTurnTaker();
                //turnTakers.Insert(turnTakers.IndexOf(temp), nextTurnTaker);

                turnTakers.Insert(turnTakersIndex + 1, nextTurnTaker);
            }
        }

        public TurnTaker GetNextTurnTaker()
        {
            TurnTaker toReturn = null;

            if (turnTakersIndex < turnTakers.Count - 1)
            {
                toReturn = turnTakers[turnTakersIndex + 1];
            }
            else
            {
                toReturn = turnTakers[0];
            }

            return toReturn;
        }

        private static void PrintCurrentTurnTaker()
        {
            Debug.Log(currentTurnTaker.name);
        }

        bool changingTurnTaker;
        public void ChangeTurnTaker()
        {
            if (changingTurnTaker) return;

            changingTurnTaker = true;

            newRound = false;
            turnTakersIndex++;

            if (currentTurnTaker != null)
            {
                EndTurn(currentTurnTaker);
            }

            if (turnTakersIndex >= turnTakers.Count)
            {
                turnTakersIndex = 0;
                roundNumber++;

                foreach (Spells.Spell spell in GameObject.FindObjectsOfType<Spells.Spell>())
                {
                    spell.TurnChangeReset();
                }

                newRound = true;
            }

            foreach (TurnCounter counter in GameObject.FindObjectsOfType<TurnCounter>())
            {
                counter.DecrementCountdowns();
            }

            foreach (AI.AIPackage package in FindObjectsOfType<AI.AIPackage>())
            {
                package.run = false;
            }

            markerOGPos = marker.transform.position;
            t = 0;

            animateTurnIndicator = true;
        }

        public void FinishChangingTurnTaker()
        {
            StartTurn(turnTakers[turnTakersIndex]);

            SS.Spells.Target[] targets = FindObjectsOfType<SS.Spells.Target>();
            for (int i = 0; i < targets.Length; i++)
            {
                //The effects applied by the turn taker may disapear; we want them to apply one last time, so that one will be handled right after the loop
                if (targets[i].GetComponent<TurnTaker>() == currentTurnTaker)
                    continue;

                targets[i].HandleStatuses(newRound, false);
            }

            if (currentTurnTaker.GetComponent<SS.Spells.Target>() != null)
            {
                currentTurnTaker.GetComponent<SS.Spells.Target>().HandleStatuses(newRound, false);
            }

            changingTurnTaker = false;
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

        public void RemoveTurnTakerFromPlay(TurnTaker tt)
        {
            turnTakers.Remove(tt);
            turnTakersIndex--;

            if (turnTakersIndex < 0)
            {
                turnTakersIndex = 0;
            }
        }

        public void EndTurn(TurnTakerPlayer turnTaker)
        {
            turnTaker.EndTurn();
        }

        public void StartTurn(TurnTaker turnTaker)
        {
            previousTurnTaker = currentTurnTaker;
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
