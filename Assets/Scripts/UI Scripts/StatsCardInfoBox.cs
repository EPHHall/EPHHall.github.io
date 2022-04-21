using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.StatusSpace;
using SS.Spells;
using SS.Item;
using SS.Character;
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

            text.text = status.GetName() + " " + status.explanation_DealsDamage + "\n" +
                status.explanation_Main;
        }
        public void BuildFromSpell(Spell spell, Target target)
        {
            contentName.text = spell.spellName;

            if (spell.main == null)
            {
                text.text = "No main effect.";
                return;
            }

            text.text = spell.spellName + ": Invokes " + spell.main.effectName + ", which is targeted by ";

            if (spell.targetMain.Count == 0)
            {
                text.text += "nothing,";
            }
            else
            {
                //Effect e = spell.targetMain[0];

                //text.text += e.effectName + ", "
            }
            for (int i = 0; i < spell.targetMain.Count; i++)
            {
                Effect e = spell.targetMain[i];

                if (i == spell.targetMain.Count - 1 && i != 0)
                {
                    text.text += " and " + e.effectName + ", ";
                }
                else
                {
                    text.text += e.effectName + ", ";
                }
            }

            text.text += "and delivers ";
            if (spell.deliveredByMain.Count == 0)
            {
                text.text += "nothing.";
            }
            for (int i = 0; i < spell.deliveredByMain.Count; i++)
            {
                Effect e = spell.deliveredByMain[i];

                if (i == spell.deliveredByMain.Count - 1 && i != 0)
                {
                    text.text += " and " + e.effectName + ".";
                }
                else
                {
                    text.text += e.effectName + ", ";
                }
            }

        }
        public void BuildFromWeapon(Weapon weapon, Target target)
        {
            contentName.text = weapon.name;

            text.text = weapon.name +
                ": " + weapon.name + " deals " + weapon.toInflict.amount + " damage.\n" +
                "Switched to this weapon.";

            if (target.GetComponent<CharacterStats>())
            {
                target.GetComponent<CharacterStats>().meleeAttack.ChangeWeapon(
                    target.GetComponent<CharacterStats>().meleeAttack.activeWeapons.IndexOf(weapon));
            }
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
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                card.pointerIsOver = true;
                pointerIsOver = true;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                card.pointerIsOver = false;
                pointerIsOver = false;
            }
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
