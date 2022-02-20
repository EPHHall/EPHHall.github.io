using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class TutorialBox : MonoBehaviour
    {
        public enum ArrowOrientation
        {
            Left,
            Right,
            None
        }

        public enum ArrowPos
        {
            Top,
            Middle,
            Bottom
        }

        public Button forwardButton;
        public Button backButton;

        public Text content;
        public Text title;

        [System.Serializable]
        public class ArrowArray
        {
            public GameObject[] arrows = new GameObject[3];
        }
        public ArrowArray[] arrows = new ArrowArray[2];

        private void Start()
        {
            
        }

        public void SetText(string text)
        {
            content.text = text;
        }
        public void SetTitle(string text)
        {
            title.text = text;
        }

        public void SetPos(Vector2 newPos)
        {
            GetComponent<RectTransform>().anchoredPosition = newPos;
        }

        public void HandleArrow(ArrowOrientation orientation, ArrowPos pos)
        {
            foreach (ArrowArray array in arrows)
            {
                foreach (GameObject arrow in array.arrows)
                {
                    arrow.SetActive(false);
                }
            }

            if (orientation != ArrowOrientation.None)
            {
                arrows[(int)orientation].arrows[(int)pos].SetActive(true);
            }
        }

        public void ActivateButtons()
        {
            forwardButton.interactable = true;
            backButton.interactable = true;
        }

        private void OnEnable()
        {
            ActivateButtons();

            foreach (TutorialBox box in GameObject.FindObjectsOfType<TutorialBox>())
            {
                if (box == this) continue;

                box.gameObject.SetActive(false);
            }

            Vector2 contentPosition = content.GetComponent<RectTransform>().anchoredPosition;
            content.GetComponent<RectTransform>().anchoredPosition = new Vector2(contentPosition.x, 0);
        }
    }
}
