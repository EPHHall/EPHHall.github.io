using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.UI;

namespace SS.LevelDesign
{
    public class Interactable : MonoBehaviour
    {
        public Room room;
        public bool noUseUntilRoomClear;
        public bool deactivateAfterUse;
        private bool roomClearHandled;
        public List<ActivationZone> activationZones;
        public string[] tagsToCheckFor;

        private bool mayBeActivated;

        public bool bringUpConfirmationBoxFirst;
        public ConfirmationBoxManager conBoxManager;
        [TextArea(6, 10)]
        public string textForConBox;

        public KeyCode activationKey = KeyCode.Space;

        public virtual void Start()
        {
            foreach (ActivationZone zone in activationZones)
            {
                zone.parent = this;

                if (noUseUntilRoomClear)
                {
                    zone.gameObject.SetActive(false);
                }
            }

            if(conBoxManager == null)
            {
                conBoxManager = FindObjectOfType<ConfirmationBoxManager>();
            }
        }

        public virtual void Update()
        {
            if (mayBeActivated && Input.GetKeyDown(activationKey))
            {
                if (bringUpConfirmationBoxFirst)
                {
                    conBoxManager.BringUpBox(textForConBox, this);
                }
                else
                {
                    ActivateInteractable();
                }
            }

            if (room.cleared && !roomClearHandled)
            {
                roomClearHandled = true;

                foreach (ActivationZone zone in activationZones)
                {
                    if(!zone.disable)
                        zone.gameObject.SetActive(true);
                }
            }
        }

        public virtual void OnTriggerEnter2DChild(Collider2D collision)
        {
            foreach (string tag in tagsToCheckFor)
            {
                if (collision.tag == tag)
                {
                    mayBeActivated = true;
                    break;
                }
            }
        }

        public virtual void OnTriggerExit2DChild(Collider2D collision)
        {
            foreach (string tag in tagsToCheckFor)
            {
                if (collision.tag == tag)
                {
                    mayBeActivated = false;
                    break;
                }
            }
        }

        public virtual void ActivateInteractable()
        {
            if (deactivateAfterUse)
            {
                foreach (ActivationZone zone in activationZones)
                {
                    zone.gameObject.SetActive(false);
                }
            }
        }
        public virtual void ActivateInteractableAlternate()
        {
            
        }
        public virtual void DeactivateInteractable()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Room>() && room == null)
            {
                room = collision.GetComponent<Room>();
            }
        }
    }
}
