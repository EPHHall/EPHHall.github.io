using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.Character;
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
        private bool pointerIsOver = false;
        private Image image;
        public GameObject[] contents;
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

            if (Input.GetKeyDown(KeyCode.Mouse0) && canDeactivate)
            {
                gameObject.SetActive(false);
            }

            if (statsToDisplay != null)
            {
                statsToDisplay.GetComponent<Spells.Target>().SelectTarget();
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

            characterName.text = stats.name;
            characterHealthMeter.SetStats(statsToDisplay);

            HandleDescription(stats);
            //characterIcon = stats.icon;
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
            else
            {
                //characterDescription.text += stats.name + "'s Potential Actions:";

                //RectTransform charDescriptionTransform = characterDescription.GetComponent<RectTransform>();
                ////the position needs to be relative, so that is set later
                //Button moveButton = Instantiate(abilityButtonPrefab, Vector2.zero, Quaternion.identity, characterDescription.transform.parent);

                //RectTransform moveButtonTransform = moveButton.GetComponent<RectTransform>();
                //moveButtonTransform.anchoredPosition = charDescriptionTransform.anchoredPosition;
                //Debug.Log(charDescriptionTransform.rect.height);
                //moveButtonTransform.anchoredPosition -= new Vector2(0, charDescriptionTransform.rect.height);
                //moveButton.GetComponentInChildren<Text>().text = "Move";

                //AI.Agent agent = stats.GetComponent<AI.Agent>();
                //RectTransform previousButton = moveButtonTransform;
                //if (agent != null)
                //{
                //    //foreach (Spells.Spell s in agent.spells)
                //    //{
                //    //    Button button = Instantiate(abilityButtonPrefab, Vector2.zero, Quaternion.identity, characterDescription.transform.parent);

                //    //    RectTransform buttonTrans = button.GetComponent<RectTransform>();
                //    //    buttonTrans.anchoredPosition = previousButton.anchoredPosition;
                //    //    buttonTrans.anchoredPosition -= new Vector2(0, previousButton.rect.height);
                //    //    moveButton.GetComponentInChildren<Text>().text = s.name;
                //    //    previousButton = buttonTrans;
                //    //}
                //}
            }
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
