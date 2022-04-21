using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.Character
{
    public class CharacterDeath : SS.LevelDesign.Interactable
    {

        [Space(10)]
        [Header("Character Death Stuff")]
        //public Tutorial.TutorialBox textBox;
        //public Vector2 textBoxPosition;
        public string deathMessage = "";
        //public string deathTitle;

        public bool respawn;
        public Vector2 respawnPoint;

        public int confirmScene;
        public int denyScene;

        public virtual void Death(CharacterStats stats)
        {
            foreach(Button button in FindObjectsOfType<Button>())
            {
                button.interactable = false;
            }

            FindObjectOfType<PlayerMovement.SS_PlayerController>().pauseMovementForCutscene = true;

            ActivateConfirmationBox();
        }

        public void ActivateConfirmationBox()
        {
            if (bringUpConfirmationBoxFirst)
            {
                string textToUse = textForConBox;
                if(deathMessage != "")
                {
                    textToUse = deathMessage;
                }

                conBoxManager.BringUpBox(textToUse, this, null, false);
            }
        }

        public override void ActivateInteractable()
        {
            base.ActivateInteractable();

            if (confirmScene < 0) return;

            FindObjectOfType<SS.GameController.SS_SceneManager>().ChangeScene(confirmScene);
        }

        public override void ActivateInteractableAlternate()
        {
            base.ActivateInteractableAlternate();

            if (denyScene < 0) return;

            FindObjectOfType<SS.GameController.SS_SceneManager>().ChangeScene(denyScene);
        }
    }
}
