using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SS.UI
{
    public class SS_DisplayInfoBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool pointerIsOver;

        //[System.NonSerialized]
        public SS_InfoBox infoBox;

        public float showDelay = .3f;
        public float hideDelay = .1f;
        public float delayTimer;

        [TextArea(6, 6)]
        public string infoString;

        // Start is called before the first frame update
        public virtual void Start()
        {
            if (infoBox == null)
            {
                infoBox = transform.GetChild(0).GetComponent<SS_InfoBox>();
            }
            infoBox.displayer = this;
        }

        // Update is called once per frame
        public virtual void Update()
        {
            if (pointerIsOver && Time.time > delayTimer)
            {
                DisplayInfoBox();
            }
            else if (!pointerIsOver && Time.time > delayTimer)
            {
                HideInfoBox();
            }
        }

        public void DisplayInfoBox()
        {
            if (!infoBox.isActiveAndEnabled)
            {
                HideInfoBoxes();

                infoBox.gameObject.SetActive(true);
                SetInfoString();
            }
        }
        public void HideInfoBoxes()
        {
            foreach (SS_InfoBox infoBox in FindObjectsOfType<SS_InfoBox>())
            {
                infoBox.gameObject.SetActive(false);
            }
        }
        public void HideInfoBox()
        {
            if (infoBox.isActiveAndEnabled)
            {
                infoBox.gameObject.SetActive(false);
            }
        }

        public virtual void SetInfoString()
        {

        }

        public virtual void EvaluateSpecialString(ref string evaluation)
        {

        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            pointerIsOver = true;
            delayTimer = Time.time + showDelay;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            pointerIsOver = false;
            delayTimer = Time.time + hideDelay;
        }
    }
}
