using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.LevelDesign
{
    public class ActivationZone : MonoBehaviour
    {
        public bool disable = false;
        [Space(5)]

        public Interactable parent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            parent.OnTriggerEnter2DChild(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            parent.OnTriggerExit2DChild(collision);
        }
    }
}
