using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.UI
{
    public class StatMeter : MonoBehaviour
    {
        public SS.Character.CharacterStats stats;
        public Text text;

        [System.NonSerialized]
        public Slider thisSlider;
        [System.NonSerialized]
        public RectTransform rectTransform;

        public virtual void Start()
        {
            thisSlider = GetComponent<Slider>();

            rectTransform = GetComponent<RectTransform>();
        }

        public virtual void Update()
        {

        }

        public virtual void SetStats(SS.Character.CharacterStats newStats)
        {
            stats = newStats;
        }

        public virtual void SetSliderMax(int max)
        {
            thisSlider.maxValue = max;
        }

        public virtual void SetSliderValue(int value)
        {
            thisSlider.value = value;
        }

        public virtual void SetText(int numerator, int denominator)
        {
            if (rectTransform.rect.width <= rectTransform.rect.height)
            {
                text.text = numerator + "\n" + denominator;
            }
            else if (rectTransform.rect.width > rectTransform.rect.height)
            {
                text.text = numerator + "/" + denominator;
            }
        }
        public virtual void SetText()
        {
            if (rectTransform.rect.width <= rectTransform.rect.height)
            {
                text.text = thisSlider.value + "\n" + thisSlider.maxValue;
            }
            else if (rectTransform.rect.width > rectTransform.rect.height)
            {
                text.text = thisSlider.value + "/" + thisSlider.maxValue;
            }
        }
    }

}
