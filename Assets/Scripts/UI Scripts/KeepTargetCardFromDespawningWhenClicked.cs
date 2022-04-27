using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SS.UI
{
    public class KeepTargetCardFromDespawningWhenClicked : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public StatsCard card;

        public bool pointerIsOver;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                card.pointerIsOver = true;
                pointerIsOver = true;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                card.pointerIsOver = false;
                pointerIsOver = false;
            }
        }

        private void OnDisable()
        {
            card.pointerIsOver = false;
        }

        private void OnDestroy()
        {
            if (GetComponent<SS.Util.ID>() != null)
            {
                SS.GameController.DestroyedTracker.instance.TrackDestroyedObject(GetComponent<SS.Util.ID>().id);
            }

            card.pointerIsOver = false;
        }
    }
}
