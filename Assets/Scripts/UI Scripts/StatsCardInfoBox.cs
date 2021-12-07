using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.StatusSpace;
using SS.Spells;
using UnityEngine.EventSystems;

namespace SS.UI
{
    public class StatsCardInfoBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Text text;
        public Text contentName;

        public Transform bigInfoBoxPositionLeft;
        public Transform bigInfoBoxPositionRight;

        public StatsSubCard card;

        public bool pointerIsOver;

        private void Start()
        {
            bigInfoBoxPositionLeft = GameObject.Find("Big Info Box Position Left").transform;
            bigInfoBoxPositionRight = GameObject.Find("Big Info Box Position Right").transform;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0) && !pointerIsOver)
            {
                HideBox();
            }
        }

        public void BuildFromStatus(Status status, Target target)
        {
            contentName.text = status.GetName();

            text.text = status.GetName() + status.explanation_DealsDamage;
        }
        public void BuildFromSpell(Spell spell, Target target)
        {
            contentName.text = spell.spellName;

            text.text = spell.spellName + ": Spell explanation here";
        }

        public void DisplayBox()
        {
            RectTransform trans = GetComponent<RectTransform>();

            if (GameObject.FindObjectOfType<StatsCard>().transform.Find("Tabs").transform.localScale.x < 0)
            {
                trans.position = bigInfoBoxPositionRight.position;
            }
            else
            {
                trans.position = bigInfoBoxPositionLeft.position;
            }

        }

        public void HideBox()
        {
            gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            card.pointerIsOver = true;
            pointerIsOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            card.pointerIsOver = false;
            pointerIsOver = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ////Debug.Log("in Thing");

            //if (!pointerIsOver && isActiveAndEnabled)
            //{
            //    HideBox();
            //    //Debug.Log("in Thing");
            //}
        }
    }
}
