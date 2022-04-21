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

        /// <summary>
        /// Either or both of interactable and action can be null, or they could both be non-null.
        /// </summary>
        /// <param name="conBoxContent"></param>
        /// <param name="interactable"></param>
        /// <param name="action"></param>
        public void BringUpBox(string conBoxContent, Interactable interactable, ConfirmationAction action, bool restrictMovement)
        {
            Debug.Log(action);
            if (player.position.y < lowerBound.position.y)
            {
                top.ActivateConfirmationBox(conBoxContent, interactable, action);

                if (restrictMovement)
                {
                    top.RestrictPlayerMovement();
                }
            }
            else
            {
                bottom.ActivateConfirmationBox(conBoxContent, interactable, action);

                if (restrictMovement)
                {
                    bottom.RestrictPlayerMovement();
                }
            }
        }
    }
}
