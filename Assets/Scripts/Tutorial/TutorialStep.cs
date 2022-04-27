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
        public bool unpausePlayerMovement = true;
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
        public List<Button> toDeactivate;
        public GreyOutObjectScript greyOut;
        public GreyOutObjectScript.GreyOutSprite emphasize;
        public string title;
        public SS.PlayerMovement.SS_PlayerController player;

        [Space(5)]
        [Header("Fail State Stuff")]
        public SS.Character.CharacterDeath_TutorialDeath characterDeath;
        public string characterDeathMessage;
        public Vector2 characterDeathRespawnPoint;

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
                player.PauseMovement_ForCutscene();

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
                textBox.SetTitle(title);
                textBox.HandleArrow(arrowOrientation, arrowPos);
                textBox.ActivateButtons();
            }

            foreach (Button button in toActivate)
            {
                button.gameObject.SetActive(true);
            }

            foreach (Button button in toDeactivate)
            {
                button.gameObject.SetActive(false);
            }

            if (greyOut != null)
            {
                greyOut.ActivateGreyOut();
                greyOut.ChangeSprite(emphasize);
            }

            if (characterDeath != null)
            {
                characterDeath.deathMessage = characterDeathMessage;

                if (characterDeathRespawnPoint != Vector2.zero)
                {
                    characterDeath.respawnPoint = characterDeathRespawnPoint;
                }
            }
        }

        public void EndStep()
        {
            if (unpausePlayerMovement)
            {
                player.UnPauseMovement_ForCutscene();
            }

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
