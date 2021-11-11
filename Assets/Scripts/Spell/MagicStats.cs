using System.Collections;
using System.Collections.Generic;

namespace SS.Spells
{
    public class MagicStats
    {
        int damage = 0;
        int castSpeed = 0;
        int range = 0;
        int manaCost = 0;
        string description = "";

        public MagicStats()
        { }

        public MagicStats(MagicStats newStats)
        {
            damage = newStats.damage;
            castSpeed = newStats.castSpeed;
            range = newStats.range;
            manaCost = newStats.manaCost;
            description = newStats.description;
        }

        public bool ModifyDamage(int damageModifier)
        {
            damage += damageModifier;
            if (damage <= 0)
            {
                damage -= damageModifier;
                return false;
            }

            return true;
        }

        public bool ModifyCastSpeed(int castSpeedModifier)
        {
            castSpeed += castSpeedModifier;
            if (damage <= 1)
            {
                damage -= castSpeedModifier;
                return false;
            }

            return true;
        }

        public bool ModifyRange(int rangeModifier)
        {
            range += rangeModifier;
            if (damage <= 0)
            {
                damage -= rangeModifier;
                return false;
            }

            return true;
        }

        public bool ModifyManaCost(int manaCostModifier)
        {
            manaCost += manaCostModifier;
            if (damage <= 1)
            {
                damage -= manaCostModifier;
                return false;
            }

            return true;
        }

        public void SetDescription(string newDescription)
        {
            description = newDescription;
        }

        public void ModifyDescription(string descriptionModifier)
        {
            description += descriptionModifier;
        }

        public int GetDamage()
        {
            return damage;
        }

        public int GetCastSpeed()
        {
            return castSpeed;
        }

        public int GetRange()
        {
            return range;
        }

        public int GetManaCost()
        {
            return manaCost;
        }

        public string GetDescription()
        {
            return description;
        }
    }
}
