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
                if (!frame.dontSetInUseFlags)
                {
                        //Debug.Log("In Thing, 3");
                    if (name == "Effect_ Arcane Bolt(Clone)")
                    {
                    }

                    tempEffect.SetInUse(false, frame);
                }
            }
            if (tempMod != null)
            {
                if (!frame.dontSetInUseFlags)
                {
                        //Debug.Log("In Thing, 4");
                    if (name == "Effect_ Arcane Bolt(Clone)")
                    {
                    }
                    tempMod.SetInUse(false, frame);
                }
            }

            if(SpellCraftingScreen.activeScreen != null)
                SpellCraftingScreen.activeScreen.SpellCraftingScreen_SpellChanged(frame, toRemove);
        }

        public void ChangeInventoryList(int i)
        {
            SpellCraftingScreen.activeScreen.ChangeInventoryList(i);
        }

        public void ChangeSpellName()
        {
            SpellCraftingScreen.activeScreen.ChangeSpellName();
        }

        public void RemoveAndDestroyContents()
        {
            int breakInt = 0;
            while (inventoryParent.transform.childCount > 0)
            {
                Transform formerChild = inventoryParent.transform.GetChild(0);
                formerChild.parent = null;

                if (formerChild.GetComponent<SS.Util.ID>() != null)
                {
                    SS.GameController.DestroyedTracker.instance.TrackDestroyedObject(formerChild.GetComponent<SS.Util.ID>().id);
                }

                Destroy(formerChild.gameObject);

                if (breakInt == 100)
                {
                    break;
                }

                breakInt++;
            }

            //Debug.Log("Spell Inventory while, break = " + breakInt);
        }
    }
}
