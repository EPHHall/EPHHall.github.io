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
                Spell_Attack spellAttack = SpellManager.activeSpell as Spell_Attack;
                Item.Weapon weapon = spellAttack.activeWeapons[spellAttack.weaponBeingUsed];

                if (weapon.spellToCast != null && weapon.spellToCast.wand != null)
                {
                    castButtonText.text = "Cast " + weapon.spellToCast.wand.spellUses + "/" + weapon.spellToCast.wand.spellUsesMax;
                }
                else
                {
                    castButtonText.text = "Attack";
                }

            }
            else
            {
                castButtonText.text = "Cast";
            }
        }
    }
}
