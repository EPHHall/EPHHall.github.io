using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.UI
{
    public class InfoBox_NameDisplay : MonoBehaviour
    {
        [System.Serializable]
        public class SpriteAndThreshold
        {
            public Sprite sprite;
            public float threshold;
            public float size;
        }

        public MagicFrame toDisplay;
        public Text text;
        public Canvas canvas = null;
        public string canvasToSearchFor = "Main Canvas";
        public Image image;
        public RectTransform rectTrans;

        public float yOffset;
        public float xOffset;

        //its important for these to be in ascending order
        public SpriteAndThreshold[] sats;

        public float textWidth;

        private void OnEnable()
        {
            canvas = GameObject.Find(canvasToSearchFor).GetComponent<Canvas>();
            image = GetComponent<Image>();
            rectTrans = GetComponent<RectTransform>();

            Init();
        }

        private void Update()
        {
            Init();
        }

        private void Init()
        {
            image.enabled = false;
            text.gameObject.SetActive(false);

            if (toDisplay.content == null) return;

            image.enabled = true;
            text.gameObject.SetActive(true);

            if (toDisplay.content is Spells.Effect)
            {
                text.text = (toDisplay.content as Spells.Effect).effectName;
            }else if (toDisplay.content is Spells.Modifier)
            {
                text.text = (toDisplay.content as Spells.Modifier).modifierName;
            }

            //rectTrans.anchoredPosition = toDisplay.GetComponent<RectTransform>().anchoredPosition + new Vector2(xOffset, yOffset);
            textWidth = text.rectTransform.rect.width * canvas.scaleFactor;

            foreach (SpriteAndThreshold sat in sats)
            {
                if (textWidth <= sat.threshold)
                {
                    image.sprite = sat.sprite;
                    rectTrans.sizeDelta = new Vector2(sat.size, rectTrans.rect.height);
                    break;
                }
            }
        }
    }
}
