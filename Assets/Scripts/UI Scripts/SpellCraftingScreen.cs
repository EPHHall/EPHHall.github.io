using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.Spells;

namespace SS.UI
{
    public class SpellCraftingScreen : MonoBehaviour
    {
        public bool screenActive = false;
        public bool forceActive = false;

        public static SpellCraftingScreen activeScreen;
        public SpellInventory spellInventory;

        public Spell spell;

        [Space(5)]
        [Header("Frame Stuff")]
        public EffectFrame mainFrame;
        public List<EffectFrame> secondaryFrames;
        public List<ModifierFrame> modifierFrames;
        public List<EffectFrame> effectFrames_Inventory;
        public ModifierFrame[] modifierFrames_Inventory1 = new ModifierFrame[16];
        public ModifierFrame[] modifierFrames_Inventory2 = new ModifierFrame[16];
        public List<MagicFrame> everyFrame = new List<MagicFrame>();

        [Space(5)]
        [Header("Inventory Stuff")]
        public int currentInvList = 0;
        public List<GameObject> inventoryLists = new List<GameObject>();

        [Space(5)]
        [Header("Text Stuff")]
        public Text nameText;
        public Text spellPointsText;
        public Text manaCostText;
        public Text damageText;
        public Text actionPointsCostText;
        public Text rangeText;
        public Text inventoryListText;
        public InputField spellNameInput;

        [Space(5)]
        [Header("Text Stuff")]
        public EffectCard effectCard;

        [Space(5)]
        [Header("Misc")]
        public bool initialized;

        [Space(5)]
        [Header("Debug")]
        public bool showActiveScreen;

        void Start()
        {
            initialized = false;

            everyFrame.Add(mainFrame);

            foreach (MagicFrame frame in secondaryFrames)
            {
                everyFrame.Add(frame);
            }
            foreach (MagicFrame frame in modifierFrames)
            {
                everyFrame.Add(frame);
            }
            foreach (MagicFrame frame in modifierFrames_Inventory1)
            {
                everyFrame.Add(frame);
            }
            foreach (MagicFrame frame in modifierFrames_Inventory2)
            {
                everyFrame.Add(frame);
            }
            foreach (MagicFrame frame in effectFrames_Inventory)
            {
                everyFrame.Add(frame);
            }

            if(!screenActive)
            {
                DeactivateFrames();
            }
        }

        void Update()
        {
            if (showActiveScreen)
            {
                Debug.Log(activeScreen.name, activeScreen.gameObject);
            }

            UpdateStatTexts();

            if (forceActive)
            {
                ActivateFrames();
            }
        }

        public void ActivateFrames()
        {
            //Debug.Log("In Activate Screens");

            foreach (MagicFrame frame in everyFrame)
            {
                frame.activeFrame = true;
            }
        }
        public void DeactivateFrames()
        {
            //Debug.Log("In Deactivate Screens");

            foreach (MagicFrame frame in everyFrame)
            {
                frame.activeFrame = false;
            }
        }

        public void AddModifierToInventory(Modifier mod, int index, int max)
        {
            if (modifierFrames_Inventory1[index] == null)
            {
                modifierFrames_Inventory1[index].content = mod;
            }
            else if (modifierFrames_Inventory2[index] == null)
            {
                modifierFrames_Inventory2[index].content = mod;
            }
        }
        public void AddEffectToInventory(Effect effect, int index, int max)
        {
            if (effectFrames_Inventory[index] == null)
            {
                effectFrames_Inventory[index].content = effect;
            }
        }
        public void SpellCraftingScreen_SpellChanged(MagicFrame frame, Object toRemove)
        {
            EffectFrame effectFrame = frame as EffectFrame;
            ModifierFrame modifierFrame = frame as ModifierFrame;

            if (modifierFrame != null && frame.content == null)
            {
                spell.RemoveModifier(toRemove as Modifier);
            }
            else if (frame == SpellCraftingScreen.activeScreen.mainFrame)
            {
                spell.SetMain(effectFrame.effect);
            }
            else if (effectFrame != null && SpellCraftingScreen.activeScreen.secondaryFrames.Contains(effectFrame))
            {
                spell.SetDelivered(effectFrame.effect, SpellCraftingScreen.activeScreen.secondaryFrames.IndexOf(effectFrame));
            }
            else if (SpellCraftingScreen.activeScreen.modifierFrames.Contains(modifierFrame))
            {
                spell.AddModifier(modifierFrame.modifier);
            }

            UpdateSpellPointsText(spell.currentSpellPoints, spell.maxSpellPoints);
        }

