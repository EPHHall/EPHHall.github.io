using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SS.LevelDesign
{
    public class EndGame : Interactable 
    {
        public override void ActivateInteractable()
        {
            base.ActivateInteractable();

            FindObjectOfType<SS.GameController.SS_SceneManager>().ChangeScene(1);
        }

        public override void ActivateInteractableAlternate()
        {
            base.ActivateInteractableAlternate();

            FindObjectOfType<SS.GameController.SS_SceneManager>().ChangeScene(0);
        }
    }
}
