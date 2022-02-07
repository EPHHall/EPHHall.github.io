using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Item;

namespace SS.Spells
{
    [ExecuteAlways]
    public class Spell_Attack : Spell
    {
        [Space(10)]
        [Header("Spell Attack Variables")]
        public List<Item.Weapon> activeWeapons;
        public Item.Weapon unarmedWeapon;
        public int weaponBeingUsed = -1;

        public override void Start()
        {
            base.Start();



            //The default, unarmed "weapon"
            if (!activeWeapons.Contains(unarmedWeapon))
            {
                activeWeapons.Insert(0, unarmedWeapon);
            }
            unarmedWeapon.unarmed = true;

            ApplyWeapon();
        }

        public override void CastSpell(bool overrideTileRequirement)
        {
            if (weaponBeingUsed != -1)
            {
                Debug.Log("0.5");
                ApplyWeapon();
            }

            base.CastSpell(overrideTileRequirement);
        }

        public override void ShowRange()
        {
            base.ShowRange();
        }

        public override void Update()
        {
            base.Update();

            if (!Application.isPlaying)
            {
                ApplyWeapon();
            }
        }

        public void ApplyWeapon()
        {
            if (weaponBeingUsed != -1)
            {
                activeWeapons[weaponBeingUsed].ApplyWeapon();
            }
            else
            {
                unarmedWeapon.ApplyWeapon();
            }
        }

        public void AddWeapon(Weapon newWeapon)
        {
            activeWeapons.Add(newWeapon);
        }

        public void ChangeWeapon(int index)
        {
            if (index < -1)
            {
                index = -1;
            }
            else if (index >= activeWeapons.Count)
            {
                index = activeWeapons.Count - 1;
            }

            weaponBeingUsed = index;

            ApplyWeapon();
        }
    }
}