        public void ChangeInventoryList(int i)
        {
            inventoryLists[currentInvList].SetActive(false);
            currentInvList += i;

            if (currentInvList >= inventoryLists.Count)
            {
                currentInvList = 0;
            }
            if (currentInvList < 0)
            {
                currentInvList = inventoryLists.Count - 1;
            }

            inventoryLists[currentInvList].SetActive(true);
            inventoryListText.text = inventoryLists[currentInvList].name;
        }
        public void ChangeInventoryList(bool initial)
        {
            //Since this function is called during OnEnabled, I dont know if setting the inventory list to inactive
            //will be an issue timing wise.
            inventoryLists[currentInvList].SetActive(false);
            currentInvList = 0;

            if (currentInvList >= inventoryLists.Count)
            {
                currentInvList = 0;
            }
            if (currentInvList < 0)
            {
                currentInvList = inventoryLists.Count - 1;
            }

            inventoryLists[currentInvList].SetActive(true);
            inventoryListText.text = inventoryLists[currentInvList].name;
        }

        public void UpdateStatTexts()
        {
            manaCostText.text = spell.manaCost + "";
            damageText.text = spell.damage + "";
            actionPointsCostText.text = spell.apCost + "";
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

        public void ChangeSpellName()
        {
            spell.spellName = spellNameInput.text;
        }

        public void ResetFrames()
        {
            for (int i = 0; i < modifierFrames_Inventory1.Length; i++)
            {
                modifierFrames_Inventory1[i].ResetFrame();
                modifierFrames_Inventory2[i].ResetFrame();
                effectFrames_Inventory[i].ResetFrame();
            }

            for (int i = 0; i < spellInventory.modifiers.Length; i++)
            {
                if (i < modifierFrames_Inventory1.Length)
                {
                    modifierFrames_Inventory1[i].SetContent(spellInventory.modifiers[i]);
                }
                else
                {
                    modifierFrames_Inventory2[i - modifierFrames_Inventory2.Length].SetContent(spellInventory.modifiers[i]);
                }
            }

            for (int i = 0; i < spellInventory.effects.Length; i++)
            {
                effectFrames_Inventory[i].SetContent(spellInventory.effects[i]);
            }
        }

        public void ResetFunctionalFrames()
        {
            for (int i = 0; i < modifierFrames.Count; i++)
            {
                modifierFrames[i].ResetFrame();
            }
            for (int i = 0; i < secondaryFrames.Count; i++)
            {
                secondaryFrames[i].ResetFrame();
            }

            mainFrame.ResetFrame();
        }

        private void ApplyFrames()
        {
            spell.SetMain(mainFrame.effect);

            /*for (int i = 0; i < secondaryFrames.Count; i++)
            {
                spell.SetDelivered(secondaryFrames[i].effect, i);
            }*/

            for (int i = 0; i < modifierFrames.Count; i++)
            {
                spell.AddModifier(modifierFrames[i].modifier);
            }
        }

        public void ResetScreen()
        {
            activeScreen = this;

            spellNameInput.text = spell.spellName;
            //spellNumber.text = "Spell " + (currentSpellIndex + 1) + ":";

            ResetFrames();

            ApplyFrames();

            ChangeInventoryList(true);

            UpdateSpellPointsText(spell.currentSpellPoints, spell.maxSpellPoints);
        }

        private void OnEnable()
        {
            ResetScreen();
        }

        private void OnDisable()
        {
            initialized = false;

            if (activeScreen == this)
            {
                activeScreen = null;
            }
        }
    }
}