using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.UI
{
    public class SpellScreenGreyOut : MonoBehaviour
    {
        EffectFrame frameToCheck;
        Image image;

        private void Start()
        {
            frameToCheck = FindObjectOfType<EffectFrame>();
            image = GetComponent<Image>();
        }

        void Update()
        {
            if(frameToCheck.activeFrame)
            {
                image.enabled = false;
            }
            else
            {
                image.enabled = true;
            }
        }
    }
}
