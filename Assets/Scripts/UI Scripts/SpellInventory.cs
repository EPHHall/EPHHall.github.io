using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.Util;
using SS.Spells;

namespace SS.UI
{
    public class SpellInventory : MonoBehaviour
    {
        [Space(5)]
        [Header("Inventory Contents")]
        public Modifier[] modifiers =  new Modifier[18];
        public Effect[] effects = new Effect[9];
        public Spell[] spells = new Spell[4];

        [Space(5)]
        [Header("Crafting Frames")]
        public ModifierFrame[] modifierFrames = new ModifierFrame[20];
        public ModifierFrame[] modifierInventoryFrames = new ModifierFrame[18];
        public EffectFrame[] effectInventoryFrames = new EffectFrame[9];
        public EffectFrame mainFrame;
        public EffectFrame[] deliveredFrames = new EffectFrame[3];
        public EffectFrame[] targetFrames = new EffectFrame[3];

        public GameObject[] inventoryLists = new GameObject[3];
        public int currentInvList = 0;
        public Text listText;

        [Space(5)]
        [Header("Stat Texts")]
        public Text manaCostText;
        public Text damageText;
        public Text actionPointText;

        [Space(5)]
        public Transform inventoryParent;
        public int currentSpellIndex = 0;
        public Text spellName;
        public InputField spellNameInput;
        public Text spellNumber;
        public Text spellPointsText;

        private void Start()
        {
            spellNameInput.text = spells[currentSpellIndex].spellName;
        }

        private void Update()
        {
            UpdateStatTexts();
        }

        public void AddModifier(int index, Modifier mod)
        {
            modifiers[index] = mod;

            modifierInventoryFrames[index].SetContent(mod);
        }

        public void AddEffect(int index, Effect effect)
        {
            effects[index] = effect;

            effectInventoryFrames[index].SetContent(effect);
        }

        public void SpellChanged(MagicFrame frame, Object toRemove)
        {
            EffectFrame effectFrame = frame as EffectFrame;
            ModifierFrame modifierFrame = frame as ModifierFrame;
            Spell currentSpell = spells[currentSpellIndex];

            if (modifierFrame != null && frame.content == null)
            {
                spells[currentSpellIndex].RemoveModifier(toRemove as Modifier);
            }
            else if (frame == mainFrame)
            {
                currentSpell.SetMain(effectFrame.effect);
            }
            else if (ArrayContains.Contains(modifierFrames, modifierFrame))
            {
                currentSpell.AddModifier(modifierFrame.modifier);
            }

            UpdateSpellPointsText(currentSpell.currentSpellPoints, currentSpell.maxSpellPoints);
        }

        public void IncrementCurrentSpellIndex()
        {
            currentSpellIndex++;
            if (currentSpellIndex >= spells.Length)
            {
                currentSpellIndex = 0;
            }

            spellNameInput.text = spells[currentSpellIndex].spellName;
            spellNumber.text = "Spell " + (currentSpellIndex + 1) + ":";
        }

        public void ChangeInventoryList(int amount)
        {
            inventoryLists[currentInvList].SetActive(false);
            currentInvList += amount;

            if (currentInvList >= inventoryLists.Length)
            {
                currentInvList = 0;
            }
            if (currentInvList < 0)
            {
                currentInvList = inventoryLists.Length - 1;
            }

            inventoryLists[currentInvList].SetActive(true);
            listText.text = inventoryLists[currentInvList].name;
        }

        public void ChangeSpellName()
        {
            spells[currentSpellIndex].spellName = spellNameInput.text;
        }

        public void UpdateSpellPointsText(int numerator, int denominator)
        {
            if (numerator < 0)
            {
                spellPointsText.color = Color.red;
            }
            else
            {
                spellPointsText.color = Color.black;
            }

            spellPointsText.text = "" + numerator + "/" + "" + denominator;
        }

        public void UpdateStatTexts()
        {
            manaCostText.text = spells[currentSpellIndex].manaCost + "";
            damageText.text = spells[currentSpellIndex].damage + "";
            actionPointText.text = spells[currentSpellIndex].apCost + "";
        }
    }
}
