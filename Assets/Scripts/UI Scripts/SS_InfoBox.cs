using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SS.UI
{
    public class SS_InfoBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool pointerIsOver = false;

        public SS_DisplayInfoBox displayer;

        public Vector2[] positions;
        public Sprite[] sprites;

        public RectTransform viewport;

        public Text text;

        private void Start()
        {
            text = transform.Find("Text").GetComponent<Text>();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                displayer.pointerIsOver = true;
            }
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                displayer.pointerIsOver = false;
            }
        }

        private void Update()
        {
            text.text = displayer.infoString;
        }

        private void OnEnable()
        {
            if (positions.Length == 0)
                return;

            for (int i = 0; i < positions.Length; i++)
            {
                transform.localPosition = positions[i];

                if (transform.localPosition.y < 0)
                {
                    RectTransform text = transform.Find("Text").GetComponent<RectTransform>();
                    text.localPosition = new Vector3(text.localPosition.x, -Mathf.Abs(text.localPosition.y), text.localPosition.z);
                }
                else
                {
                    RectTransform text = transform.Find("Text").GetComponent<RectTransform>();
                    text.localPosition = new Vector3(text.localPosition.x, Mathf.Abs(text.localPosition.y), text.localPosition.z);
                }

                transform.GetComponent<Image>().sprite = sprites[i];
                Rect rect = GetComponent<RectTransform>().rect;
                RectTransform rectTransform = GetComponent<RectTransform>();

                //need corners 0 and 2: bottom left and top right
                Vector3[] viewportCorners = new Vector3[4];
                viewport.GetComponent<RectTransform>().GetWorldCorners(viewportCorners);
                Vector3[] thisCorners = new Vector3[4];
                GetComponent<RectTransform>().GetWorldCorners(thisCorners);

                bool withinWidth =  thisCorners[0].x < viewportCorners[2].x && thisCorners[0].x > viewportCorners[0].x &&
                                    thisCorners[2].x < viewportCorners[2].x && thisCorners[2].x > viewportCorners[0].x;
                bool withinHeight = thisCorners[0].y < viewportCorners[2].y && thisCorners[0].y > viewportCorners[0].y &&
                                    thisCorners[2].y < viewportCorners[2].y && thisCorners[2].y > viewportCorners[0].y;

                if (withinHeight && withinWidth)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            SetContent();
        }

        public void SetContent()
        {
            
        }

        public void HideBox()
        {
            gameObject.SetActive(false);
        }
    }
}
