using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.UI
{
    public class SS_CollapseExpand : MonoBehaviour
    {
        public SS_InGameMenu menu;
        public GameObject otherButton;

        public void Collapse()
        {
            menu.gameObject.SetActive(false);
            otherButton.SetActive(true);
            gameObject.SetActive(false);
        }

        public void Expand()
        {
            menu.gameObject.SetActive(true);
            otherButton.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
