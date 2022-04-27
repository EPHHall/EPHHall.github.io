using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    public class EffectPickup : MagicPickup
    {
        public GameObject toPickUp;

        private void Start()
        {
            
        }

        public override void BePickedUp()
        {
            base.BePickedUp();

            int index = SS.Util.GetEmptyArrayIndex.GetArrayIndex(spellInventory.effects);
            if (index != -1)
            {
                Effect effect = Instantiate(toPickUp, spellInventory.inventoryParent.position, Quaternion.identity, spellInventory.inventoryParent).GetComponent<Effect>();

                spellInventory.AddEffect(index, effect);

                DisplayMessage("Effect picked up.");

                if (GetComponent<SS.Util.ID>() != null && SS.GameController.DestroyedTracker.instance != null)
                {
                    SS.GameController.DestroyedTracker.instance.TrackDestroyedObject(GetComponent<SS.Util.ID>().id);
                }

                Destroy(gameObject);
            }
            else
            {
                DisplayMessage("Not enough space!");
            }
        }
    }
}
