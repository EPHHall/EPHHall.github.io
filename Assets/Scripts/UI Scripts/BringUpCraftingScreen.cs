using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SS.UI
{
    public class BringUpCraftingScreen : MonoBehaviour, IPointerClickHandler 
    {
        public PauseScreen pauseScreen;
        public GameObject craftingScreen;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                pauseScreen.Toggle();
                pauseScreen.ChangeScreen(craftingScreen);
            }
        }
    }
}
