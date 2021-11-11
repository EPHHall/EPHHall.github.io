using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.Spells;
using UnityEngine.EventSystems;

namespace SS.UI
{
    public class TargetMenuRadial : MonoBehaviour
    {
        public GameObject graphic;

        [Space(5)]
        [Header("Buttons")]
        public TargetButtonRadialMenu enemyButton;
        public TargetButtonRadialMenu allyButton;
        public TargetButtonRadialMenu structureButton;
        public TargetButtonRadialMenu itemButton;
        public TargetButtonRadialMenu spellButton;
        public TargetButtonRadialMenu tileButton;
        public TargetButtonRadialMenu weaponButton;
        public enum ButtonType
        { enemyButton, allyButton, structureButton, itemButton, spellButton, tileButton, weaponButton }

        [Space(5)]
        [Header("Positioning")]
        public Transform player;

        [Space(5)]
        [Header("Highlighting/Selecting")]
        public GameObject buttonHighlight;

        [System.NonSerialized]
        public bool dontDisableThisFrame;
        private int dontDisableTimer;
        private TargetType typesDetected;
        private List<TargetButtonRadialMenu> buttons = new List<TargetButtonRadialMenu>();

        private void Start()
        {
            buttons.Add(enemyButton);
            buttons.Add(allyButton);
            buttons.Add(structureButton);
            buttons.Add(itemButton);
            buttons.Add(spellButton);
            buttons.Add(tileButton);
            buttons.Add(weaponButton);

            //ActivateAndPlaceMenu();
        }

        private void Update()
        {
            if (dontDisableThisFrame)
            {
                dontDisableTimer = 1;
                dontDisableThisFrame = false;
            }

            if (Input.anyKeyDown && dontDisableTimer <= 0)
            {
                DeactivateMenu();
            }

            dontDisableTimer = dontDisableTimer > 0 ? dontDisableTimer - 1 : dontDisableTimer = 0;
        }

        public void EnableButton(ButtonType buttonType)
        {
            buttons[(int)buttonType].gameObject.SetActive(true);
        }
        public void EnableButton(int index)
        {
            buttons[index].gameObject.SetActive(true);
        }

        public void DisableButton(ButtonType buttonType)
        {
            buttons[(int)buttonType].gameObject.SetActive(false);
        }
        public void DisableButton(int index)
        {
            buttons[index].gameObject.SetActive(false);
        }

        public void DisableAllButtons()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                DisableButton(i);
            }
        }

        public void ActivateAndPlaceMenu()
        {
            graphic.SetActive(true);

            //fresh slate, now we just enable the buttons corresponding to the targets
            DisableAllButtons();

            PlaceMenu();

            dontDisableThisFrame = true;

            HighlightButtons();
        }

        public void PlaceMenu()
        {
            if (CastingTile.selectedTiles.Count > 0)
            {
                GetComponent<RectTransform>().position = CastingTile.selectedTiles[0].transform.position;
            }
            else
            {
                GetComponent<RectTransform>().position = player.position;
            }
        }

        public void HighlightButtons()
        {
            foreach (CastingTile tile in CastingTile.selectedTiles)
            {
                foreach (Target target in tile.targets)
                {
                    ButtonType buttonType = TargetTypeToButtonType(target.targetType);
                    EnableButton(buttonType);

                    buttons[(int)buttonType].SetAssociatedTarget(target);
                }
            }
        }

        public ButtonType TargetTypeToButtonType(TargetType targetType)
        {
            if (targetType.enemy)
                return ButtonType.enemyButton;
            else if (targetType.ally)
                return ButtonType.allyButton;
            else if (targetType.structure)
                return ButtonType.structureButton;
            else if (targetType.item)
                return ButtonType.itemButton;
            else if (targetType.spell)
                return ButtonType.spellButton;
            else if (targetType.tile)
                return ButtonType.tileButton;
            else
                return ButtonType.weaponButton;
        }

        public void DeactivateMenu()
        {
            buttonHighlight.SetActive(false);

            graphic.SetActive(false);
        }

        //public void OnPointerEnter(PointerEventData eventData)
        //{
        //    dontDisableThisFrame = true;
        //}

        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    //pointerIsOver = false;
        //}
    }
}
