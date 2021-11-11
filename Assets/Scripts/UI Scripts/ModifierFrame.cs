using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.Spells;

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
                modifier = newContent as Modifier;
                base.SetContent(newContent);
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
    }
}
