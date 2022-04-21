using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.LevelDesign
{
    public class SpellCraftingAltar_ClassRestriction : SpellCraftingAltar
    {
        [Space(10)]
        [Header("Class Restriction Stuff")]
        public string classToCheckFor;

        public override void ActivateInteractable()
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Character.CharacterStats>().className == classToCheckFor)
            {
                base.ActivateInteractable();
            }
        }
    }
}
