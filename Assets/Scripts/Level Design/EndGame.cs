using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SS.LevelDesign
{
    public class EndGame : Interactable 
    {
        public int confirmScene;
        public int denyScene;

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
