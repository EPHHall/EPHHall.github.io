using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.PlayerMovement;
using SS.GameController;

namespace SS.Spells
{
    public class ControllableByPlayer : MonoBehaviour
    {
        public TurnCounter counter;
        public int baseMoveSpeed = 6;

        public void Initialize(int duration)
        {
            if (GetComponent<SS_PlayerController>() == null)
            {
                gameObject.AddComponent<SS_PlayerController>();
            }

            if (GetComponent<TurnTakerControlledObject>() == null)
            {
                gameObject.AddComponent<TurnTakerControlledObject>();
            }

            if (GetComponent<SS_PlayerMoveRange>() == null)
            {
                gameObject.AddComponent<SS_PlayerMoveRange>();
                gameObject.GetComponent<SS_PlayerMoveRange>().moveRange = baseMoveSpeed;
            }

            if (GetComponent<TurnCounter>() == null)
            {
                gameObject.AddComponent<TurnCounter>();
            }

            counter = GetComponent<TurnCounter>();
            counter.countDowns.Add(new TurnCounter.CountdownAndEffect(duration));


            int indexToInsertAt = TurnManager.instance.TurnTakers.IndexOf(TurnManager.instance.CurrentTurnTaker) + 1;
            if (indexToInsertAt >= TurnManager.instance.TurnTakers.Count)
            {
                indexToInsertAt = 0;
            }
            TurnManager.instance.TurnTakers.Insert(indexToInsertAt, GetComponent<TurnTakerControlledObject>());

            GetComponent<SS_PlayerController>().movementMask = GameObject.FindGameObjectWithTag("Player").GetComponent<SS_PlayerController>().movementMask;
        }

        private void Update()
        {
            if (GetComponent<TurnCounter>() == null)
            {
                TurnManager.instance.TurnTakers.Remove(GetComponent<TurnTakerControlledObject>());

                Destroy(GetComponent<SS_PlayerController>());
                Destroy(GetComponent<TurnTakerControlledObject>());
                Destroy(GetComponent<SS_PlayerMoveRange>());


                Destroy(this);
            }
        }
    }
}
