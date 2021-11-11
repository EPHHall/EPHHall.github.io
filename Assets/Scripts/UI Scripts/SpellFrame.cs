using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;

namespace SS.UI
{
    public class SpellFrame : MonoBehaviour
    {
        private enum ContentType
        {
            Mod,
            Effect,
            Spell
        }
        private ContentType contentType;

        public Object content;
        private Modifier modifier;
        private Effect effect;
        private Spell spell;

        private Sprite icon;

        public void SetContent(Object content)
        {
            if (content is Modifier)
            {

            }
        }

        private void SetIcon()
        {
            if (modifier != null)
            {

            }
            else if (modifier != null)
            {

            }
            else if (modifier != null)
            {

            }
        }
    }
}
