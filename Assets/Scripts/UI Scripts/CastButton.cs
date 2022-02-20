using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;
using UnityEngine.UI;

namespace SS.UI
{
    public class CastButton : MonoBehaviour
    {
        public Text castButtonText;

        void Update()
        {
            if (SpellManager.activeSpell != null && SpellManager.activeSpell is Spell_Attack)
            {
                castButtonText.text = "Attack";
            }
            else
            {
                castButtonText.text = "Cast";
            }
        }
    }
}
