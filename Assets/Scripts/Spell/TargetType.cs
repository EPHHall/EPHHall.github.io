using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    [System.Serializable]
    public class TargetType
    {
        public bool toggleNone;
        public bool toggleAll;

        [Space(5)]
        public bool enemy;
        public bool ally;
        public bool structure;
        public bool item;
        public bool spell;
        public bool tile;
        public bool weapon;
        public bool effect;

        public TargetType()
        {
        }

        public TargetType(bool enemy, bool ally, bool structure, bool item, bool spell, bool tile, bool weapon, bool effect)
        {
            this.enemy = enemy;
            this.ally = ally;
            this.structure = structure;
            this.item = item;
            this.spell = spell;
            this.tile = tile;
            this.weapon = weapon;
            this.effect = effect;
        }

        public TargetType(bool setting)
        {
            enemy = setting;
            ally = setting;
            structure = setting;
            item = setting;
            spell = setting;
            tile = setting;
            weapon = setting;
            effect = setting;
        }

        public bool CompareTypes(TargetType otherType)
        {
            return enemy == otherType.enemy && ally == otherType.ally && structure == otherType.structure
                && item == otherType.item && spell == otherType.spell && tile == otherType.tile
                && weapon == otherType.weapon && effect == otherType.effect;
        }

        //Is at least 1 type true for both TargetTypes
        public bool DoesGTETOneTypeMatch(TargetType otherType)
        {
            return (enemy && otherType.enemy) || (ally && otherType.ally) || (structure && otherType.structure)
                || (item && otherType.item) || (spell && otherType.spell) || (tile && otherType.tile)
                || (weapon && otherType.weapon) || (effect && otherType.effect);
        }

        public override string ToString()
        {
            return "Enemy: " + enemy + ", Ally: " + ally + ", Structure: " + structure +
                   ", Item: " + item + ", Spell: " + spell + ", Tile: " + tile +
                   ", Weapon: " + weapon + ", Effect: " + effect;
        }
    }
}
