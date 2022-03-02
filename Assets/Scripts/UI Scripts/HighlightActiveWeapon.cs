using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.UI
{
    public class HighlightActiveWeapon : MonoBehaviour
    {
        //List<StatsSubCard> cards;
        SS.Spells.Target character;
        SS.Spells.Spell_Attack meleeAttack;

        void Update()
        {
            //cards.Clear();

            if (meleeAttack != null)
            {
                foreach (StatsSubCard card in transform.GetComponentsInChildren<StatsSubCard>())
                {
                    card.GetComponent<Image>().color = Color.white;

                    Item.Weapon weaponInUse;
                    if (meleeAttack.weaponBeingUsed == -1)
                        weaponInUse = meleeAttack.unarmedWeapon;
                    else
                        weaponInUse = meleeAttack.activeWeapons[meleeAttack.weaponBeingUsed];


                    if (card.weaponBuiltFrom != null && card.weaponBuiltFrom == weaponInUse)
                    {
                        card.GetComponent<Image>().color = Color.green;
                    }
                }
            }
        }

        public void SetCharacter(SS.Spells.Target character)
        {
            this.character = character;
        }
        public void SetMeleeAttack(Spells.Spell_Attack meleeAttack)
        {
            this.meleeAttack = meleeAttack;
        }
    }
}
