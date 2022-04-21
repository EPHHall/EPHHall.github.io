using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.Character;

namespace SS.GameController
{
    public class TurnTakerPlayer : TurnTaker
    {
        public Button[] buttonsToToggle;

        public override void EndTurn()
        {
            base.EndTurn();

            ToggleButtons(false);

            GameObject.FindGameObjectWithTag("Next Turn Button").GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        public override void StartTurn()
        {
            base.StartTurn();
            GameObject.FindGameObjectWithTag("Next Turn Button").GetComponent<UnityEngine.UI.Button>().interactable = true;

            //Debug.Log(GetComponent<CharacterStats>().mana);
            GetComponent<SS.PlayerMovement.SS_PlayerMoveRange>().ResetMoveRange(transform.position);
            SS.Util.CharacterStatsInterface.ResetAP(GetComponent<CharacterStats>());
            SS.Util.CharacterStatsInterface.ResetMana(GetComponent<CharacterStats>());
            //Debug.Log(GetComponent<CharacterStats>().mana);

            ToggleButtons(true);
        }

        public void ToggleButtons(bool toggle)
        {
            foreach (Button button in buttonsToToggle)
            {
                button.interactable = toggle;
            }
        }
    }
}
