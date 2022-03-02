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
        [System.Serializable]
        public class WeaponTemplate
        {
            public int damageMod;
            public int speedMod;
            public int rangeMod;

            public Spell_Attack attack;

            public List<Status> statusesToInflict;

            public Damage toInflict;

            public bool unarmed;

            public string weaponName;

            public GameObject spellObject;
            public Spell spellToCast;
            public int spellUses;
            public int spellUsesMax;
        }

        //stats
        public int damageMod;
        public int speedMod;
        public int rangeMod;

        public Spell_Attack attack;

        public List<Status> statusesToInflict;

        public Damage toInflict;
        public GameObject spellObject;
        public Spell spellToCast;
        public int spellUses;
        public int spellUsesMax;

        public bool unarmed;

        public string weaponName;

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

        public static Weapon CreateWeapon(WeaponTemplate weaponTemplate)
        {
            Weapon newWeapon = CreateWeapon(weaponTemplate.damageMod, weaponTemplate.speedMod, weaponTemplate.rangeMod, weaponTemplate.statusesToInflict, weaponTemplate.toInflict, weaponTemplate.unarmed, weaponTemplate.attack, 
                weaponTemplate.weaponName, weaponTemplate.spellObject, weaponTemplate.spellToCast, weaponTemplate.spellUses, weaponTemplate.spellUsesMax);

            return newWeapon;
        }
        public static Weapon CreateWeapon(Weapon toCreate, Spell_Attack parent, string name)
        {
            Weapon newWeapon = CreateWeapon(toCreate.damageMod, toCreate.speedMod, toCreate.rangeMod, toCreate.statusesToInflict, toCreate.toInflict, toCreate.unarmed, parent, name, 
                toCreate.spellObject, toCreate.spellToCast, toCreate.spellUses, toCreate.spellUsesMax);

            return newWeapon;
        }
        public static Weapon CreateWeapon(int damageMod, int speedMod, int rangeMod, 
            List<Status> statusesToInflict, Damage toInflict, bool unarmed, Spell_Attack parent,
            string name, GameObject spellObject, Spell spellToCast, int spellUses, int maxUses)
        {
            GameObject instantiated = new GameObject(name);
            instantiated.transform.parent = parent.transform;
            instantiated.transform.localPosition = Vector2.zero;
            instantiated.transform.rotation = Quaternion.identity;
            
            Weapon newWeapon = instantiated.AddComponent<Weapon>();
            newWeapon.damageMod = damageMod;
            newWeapon.speedMod = speedMod;
            newWeapon.rangeMod = rangeMod;

            newWeapon.statusesToInflict = new List<Status>();
            if (statusesToInflict != null)
            {
                newWeapon.statusesToInflict = new List<Status>(statusesToInflict);
            }

            newWeapon.toInflict = toInflict;
            toInflict.weapon = newWeapon;

            newWeapon.unarmed = unarmed;

            newWeapon.attack = parent;

            newWeapon.weaponName = name;

            parent.AddWeapon(newWeapon);

            newWeapon.gameObject.AddComponent<CircleCollider2D>();
            newWeapon.GetComponent<CircleCollider2D>().radius = .4f;
            newWeapon.gameObject.AddComponent<Target>();
            newWeapon.GetComponent<Target>().targetType = new TargetType(false, false, true, false);
            newWeapon.GetComponent<Target>().targetName = name;

            if (spellObject != null)
            {
                GameObject so = Instantiate(spellObject, newWeapon.transform);
                newWeapon.spellToCast = so.GetComponent<Spell>();

                so.transform.localPosition = Vector2.zero;
            }
            newWeapon.spellUses = spellUses;
            newWeapon.spellUsesMax = maxUses;
    
            return newWeapon;
        }

        public void ApplyWeapon()
        {
            if (attack == null) return;
            if (attack.main == null) return;

            if (name == "Channel Bolt")
            {
                Debug.Log("past beginnning");
                Debug.Log(rangeMod);
            }

            attack.main.ResetStats();

            if (attack.activeWeapons.Contains(this) &&
                (attack.weaponBeingUsed == attack.activeWeapons.IndexOf(this) || 
                (attack.weaponBeingUsed == -1 && attack.activeWeapons.IndexOf(this) == 0)))
            {
                foreach (Damage damage in attack.main.originalDamageList)
                {
                    if (damage.weapon != null)
                    {
                        attack.main.RemoveFromOriginalDamageList(damage);
                    }
                }
                
                attack.main.AddToOriginalDamageList(toInflict, this);

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
            Status newStatus = new Status(status);

            statusesToInflict.Add(newStatus);
            newStatus.applyingEffect = attack.main;

            ApplyWeapon();
        }

        public void Update()
        {
            if (!Application.isPlaying)
            {
                //ApplyWeapon();
            }
        }
    }
}
