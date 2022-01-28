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
        public Modifier[] modifiers =  new Modifier[32];
        public Effect[] effects = new Effect[16];
        public Spell[] spells = new Spell[4];

        [Space(5)]
        [Header("Spell Crafting Screens")]
        public List<SpellCraftingScreen> screens = new List<SpellCraftingScreen>();

        [Space(5)]
        public Transform inventoryParent;
        public int currentSpellIndex = 0;
        public Text spellName;
        public Text spellNumber;
        public Text spellPointsText;

        private void Start()
        {
        }

        private void Update()
        {
        }

        public void AddModifier(int index, Modifier mod)
        {
            modifiers[index] = mod;
        }

        public void AddEffect(int index, Effect effect)
        {
            effects[index] = effect;
        }

        public void SpellChanged(MagicFrame frame, Object toRemove)
        {
            Effect tempEffect = toRemove as Effect;
            Modifier tempMod = toRemove as Modifier;

            if (tempEffect != null)
            {
                if(!frame.dontSetInUseFlags)
                    tempEffect.SetInUse(false, frame);
            }
            if (tempMod != null)
            {
                if(!frame.dontSetInUseFlags)
                    tempMod.SetInUse(false, frame);
            }

            SpellCraftingScreen.activeScreen.SpellChanged(frame, toRemove);
        }

        public void ChangeInventoryList(int i)
        {
            SpellCraftingScreen.activeScreen.ChangeInventoryList(i);
        }

        public void ChangeSpellName()
        {
            SpellCraftingScreen.activeScreen.ChangeSpellName();
        }
    }
}
