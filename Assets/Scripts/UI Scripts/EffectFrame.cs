using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.Spells;
using UnityEngine.EventSystems;

namespace SS.UI
{
    public class EffectFrame : MagicFrame
    {
        public Effect effect;

        public override void Start()
        {
            base.Start();

            if (content != null && content is Effect)
            {
                currentSprite = (content as Effect).icon;
            }
        }

        public override void SetContent(Object newContent)
        {
            if (newContent is Effect)
            {
                Effect tempEffect = newContent as Effect;

                if (inventoryFrame)
                {
                    effect = tempEffect;
                    base.SetContent(newContent);
                }
                else if (!tempEffect.inUse)
                {
                    effect = tempEffect;
                    base.SetContent(newContent);
                }
            }
            else if (newContent == null)
            {
                effect = null;
                base.SetContent(newContent);
            }

            Tutorial.TutorialHandler.frameAddedTo = this;
        }

        public override void ResetDisplay()
        {
            base.ResetDisplay();

            if (effect != null)
            {
                DisplayIcon(effect.icon);
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            if (content != null && eventData.button == PointerEventData.InputButton.Left && pointerOver)
            {
                SpellCraftingScreen.activeScreen.effectCard.Spawn(effect);
            }
        }
    }
}