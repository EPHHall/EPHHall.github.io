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
        public bool creature;
        public bool obj;
        public bool weapon;
        public bool tile;

        public TargetType()
        {
        }

        public TargetType(bool creature, bool obj, bool weapon, bool tile)
        {
            this.creature = creature;
            this.obj = obj;
            this.weapon = weapon;
            this.tile = tile;
        }

        public TargetType(bool setting)
        {
            this.creature = setting;
            this.obj = setting;
            this.weapon = setting;
            this.tile = setting;
        }

        public bool CompareTypes(TargetType otherType)
        {
            return creature == otherType.creature && obj == otherType.obj && weapon == otherType.weapon
                && tile == otherType.tile;
        }

        //Is at least 1 type true for both TargetTypes
        public bool DoesGTETOneTypeMatch(TargetType otherType)
        {
            return (creature && otherType.creature) || (obj && otherType.obj) || (weapon && otherType.weapon)
                || (tile && otherType.tile);
        }

        public override string ToString()
        {
            return "Creature: " + creature + ", Object: " + obj + ", Weapon: " + weapon +
                   ", Tile: " + tile;
        }
    }
}
