using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using SS.Spells;

namespace SS.UI
{
    public class MagicFrame : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public Sprite emptySprite;
        public GameObject dragObject;

        [Space(5)]
        [Header("Dont Touch")]
        public Image image;
        public Object content;
        private GameObject currentDragObject;
        public Sprite currentSprite;
        private Button thisButton;
        public SpellInventory spellInventory;

        public virtual void Start()
        {
            spellInventory = FindObjectOfType<SpellInventory>();

            image = GetComponent<Image>();
            thisButton = GetComponent<Button>();

            if (content == null)
            {
                currentSprite = emptySprite;
            }
        }

        public virtual void Update()
        {
            image.color = new Color(1,1,1,1);

            if (image.sprite != currentSprite)
            {
                image.sprite = currentSprite;
            }
        }

        public void DisplayIcon(Sprite icon)
        {
            currentSprite = icon;
        }

        public virtual void SetContent(Object newContent)
        {
            if (!isActiveAndEnabled)
            {
                Start();
            }

            Object temp = content;
            content = newContent;

            ResetDisplay();

            //Debug.Log(spellInventory);
            spellInventory.SpellChanged(this, temp);
        }

        public virtual void ResetDisplay()
        {
            currentSprite = emptySprite;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (dragObject != null)
            {
                currentDragObject = Instantiate(dragObject, GameObject.Find("Drag Box Parent").transform);
                currentDragObject.GetComponent<FrameDragObject>().createdFrom = this;
                currentDragObject.GetComponent<FrameDragObject>().SetIcon();
            }
        }

        public virtual void Highlight()
        {
            image.color = thisButton.colors.highlightedColor;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (currentDragObject != null)
            {
                currentDragObject.GetComponent<FrameDragObject>().FillCurrentFrame();

                Destroy(currentDragObject);
                currentDragObject = null;
            }
        }

        public virtual void ResetFrame()
        {
            SetContent(null);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                ResetFrame();
            }
        }
    }
}
