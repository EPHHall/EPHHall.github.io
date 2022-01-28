using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.Spells;
using UnityEngine.EventSystems;

namespace SS.UI
{
    public class ModifierFrame : MagicFrame
    {
        public Modifier modifier;

        public override void Start()
        {
            base.Start();

            if (content != null && content is Modifier)
            {
                currentSprite = (content as Modifier).icon;
            }
        }

        public override void SetContent(Object newContent)
        {
            if (newContent is Modifier)
            { 
                Modifier tempModifier = newContent as Modifier;

                if (!tempModifier.inUse)
                {
                    modifier = tempModifier;
                    base.SetContent(newContent);
                }
            }
            else if (newContent == null)
            {
                modifier = null;
                base.SetContent(newContent);
            }
        }

        public override void ResetDisplay()
        {
            base.ResetDisplay();

            if (modifier != null)
            {
                DisplayIcon(modifier.icon);
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            if (content != null && pointerOver)
            {
                SpellCraftingScreen.activeScreen.effectCard.Spawn(modifier);
            }
        }
    }
}
