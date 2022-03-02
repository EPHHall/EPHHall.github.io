using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.LevelDesign;

namespace SS.UI
{
    public class ConfirmationBoxManager : MonoBehaviour
    {
        public ConfirmationBox bottom;
        public ConfirmationBox top;

        public Transform lowerBound;
        public Transform player;

        public void BringUpBox(string conBoxContent, Interactable interactable)
        {
            if (player.position.y < lowerBound.position.y)
            {
                top.ActivateConfirmationBox(conBoxContent, interactable);
            }
            else
            {
                bottom.ActivateConfirmationBox(conBoxContent, interactable);
            }
        }
    }
}
