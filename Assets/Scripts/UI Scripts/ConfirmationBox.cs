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
        public ConfirmationAction action;

        public Button confirmButton;
        public Button denyButton;

        private bool didRestrictMovement;

        private PlayerMovement.SS_PlayerController playerController;

        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerController = player.GetComponent<PlayerMovement.SS_PlayerController>();
            }
        }

        public void ActivateConfirmationBox(string conBoxContent, Interactable newInteractable, ConfirmationAction newAction)
        {
            Debug.Log(newAction);

            gameObject.SetActive(true);

            interactable = newInteractable;
            action = newAction;
            text.text = conBoxContent;

            confirmButton.interactable = true;
            denyButton.interactable = true;
        }

        public void ConfirmAction()
        {
            if (interactable != null)
                interactable.ActivateInteractable();

            if (action != null)
                action.Confirm();

            gameObject.SetActive(false);

            if (didRestrictMovement)
            {
                UnRestrictPlayerMovement();
            }
        }
        public void CancelAction()
        {
            if (interactable != null)
                interactable.ActivateInteractableAlternate();

            if (action != null)
                action.Deny();

            gameObject.SetActive(false);

            if (didRestrictMovement)
            {
                UnRestrictPlayerMovement();
            }
        }

        public void RestrictPlayerMovement()
        {
            if(playerController != null)
            {
                playerController.PauseMovement_Confirmation();
                didRestrictMovement = true;
            }
        }

        public void UnRestrictPlayerMovement()
        {
            didRestrictMovement = false;

            if (playerController != null)
            {
                playerController.UnPauseMovement_Confirmation();
            }
        }
    }
}
