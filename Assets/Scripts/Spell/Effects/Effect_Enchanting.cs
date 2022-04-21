using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;
using SS.StatusSpace;
using SS.Character;

namespace SS.Spells
{
    public class Effect_Enchanting : Effect
    {
        public Status enchantmentStatus;
        public Target enchantmentTarget;

        public BehaviorPackage package;

        public override void Awake()
        {
            base.Awake();

            enchantmentStatus = mainStatus;
        }

        public void BuildPackage_Enchanting(List<Target> t)
        {
            package = new BehaviorPackage();
            package.status = mainStatus;
            package.effect = this;
            package.targets = t;
        }

        //public override void BehaviorWhenDelivered_Enchanting(BehaviorPackage package)
        //{
        //    base.BehaviorWhenDelivered_Enchanting(package);

        //    Debug.Log("Buh");

        //    foreach (Target target in package.targets)
        //    {
        //        CharacterStats stats;
        //        if (target.TryGetComponent<CharacterStats>(out stats) && stats.meleeAttack != null)
        //        {
        //            //the first wepaon is always the melee attack
        //            stats.meleeAttack.activeWeapons[0].AddStatusToInflict(mainStatus);
        //        }
        //    }
        //}

        //public override void BehaviorWhenDelivered_Mutation(BehaviorPackage package)
        //{
        //    base.BehaviorWhenDelivered_Mutation(package);

        //    foreach (Target target in package.targets)
        //    {
        //        //for every mutated creature, make that creature inflict the status effect with its attacks.
        //        CharacterStats stats;
        //        if (target.TryGetComponent<CharacterStats>(out stats) && stats.meleeAttack != null)
        //        {
        //            //the first wepaon is always the melee attack
        //            stats.meleeAttack.activeWeapons[0].AddStatusToInflict(mainStatus);
        //        }
        //    }
        //}

        //public override void BehaviorWhenDelivered_Possession(BehaviorPackage package)
        //{
        //    base.BehaviorWhenDelivered_Possession(package);

        //    foreach (Target target in package.targets)
        //    {
        //        //for every possessed creature, make that creature inflict the status effect with its attacks.
        //        CharacterStats stats;
        //        if (target.TryGetComponent<CharacterStats>(out stats) && stats.meleeAttack != null)
        //        {
        //            //the first weapon is always the melee attack
        //            stats.meleeAttack.activeWeapons[0].AddStatusToInflict(mainStatus);
        //        }
        //    }
        //}

        //public override void BehaviorWhenDelivered_Projection(BehaviorPackage package)
        //{
        //    base.BehaviorWhenDelivered_Projection(package);

        //    Debug.Log("In Delovered");

        //    foreach (Target target in package.targets)
        //    {
        //        target.ApplyStatus(mainStatus, this);
        //    }
        //}

        //public override void BehaviorWhenDelivered_Summoning(BehaviorPackage package)
        //{
        //    base.BehaviorWhenDelivered_Summoning(package);

        //    Effect_Summoning.BehaviorPackageSummoning bps =
        //package as Effect_Summoning.BehaviorPackageSummoning;

        //    //for every summoned creature, make that creature inflict the status effect with its attacks.
        //    CharacterStats stats;
        //    if (bps.toSummon.TryGetComponent<CharacterStats>(out stats) && stats.meleeAttack != null)
        //    {
        //        SS.Item.Weapon weapon = stats.meleeAttack.unarmedWeapon;

        //        //the first wepaon is always the melee attack
        //        weapon.AddStatusToInflict(mainStatus);
        //    }
        //}





        public override void BehaviorWhenDelivered_Enchanting(BehaviorPackage package)
        {
            base.BehaviorWhenDelivered_Enchanting(package);

            Debug.Log("Buh Buh");

            package.effect.AddToMainStatusList(mainStatus);
        }

        public override void BehaviorWhenDelivered_Mutation(BehaviorPackage package)
        {
            base.BehaviorWhenDelivered_Mutation(package);

            foreach (Target target in package.targets)
            {
                target.ApplyStatus(mainStatus, this);
            }
        }

        public override void BehaviorWhenDelivered_Possession(BehaviorPackage package)
        {
            base.BehaviorWhenDelivered_Possession(package);

            foreach (Target target in package.targets)
            {
                target.ApplyStatus(mainStatus, this);
            }
        }

        public override void BehaviorWhenDelivered_Projection(BehaviorPackage package)
        {
            base.BehaviorWhenDelivered_Projection(package);

            Debug.Log("In Delivered");

            //package.effect.AddToMainDamageList(new SS.Character.Damage(mainStatus.GetDamageType(), mainStatus.magnitude));

            foreach (Target target in package.targets)
            {
                target.ApplyStatus(mainStatus, this);
            }
        }

        public override void BehaviorWhenDelivered_Summoning(BehaviorPackage package)
        {
            base.BehaviorWhenDelivered_Summoning(package);

            Effect_Summoning.BehaviorPackageSummoning bps = 
                package as Effect_Summoning.BehaviorPackageSummoning;

            bps.toSummon.ApplyStatus(mainStatus, this);
        }

        public virtual void HandleDeliveredAndTargeting(List<Target> targets)
        {
            BuildPackage_Enchanting(targets);

            foreach (Effect e in targetMeEffects)
            {
                if (e == null) continue;

                e.BehaviorWhenDelivered(package);
            }
            foreach (Effect e in deliveredEffects)
            {
                if (e == null) continue;

                e.BehaviorWhenDelivered(package);
            }
        }
    }
}
