using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.Character;
using SS.Spells;
using UnityEngine.EventSystems;

namespace SS.UI
{
    public class StatsCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [System.Serializable]
        public struct StatsCardLayout
        {
            public Sprite background;
            public GameObject content;
            public Vector2 contentPos;
        }

        public Vector2 leftPos;
        public Vector2 rightPos;

        public RectTransform rectTransform;

        public bool setInStart;

        public Transform tabsParent;

        [Space(5)]
        [Header("Info to Fill In")]
        public Text characterName;
        public Text characterDescription;
        public Image characterIcon;
        public StatMeterHealth characterHealthMeter;

        [Space(5)]
        [Header("Showing Abilities")]
        public Button abilityButtonPrefab;

        //[Space(5)]
        //[Header("Description Variables")]

        [Space(5)]
        [Header("Don't Touch")]
        public bool canDeactivate = true;
        public bool firstActivation = true;
        public CharacterStats statsToDisplay;
        private bool deactivateOnFirstFrame = false;
        public bool pointerIsOver = false;
        private Image image;
        public GameObject[] contents;
        public List<Target> targets;
        public int targetIndex = 0;
        //public Dictionary<string, string> evaluateOn;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();

            image = GetComponent<Image>();
        }

        private void Update()
        {
            if (pointerIsOver)
            {
                canDeactivate = false;
            }

            if (deactivateOnFirstFrame)
            {
                gameObject.SetActive(false);

                deactivateOnFirstFrame = false;
            }

            //**************************************************
            //if (Input.GetKeyDown(KeyCode.Mouse0) && canDeactivate)
            //{
            //    gameObject.SetActive(false);
            //}
            //**************************************************


            if (targets.Count > 0)
            {
                if (targetIndex >= targets.Count)
                {
                    targetIndex = targets.Count - 1;
                }

                targetIndex = Mathf.Max(0, targetIndex);

                targets[targetIndex].SelectTarget(targets[targetIndex]);
            }

            canDeactivate = true;
        }

        public void ChangeLayout(StatsCardLayout layout)
        {
            image.sprite = layout.background;
            foreach (GameObject content in contents)
            {
                content.SetActive(false);
            }

            layout.content.SetActive(true);
            layout.content.GetComponent<RectTransform>().anchoredPosition = layout.contentPos;
        }

        public void SetPosition(bool right)
        {
            firstActivation = false;

            if (right)
            {
                rectTransform.anchoredPosition = rightPos;
                tabsParent.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                rectTransform.anchoredPosition = leftPos;
                tabsParent.localScale = new Vector3(-1, 1, 1);
            }
        }

        public void SetStatsToDisplay(CharacterStats stats)
        {
            statsToDisplay = stats;

            if (statsToDisplay != null)
            {
                targetIndex = targets.IndexOf(stats.GetComponent<Target>());

                Tutorial.TutorialHandler.TargetSelectedEventHandler(stats.GetComponent<Target>());
            }

            BuildCard();
            //characterIcon = stats.icon;
        }

        public void BuildCard()
        {
            if (targetIndex >= targets.Count)
            {
                targetIndex = targets.Count - 1;
            }

            if (statsToDisplay != null)
            {
                characterName.text = statsToDisplay.name;
                characterHealthMeter.SetStats(statsToDisplay);

                HandleDescription(statsToDisplay);
            }
            else
            {
                if (targets[targetIndex].targetName != "")
                    characterName.text = targets[targetIndex].targetName;
                else
                {
                    characterName.text = targets[targetIndex].name;
                }

                characterHealthMeter.SetStats(null);

                HandleDescription(targets[targetIndex]);
            }
        }

        public void ActivateStatsCard(float positionX, List<Target> targets, CharacterStats stats)
        {
            gameObject.SetActive(true);
            SetPosition(positionX < Camera.main.transform.position.x);
            this.targets = targets;
            SetStatsToDisplay(stats);
            canDeactivate = false;
        }

        public void DeactivateStatsCard()
        {
            gameObject.SetActive(false);
        }

        public void HandleDescription(CharacterStats stats)
        {
            characterDescription.text = "";

            if (/*stats.characterFollower == null || stats.characterFollower.showing*/ true)
            {
                characterDescription.text += stats.description;

                if (Spells.SpellManager.activeSpell != null)
                {
                    Spells.Spell spell = Spells.SpellManager.activeSpell;

                    characterDescription.text += "\n";
                    characterDescription.text += "You are about to cast " + spell.name + ". " +
                        stats.name + " will take " + spell.damage + " damage.";

                    if (spell.statusesDescription != "")
                    {
                        characterDescription.text += " They also suffer: " + spell.statusesDescription + ".";
                    }
                }
            }
        }
        public void HandleDescription(Target target)
        {
            characterDescription.text = "Buh";

            //if (/*stats.characterFollower == null || stats.characterFollower.showing*/ true)
            //{
            //    characterDescription.text += stats.description;

            //    if (Spells.SpellManager.activeSpell != null)
            //    {
            //        Spells.Spell spell = Spells.SpellManager.activeSpell;

            //        characterDescription.text += "\n";
            //        characterDescription.text += "You are about to cast " + spell.name + ". " +
            //            stats.name + " will take " + spell.damage + " damage.";

            //        if (spell.statusesDescription != "")
            //        {
            //            characterDescription.text += " They also suffer: " + spell.statusesDescription + ".";
            //        }
            //    }
            //}
        }

        public void ChangeTarget(int by)
        {
            targetIndex += by;

            if (targetIndex < 0)
            {
                targetIndex = targets.Count - 1;
            }
            else if (targetIndex >= targets.Count)
            {
                targetIndex = 0;
            }

            statsToDisplay = targets[targetIndex].GetComponent<CharacterStats>();

            BuildCard();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            pointerIsOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            pointerIsOver = false;
        }

        public void OnEnable()
        {
            foreach (AbilityButton abilityButton in FindObjectsOfType<AbilityButton>())
            {
                Destroy(abilityButton.gameObject);
            }
        }

        private void OnDisable()
        {
            statsToDisplay = null;
        }
    }
}
