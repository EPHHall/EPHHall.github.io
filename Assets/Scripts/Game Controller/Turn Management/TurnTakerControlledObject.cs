using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.GameController
{
    public class TurnTakerControlledObject : TurnTaker
    {
        public GameObject[] buttonsToSpawn = new GameObject[0];

        public override void EndTurn()
        {
            base.EndTurn();

            ToggleButtons(false);
        }

        public override void StartTurn()
        {
            base.StartTurn();

            GetComponent<SS.PlayerMovement.SS_PlayerMoveRange>().ResetMoveRange(transform.position);
            ToggleButtons(true);
        }

        public void ToggleButtons(bool toggle)
        {
            if (buttonsToSpawn.Length > 0)
            {
                foreach (GameObject button in buttonsToSpawn)
                {
                    button.SetActive(toggle);
                }
            }
        }
    }
}
