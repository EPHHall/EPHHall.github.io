using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;

namespace SS.Item
{
    [ExecuteAlways]
    public class Weapon : Item
    {
        //stats
        public int damageMod;
        public int speedMod;
        public int rangeMod;

        public Spell attack;

        public void Awake()
        {
            attack.damage += damageMod;
            attack.castSpeed += speedMod;
            attack.range += rangeMod;
        }

        public void Update()
        {
            if (Application.isEditor)
            {
                attack.main.ResetStats();

                attack.main.damage += damageMod;
                attack.main.speed += speedMod;
                attack.main.range += rangeMod;

                attack.SetAllStats();
            }
        }
    }
}
