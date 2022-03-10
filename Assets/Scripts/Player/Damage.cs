using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;

namespace SS.Character
{
    [System.Serializable]
    public class Damage
    {
        public enum DamageType
        {
            None,
            Inflame,
            Fire,
            Enchantment,
            Arcane,
            Physical
        }

        public DamageType type;
        public int amount;

        public List<Status> statusesToInflict = new List<Status>();

        public Item.Weapon weapon;

        public bool temporary;

        public Damage(DamageType type, int amount)
        {
            this.type = type;
            this.amount = amount;
            temporary = false;
        }

        public Damage(DamageType type, int amount, bool temporary)
        {
            this.type = type;
            this.amount = amount;
            this.temporary = temporary;
        }

        public void InflictOnTarget(SS.Spells.Target target, Character.CharacterStats stats, Spells.Effect inflictor)
        {
            Util.CharacterStatsInterface.DamageHP(stats, this);

            foreach (Status status in statusesToInflict)
            {
                target.ApplyStatus(status, inflictor);
            }
        }
    }
}
