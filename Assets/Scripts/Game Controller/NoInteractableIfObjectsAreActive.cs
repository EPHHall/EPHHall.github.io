using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.GameController
{
    public class NoInteractableIfObjectsAreActive : MonoBehaviour
    {
        public static NoInteractableIfObjectsAreActive noInteract;
        public GameObject[] objectsToCheck;

        private void Start()
        {
            if(noInteract == null)
            {
                noInteract = this;
            }
            if(noInteract != this)
            {
                Destroy(gameObject);
            }
        }

        public bool CanInteract()
        {
            bool result = true;

            foreach(GameObject obj in objectsToCheck)
            { 
                result = !obj.activeInHierarchy;

                if(!result)
                {
                    break;
                }
            }

            return result;
        }
    }
}
