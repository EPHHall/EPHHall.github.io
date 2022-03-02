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
                ApplyWeapon();
            }
            else
            {
                weaponBeingUsed = 0;
                ApplyWeapon();
            }

            base.CastSpell(overrideTileRequirement);

            Weapon currentWeapon = activeWeapons[weaponBeingUsed];
            if(currentWeapon.spellToCast != null && currentWeapon.spellUses > 0)
            {
                currentWeapon.spellToCast.CastSpell(overrideTileRequirement, true, true);
                currentWeapon.spellUses--;
            }
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


        public void RemoveAndDestroyWeapons()
        {
            List<Weapon> toRemove = new List<Weapon>();
            foreach (Weapon weapon in activeWeapons)
            {
                if (weapon == unarmedWeapon) continue;

                toRemove.Add(weapon);
            }

            int breakInt = 0;
            while(toRemove.Count > 0)
            {
                Weapon temp = toRemove[0];
                toRemove.Remove(temp);
                activeWeapons.Remove(temp);

                Destroy(temp.gameObject);

                if (breakInt == 100)
                {
                    break;
                }

                breakInt++;
            }


            Debug.Log("Spell_Attack while, breakInt = " + breakInt);
        }
    }
}
