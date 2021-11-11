using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    public class ModifierPickup : MagicPickup
    {
        public GameObject toPickUp;

        public override void BePickedUp()
        {
            base.BePickedUp();

            int index = SS.Util.GetEmptyArrayIndex.GetArrayIndex(spellInventory.modifiers);
            if (index != -1)
            {
                Modifier modifier = Instantiate(toPickUp, spellInventory.inventoryParent.position, Quaternion.identity, spellInventory.inventoryParent).GetComponent<Modifier>();
                spellInventory.AddModifier(index, modifier);

                DisplayMessage("Modifier picked up.");

                Destroy(gameObject);
            }
            else
            {
                DisplayMessage("Not enough space!");
            }
        }
    }
}
