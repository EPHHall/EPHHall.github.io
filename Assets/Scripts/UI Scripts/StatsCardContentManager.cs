using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;
using SS.Spells;
using SS.Character;
using SS.Item;

namespace SS.UI
{
    public class StatsCardContentManager : MonoBehaviour
    {
        public float defaultPos;
        public float verticalDelta;
        public StatsCard statsCard;

        public bool statusContent;
        public bool actionContent;
        public bool spellContent;

        public GameObject subCardPrefab;

        public Transform actionButtonParent;

        Target character;
        Target prevCharacter;
        List<StatsSubCard> subCards = new List<StatsSubCard>();
        bool wasEnabled;

        private void Update()
        {
            if (statusContent)
            {
                HandleStatusContent();
            }
            else if (actionContent)
            {
                HandleActionContent();
            }
            else if (spellContent)
            {
                HandleSpellContent();
            }
        }

        public void HandleStatusContent()
        {
            prevCharacter = character;
            int prevStatusNumber = 0;
            if (character != null)
            {
                prevStatusNumber = character.statuses.Count;
            }

            character = statsCard.targets[statsCard.targetIndex];
            int statusNumber = 0;
            if (character != null)
            {
                statusNumber = character.statuses.Count;
            }

            if (character != null)
            {
                if (wasEnabled || prevCharacter != character || prevStatusNumber != statusNumber)
                {
                    wasEnabled = false;

                    for (int i = 0; i < subCards.Count; i++)
                    {
                        subCards[i].shouldDestroy = true;
                        subCards[i].gameObject.SetActive(false);
                    }
                    subCards.Clear();

                    for (int i = 0; i < character.statuses.Count; i++)
                    {
                        StatsSubCard card = Instantiate(subCardPrefab, transform).GetComponent<StatsSubCard>();
                        card.transform.localPosition = new Vector2(0, defaultPos + (i * verticalDelta));
                        card.transform.rotation = Quaternion.identity;

                        card.BuildFromStatus(character.statuses[i], character);

                        subCards.Add(card);
                    }
                }
            }
        }

        public void HandleActionContent()
        {
            prevCharacter = character;
            character = statsCard.targets[statsCard.targetIndex];

            if (character != null)
            {
                if (wasEnabled || prevCharacter != character)
                {
                    wasEnabled = false;

                    for (int i = 0; i < subCards.Count; i++)
                    {
                        subCards[i].shouldDestroy = true;
                        subCards[i].gameObject.SetActive(false);
                    }
                    subCards.Clear();

                    if (character.targetType.creature)
                    {
                        Spell_Attack meleeAttack = character.transform.Find("Melee Attack").GetComponent<Spell_Attack>();
                        Weapon[] weaponChildren = meleeAttack.transform.GetComponentsInChildren<Weapon>();
                        for (int i = 0; i < weaponChildren.Length; i++)
                        {
                            Weapon currentWeapon = weaponChildren[i];

                            Transform parent;
                            if (actionButtonParent != null)
                            {
                                parent = actionButtonParent;
                                actionButtonParent.GetComponent<HighlightActiveWeapon>().SetCharacter(character);
                                actionButtonParent.GetComponent<HighlightActiveWeapon>().SetMeleeAttack(meleeAttack);
                            }
                            else
                                parent = transform;

                            StatsSubCard card = Instantiate(subCardPrefab, parent).GetComponent<StatsSubCard>();
                            card.transform.localPosition = new Vector2(0, defaultPos + (i * verticalDelta));
                            card.transform.rotation = Quaternion.identity;

                            card.BuildFromWeapon(currentWeapon, character);

                            subCards.Add(card);
                        }
                    }
                }
            }
        }

        public Spell[] spellChildren;
        public void HandleSpellContent()
        {
            prevCharacter = character;
            character = statsCard.targets[statsCard.targetIndex];

            if (character != null)
            {
                if (wasEnabled || prevCharacter != character)
                {
                    wasEnabled = false;

                    for (int i = 0; i < subCards.Count; i++)
                    {
                        subCards[i].shouldDestroy = true;
                        subCards[i].gameObject.SetActive(false);
                    }
                    subCards.Clear();

                    spellChildren = character.transform.GetComponentsInChildren<Spell>();
                    int verticalOffset = 0;
                    for (int i = 0; i < spellChildren.Length; i++)
                    {
                        Spell currentSpell = spellChildren[i];

                        if (currentSpell.name == "Melee Attack")
                        {
                            verticalOffset--;
                            continue;
                        }

                        StatsSubCard card = Instantiate(subCardPrefab, transform).GetComponent<StatsSubCard>();
                        card.transform.localPosition = new Vector2(0, defaultPos + ((i + verticalOffset) * verticalDelta));
                        card.transform.rotation = Quaternion.identity;

                        card.BuildFromSpell(currentSpell, character);

                        subCards.Add(card);
                    }
                }
            }
        }



        private void OnEnable()
        {
            wasEnabled = true;
        }
    }
}
