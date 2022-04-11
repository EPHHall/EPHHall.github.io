using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.LevelDesign;

namespace SS.UI
{
    public class ConfirmationBox : MonoBehaviour
    {
        public Text text;
        public Interactable interactable;

        public void ActivateConfirmationBox(string conBoxContent, Interactable newInteractable)
        {
            gameObject.SetActive(true);

            interactable = newInteractable;
            text.text = conBoxContent;
        }

        public void ConfirmAction()
        {
            interactable.ActivateInteractable();
            gameObject.SetActive(false);
        }
        public void CancelAction()
        {
            interactable.ActivateInteractableAlternate();
            gameObject.SetActive(false);
        }
    }
}
