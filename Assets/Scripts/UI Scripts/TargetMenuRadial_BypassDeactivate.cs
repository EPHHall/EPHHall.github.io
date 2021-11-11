using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.Spells;
using UnityEngine.EventSystems;

namespace SS.UI
{
    public class TargetMenuRadial_BypassDeactivate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        TargetMenuRadial menu;

        bool pointerIsOver;

        void Start()
        {
            menu = FindObjectOfType<TargetMenuRadial>();
        }

        private void Update()
        {
            if (pointerIsOver)
            {
                menu.dontDisableThisFrame = true;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            pointerIsOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            pointerIsOver = false;
        }
    }
}
