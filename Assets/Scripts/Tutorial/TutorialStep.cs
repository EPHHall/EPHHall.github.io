using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.UI;

namespace Tutorial
{
    /*
    Each tutorial "Step" will be made up of a text box to bring up, greying out certain parts of the ui/game, deactivating certain parts of the ui/game, and deactivating some of the actions
    the player can take
    */
    public class TutorialStep : MonoBehaviour
    {
        public TutorialScene scene;
        [Space(5)]

        public bool preventPlayerMovement;
        public bool deactivateAllButtons;
        public bool activateAllButtons;

        [Space(5)]
        public TutorialBox textBox;
        public TutorialBox.ArrowOrientation arrowOrientation;
        public TutorialBox.ArrowPos arrowPos;
        public Vector2 textBoxPos;
        [TextArea(6,6)]
        public string text;
        public List<Button> toActivate;
        public GreyOutObjectScript greyOut;
        public GreyOutObjectScript.GreyOutSprite emphasize;

        public SS.PlayerMovement.SS_PlayerController player;

        private void Start()
        {
            if (player == null)
            {
                player = GameObject.Find("Player").GetComponent<SS.PlayerMovement.SS_PlayerController>();
            }
        }

        public void StartStep(TutorialScene partOf)
        {
            scene = partOf;

            if (preventPlayerMovement)
                player.pauseMovementForCutscene = true;

            if (deactivateAllButtons)
            {
                foreach (Button button in FindObjectsOfType<Button>(true))
                {
                    button.interactable = false;
                }
            }

            //text box buttons get enabled when the box is activated
            if (textBox != null)
            {
                textBox.gameObject.SetActive(true);
                textBox.SetPos(textBoxPos);
                textBox.SetText(text);
                textBox.HandleArrow(arrowOrientation, arrowPos);
            }

            foreach (Button button in toActivate)
            {
                button.interactable = true;
            }

            if (greyOut != null)
            {
                greyOut.ActivateGreyOut();
                greyOut.ChangeSprite(emphasize);
            }
        }

        public void EndStep()
        {
            player.pauseMovementForCutscene = false;

            if (activateAllButtons)
            {
                foreach (Button button in FindObjectsOfType<Button>(true))
                {
                    button.interactable = true;
                }
            }

            if (textBox != null)
            {
                textBox.gameObject.SetActive(false);
            }

            if (greyOut != null)
            {
                greyOut.DeactivateGreyOut();
            }
        }
    }
}
