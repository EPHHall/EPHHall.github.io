using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using SS.Spells;

namespace SS.UI
{
    public class MagicFrame : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler
    {
        public Sprite emptySprite;
        public GameObject dragObject;
        public Material lineMaterial;
        public bool inventoryFrame;
        public InfoBox_NameDisplay infoBoxName;
        public bool activeFrame = true;

        [Space(5)]
        [Header("Dont Touch")]
        public Image image;
        public Object content;
        private GameObject currentDragObject;
        public Sprite currentSprite;
        private Button thisButton;
        public SpellInventory spellInventory;
        public LineRenderer lineRenderer;
        public bool pointerOver;
        public bool dontSetInUseFlags;
        public bool pointerIsDown;

        public virtual void Start()
        {
            spellInventory = FindObjectOfType<SpellInventory>();

            image = GetComponent<Image>();
            thisButton = GetComponent<Button>();

            if (content == null)
            {
                currentSprite = emptySprite;
            }

            if (lineRenderer == null)
            {
                GameObject line = new GameObject(name + " Line");
                line.transform.parent = transform;
                line.transform.localPosition = Vector2.zero;
                line.AddComponent<LineRenderer>();
                lineRenderer = line.GetComponent<LineRenderer>();

                lineRenderer.material = lineMaterial;
                lineRenderer.positionCount = 2;
                lineRenderer.startColor = Color.black;
                lineRenderer.endColor = Color.black;
                //TODO: This shouldn't be hardcoded
                lineRenderer.sortingOrder = 501;
                lineRenderer.startWidth = .1f;
                lineRenderer.endWidth = .1f;

                lineRenderer.gameObject.SetActive(false);
            }
        }

        public virtual void Update()
        {
            image.color = new Color(1,1,1,1);

            if (image.sprite != currentSprite)
            {
                image.sprite = currentSprite;
            }

            //TODO inventory frames should probabaly just be their own subclass.
            if (inventoryFrame)
            {
                if (pointerOver)
                {
                    infoBoxName.gameObject.SetActive(true);
                }
                else
                {
                    infoBoxName.gameObject.SetActive(false);
                }
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

            if (newContent is Effect)
            {
                Effect effect = newContent as Effect;

                if (!dontSetInUseFlags)
                {
                    if (inventoryFrame)
                    {

                        //effect.SetInUse(false, this);
                        dontSetInUseFlags = true;
                    }
                    else
                    {
                        effect.SetInUse(true, this);
                    }
                }
            }
            if (newContent is Modifier)
            {
                Modifier mod = newContent as Modifier;

                if (!dontSetInUseFlags)
                {
                    if (inventoryFrame)
                    {
                        //mod.SetInUse(false, this);
                        dontSetInUseFlags = true;
                    }
                    else
                    {
                        mod.SetInUse(true, this);
                    }
                }
            }

            if (spellInventory == null)
            {
                spellInventory = FindObjectOfType<SpellInventory>();
            }

            spellInventory.SpellChanged(this, temp);
        }

        public virtual void ResetDisplay()
        {
            currentSprite = emptySprite;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            //pointerIsDown = true;

            //eventData.button != PointerEventData.InputButton.Right needs to be there or else the OnPointerClick later on doesn't run
            //if (dragObject != null && eventData.button != PointerEventData.InputButton.Right)
            //{
            //    currentDragObject = Instantiate(dragObject, GameObject.Find("Drag Box Parent").transform);
            //    currentDragObject.GetComponent<FrameDragObject>().createdFrom = this;
            //    currentDragObject.GetComponent<FrameDragObject>().SetIcon();
            //}
        }

        public virtual void Highlight()
        {
            image.color = thisButton.colors.highlightedColor;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                    //pointerIsDown = false;

                if (currentDragObject != null)
                {
                    currentDragObject.GetComponent<FrameDragObject>().FillCurrentFrame();

                    Destroy(currentDragObject);
                    currentDragObject = null;
                }
            }
        }

        public virtual void ResetFrame()
        {
            SetContent(null);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                if (eventData.button == PointerEventData.InputButton.Right)
                {
                    ResetFrame();
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                pointerOver = true;

                lineRenderer.gameObject.SetActive(true);

                Effect contentEffect = content as Effect;
                Modifier contentMod = content as Modifier;

                if (content != null)
                {
                    if (contentEffect != null && contentEffect.inventoryFrame != null && contentEffect.activeFrame != null)
                    {
                        Vector2 pos1 = contentEffect.inventoryFrame.transform.position;
                        Vector2 pos2 = contentEffect.activeFrame.transform.position;

                        lineRenderer.SetPosition(0, pos1);
                        lineRenderer.SetPosition(1, pos2);
                    }
                    else if (contentMod != null && contentMod.inventoryFrame != null && contentMod.activeFrame != null)
                    {
                        Vector2 pos1 = contentMod.inventoryFrame.transform.position;
                        Vector2 pos2 = contentMod.activeFrame.transform.position;

                        lineRenderer.SetPosition(0, pos1);
                        lineRenderer.SetPosition(1, pos2);
                    }
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                pointerOver = false;

                lineRenderer.gameObject.SetActive(false);
                lineRenderer.SetPosition(0, Vector2.zero);
                lineRenderer.SetPosition(1, Vector2.zero);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                    //TODO: If there are any issues with spawning the dragobject, it could be because this code was moved from 
                    //OnPointerDown
                if (activeFrame && dragObject != null && eventData.button != PointerEventData.InputButton.Right)
                {
                    currentDragObject = Instantiate(dragObject, GameObject.Find("Drag Box Parent").transform);
                    currentDragObject.GetComponent<FrameDragObject>().createdFrom = this;
                    currentDragObject.GetComponent<FrameDragObject>().SetIcon();
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
    }
}
