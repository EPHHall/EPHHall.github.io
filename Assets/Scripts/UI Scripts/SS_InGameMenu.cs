using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.UI
{
    public class SS_InGameMenu : MonoBehaviour
    {
        public static RectTransform currentTab;
        public RectTransform[] tabContents;
        public Button[] tabs;
        public Vector2 tabPos;

        private void Start()
        {
            SetTabsInactive();

            ActivateTab(tabContents[0]);
        }

        public void ChangeTab(RectTransform newTab)
        {
            SetTabsInactive();

            ActivateTab(newTab);
        }

        public void SetTabsInactive()
        {
            foreach (RectTransform tab in tabContents)
            {
                tab.gameObject.SetActive(false);
            }
        }

        public void ActivateTab(RectTransform tab)
        {
            tab.gameObject.SetActive(true);

            tab.anchoredPosition = tabPos;

            currentTab = tab;
        }
    }
}
