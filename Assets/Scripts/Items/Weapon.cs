using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;
using SS.StatusSpace;
using SS.Character;

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

        public List<Status> statusesToInflict;

        public Damage toInflict;

        public void Awake()
        {
            if (!Application.isPlaying)
            {
                ApplyWeapon();
            }
        }

        private void Start()
        {
            ApplyWeapon();
        }

        public void ApplyWeapon()
        {
            attack.main.ResetStats();

            if (attack.activeWeapons.Contains(this))
            {

                Debug.Log("Buh");

                if (!attack.main.originalDamageList.Contains(toInflict))
                {
                    attack.main.originalDamageList.Add(toInflict);
                }

                attack.main.ResetMainDamageList();

                attack.main.speed += speedMod;
                attack.main.range += rangeMod;

                toInflict.statusesToInflict = new List<Status>(statusesToInflict);
            }
            else
            {
                attack.main.originalDamageList.Remove(toInflict);
                attack.main.ResetMainDamageList();
            }

            attack.SetAllStats();
        }

        public void UpdateToInflict()
        {
            toInflict.statusesToInflict = new List<Status>(statusesToInflict);
        }

        public void AddStatusToInflict(Status status)
        {
            statusesToInflict.Add(status);
            status.applyingEffect = attack.main;

            ApplyWeapon();
        }

        public void Update()
        {
            if (!Application.isPlaying)
            {
                ApplyWeapon();
            }
        }
    }
}
