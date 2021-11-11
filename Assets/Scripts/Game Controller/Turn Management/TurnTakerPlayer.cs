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
        }

        public override void StartTurn()
        {
            base.StartTurn();

            //Debug.Log(GetComponent<CharacterStats>().mana);
            GetComponent<SS.PlayerMovement.SS_PlayerMoveRange>().ResetMoveRange();
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
