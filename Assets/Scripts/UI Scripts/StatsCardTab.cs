using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SS.UI
{
    public class StatsCardTab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        static StatsCardTab selectedTab;

        public enum TabType
        {
            Top, Middle, Bottom
        }

        public TabType tabType;
        public Sprite selectedSprite;
        public Sprite originalSprite;
        public Image icon;
        public Sprite iconSelected;
        public Sprite iconOriginal;
        public Vector2 iconOffset; //moves the icon when it is deselected
        public StatsCard.StatsCardLayout layout;
        public StatsCard statsCard;

        public bool unused;

        [Space(5)]
        [Header("Don't touch")]
        public StatsCardTab[] tabs;
        public Image image;
        public bool active; //In the sense that it can be interacted with at all; might be that some targets won't need to use every tab
        public Button button;
        public Vector2 iconPosOriginal;
        public bool pointerIsOver;

        private void Awake()
        {
            image = GetComponent<Image>();

            button = GetComponent<Button>();
        }
        private void Start()
        {
            image = GetComponent<Image>();

            button = GetComponent<Button>();

            if (name == "Top Tab")
            {
                SelectTab();
            }

            if (icon != null)
            {
                if (selectedTab == this)
                {
                    iconPosOriginal = (Vector2)icon.transform.localPosition - iconOffset;
                }
                else
                {
                    iconPosOriginal = icon.transform.localPosition;
                }
            }


        }

        private void Update()
        {
            if (icon != null && !unused)
            {
                if (pointerIsOver)
                {
                    icon.transform.localPosition = iconPosOriginal + iconOffset;
                }
                else if (selectedTab != this)
                {
                    icon.transform.localPosition = iconPosOriginal;
                }
            }
        }

        public void SelectTab()
        {
            if (!active || unused) return;

            DeselectTabs();

            selectedTab = this;
            image.sprite = selectedSprite;
            button.interactable = false;

            transform.SetAsLastSibling();

            statsCard.ChangeLayout(layout);

            if (icon != null)
            {
                icon.sprite = iconSelected;
                icon.transform.localPosition = iconPosOriginal + iconOffset;
            }
        }

        public void DeselectTabs()
        {
            foreach (StatsCardTab tab in GameObject.FindObjectsOfType<StatsCardTab>())
            {
                tab.DeselectTab();
            }
        }

        public void DeselectTab()
        {
            if (unused) return;

            if (selectedTab == this) selectedTab = null;

            if (image != null)
                image.sprite = originalSprite;

            if (button != null)
                button.interactable = true;


            if (icon != null)
            {
                icon.sprite = iconOriginal;
                icon.transform.localPosition = iconPosOriginal;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                pointerIsOver = true;
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                pointerIsOver = false;
            }
        }
    }
}