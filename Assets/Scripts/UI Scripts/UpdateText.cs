using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.UI
{
    public class UpdateText : MonoBehaviour
    {
        public bool fading;
        public float activeTime = 2f;
        public float fadeTime = .5f;
        public Vector2 localPos;
        
        private float timer = float.PositiveInfinity;
        private float fadeTimer = -1;

        [Space(5)]
        [Header("Don't Touch")]
        public List<UpdateText> updateTexts = new List<UpdateText>();
        public Text text;

        public virtual void Start()
        {
            text = GetComponent<Text>();
            Deactivate();

            foreach (UpdateText uText in FindObjectsOfType<UpdateText>())
            {
                updateTexts.Add(uText);
            }
        }

        public virtual void SetMessage(string message)
        {
            foreach (UpdateText uText in updateTexts)
            {
                uText.Deactivate();
            }

            text.text = message;
            text.color = Color.white;//new Color(text.color.r, text.color.g, text.color.b, 1);
            timer = Time.time + activeTime;
            fading = false;
        }
        public virtual void SetMessage(string message, Color color)
        {
            foreach (UpdateText uText in updateTexts)
            {
                uText.Deactivate();
            }

            text.text = message;
            text.color = color;
            timer = Time.time + activeTime;
            fading = false;
        }

        public void Deactivate()
        {
            timer = float.PositiveInfinity;
            fadeTimer = -1;

            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }

        public virtual void Update()
        {
            if (timer < Time.time && !fading)
            {
                fading = true;
                fadeTimer = Time.time + fadeTime;
            }

            if (fading && fadeTimer > Time.time)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(0, 1, (fadeTimer - Time.time) / fadeTime));
            }
            else if(fading)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            }
        }
    }
}
