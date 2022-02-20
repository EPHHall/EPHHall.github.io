using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Character;

namespace SS.Util
{
    public class CharacterStatsInterface
    {
        public static void DamageHP(CharacterStats character, Damage damage)
        {
            if (character != null)
            {
                character.hp -= damage.amount;
                if (character.textTemp != null)
                {
                    character.DisplayText("Took " + damage + " damage.");
                }
            }
        }

        public static void DamageMana(CharacterStats character, int damage)
        {
            //Debug.Log("In Thing");

            if (character != null)
            {
                //Debug.Log("In Thing 2");

                //SS.GameController.TurnManager.PrintCurrentTurnTaker();
                //Debug.Log(character.name);

                character.mana -= damage;
            }
        }

        public static void DamageActionPoints(CharacterStats character, int damage)
        {
            if (character != null)
            {
                character.actionPoints -= damage;
            }
        }

        public static void ResetMana(CharacterStats character)
        {
            character.ResetMana();
        }

        public static void ResetAP(CharacterStats character)
        {
            character.ResetAP();
        }

        public static void ResetHealth(CharacterStats character)
        {
            character.ResetHealth();
        }
    }
}
