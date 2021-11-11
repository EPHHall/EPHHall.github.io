using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.Spells;

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
                effect = newContent as Effect;
                base.SetContent(newContent);
            }
            else if (newContent == null)
            {
                effect = null;
                base.SetContent(newContent);
            }
        }

        public override void ResetDisplay()
        {
            base.ResetDisplay();

            if (effect != null)
            {
                DisplayIcon(effect.icon);
            }
        }
    }
}