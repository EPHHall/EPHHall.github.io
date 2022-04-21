using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SS.StatusSpace;
using SS.Spells;
using SS.Item;

namespace SS.UI
{
    public class StatsSubCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public StatsCardInfoBox bigInfoBox;

        [Space(5)]
        [Header("Don't need to be set manually")]
        public Text text;
        public Image icon;
        public Button button;

        public bool shouldDestroy;
        public bool pointerIsOver;

        public Status statusBuiltFrom;
        public Spell spellBuiltFrom;
        public Weapon weaponBuiltFrom;

        public Target currentTarget;

        private void Start()
        {
            text = GetComponentInChildren<Text>();
            icon = GetComponentInChildren<Image>();

            button = GetComponent<Button>();
        }

        public void BuildFromStatus(Status status, Target target)
        {
            if (status == null || target == null) return;

            Start();
            text.text = status.GetName() + "\n" + status.magnitude + " dmg, " + status.duration + " rounds left";

            if (target.targetType.obj)
            {
                text.text += " " + status.radius + " range";
            }

            currentTarget = target;
            spellBuiltFrom = null;
            statusBuiltFrom = status;
            weaponBuiltFrom = null;
        }

        public void BuildFromSpell(Spell spell, Target target)
        {
            if (spell == null || target == null) return;

            Start();
            text.text = spell.spellName + "\n" + spell.damage + " dmg";
            text.text += " " + spell.range + " range";

            currentTarget = target;
            spellBuiltFrom = spell;
            statusBuiltFrom = null;
            weaponBuiltFrom = null;
        }

        public void BuildFromWeapon(Weapon weapon, Target target)
        {
            if (weapon == null || target == null) return;

            Start();
            text.text = weapon.name + "\n" + weapon.toInflict.amount + " dmg";
            text.text += " +" + weapon.rangeMod + " range.";

            currentTarget = target;
            weaponBuiltFrom = weapon;
            statusBuiltFrom = null;
            spellBuiltFrom = null;
        }

        private void OnDisable()
        {
            if (shouldDestroy)
            {
                //probably should make the number a variable. This is fine for testing
                Destroy(gameObject, 5);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                //Debug.Log("In Thing");

                bigInfoBox.card = this;

                bigInfoBox.gameObject.SetActive(true);

                if (statusBuiltFrom != null)
                {    
                    bigInfoBox.BuildFromStatus(statusBuiltFrom, currentTarget);
                }
                else if(spellBuiltFrom != null)
                {
                    bigInfoBox.BuildFromSpell(spellBuiltFrom, currentTarget);
                }
                else if(weaponBuiltFrom != null)
                {
                    bigInfoBox.BuildFromWeapon(weaponBuiltFrom, currentTarget);
                }

                bigInfoBox.DisplayBox();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                pointerIsOver = true;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (SS.GameController.NoInteractableIfObjectsAreActive.noInteract == null || SS.GameController.NoInteractableIfObjectsAreActive.noInteract.CanInteract())
            {
                pointerIsOver = false;
            }
        }
    }
}
