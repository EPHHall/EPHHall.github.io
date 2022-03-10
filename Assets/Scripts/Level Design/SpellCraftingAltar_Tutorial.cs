using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.LevelDesign
{
    public class SpellCraftingAltar_Tutorial : SpellCraftingAltar
    {
        public override void Start()
        {
            base.Start();

            deactivateAfterUse = false;
        }

        public override void ActivateInteractable()
        {
            base.ActivateInteractable();

            if (!altarSpent)
            {
                Tutorial.TutorialHandler.altarWasActivated = true;
            }
        }
    }
}
