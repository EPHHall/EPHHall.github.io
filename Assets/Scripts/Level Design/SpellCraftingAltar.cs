using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.LevelDesign
{
    public class SpellCraftingAltar : Interactable
    {
        public Sprite[] sprites = new Sprite[3];
        private int spriteIndex;
        public bool altarActivated;
        public bool altarSpent;
        private SpriteRenderer sr;

        [Space(5)]
        [Header("Manipulating Spell Crafting")]
        public UI.SpellCraftingScreen[] screens;

        public override void Start()
        {
            base.Start();

            spriteIndex = 0;
            sr = GetComponent<SpriteRenderer>();
        }

        public override void Update()
        {
            base.Update();

            if (spriteIndex == 0 && room.cleared)
            {
                spriteIndex = 1;
            }

            if (spriteIndex == 1 && altarActivated)
            {
                spriteIndex = 2;
            }

            if (sprites[spriteIndex] != null)
            {
                sr.sprite = sprites[spriteIndex];
            }

            if (altarActivated && !altarSpent)
            {
                if (room != Room.currentlyActive)
                {
                    altarSpent = true;

                    foreach (UI.SpellCraftingScreen screen in screens)
                    {
                        screen.DeactivateFrames();
                    }
                }
                else
                {
                    foreach (UI.SpellCraftingScreen screen in screens)
                    {
                        screen.ActivateFrames();
                    }
                }
            }
        }

        public override void ActivateInteractable()
        {
            base.ActivateInteractable();

            if (!altarSpent)
                altarActivated = true;
        }
    }
}
