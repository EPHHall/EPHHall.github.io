using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;
using SS.StatusSpace;
using SS.Character;

public class Effect_Projection : Effect
{
    public BehaviorPackage package;

    public override void Awake()
    {
        base.Awake();
    }

    public void BuildPackage_Projection(List<Target> t)
    {
        package = new BehaviorPackage();
        package.status = mainStatus;
        package.effect = this;
        package.targets = t;
    }

    //public override void BehaviorWhenDelivered_Enchanting(BehaviorPackage package)
    //{
    //    base.BehaviorWhenDelivered_Enchanting(package);

    //    Damage newDamage = new Damage(Damage.DamageType.Arcane, mainDamage.amount);

    //    foreach (Target target in package.targets)
    //    {
    //        SS.Item.Weapon.CreateWeapon(0, 0, range, null, newDamage, 
    //            true, target.GetComponent<CharacterStats>().meleeAttack, "Channel Bolt", null, null, 0, 0);
    //    }
    //}

    //public override void BehaviorWhenDelivered_Mutation(BehaviorPackage package)
    //{
    //    base.BehaviorWhenDelivered_Mutation(package);

    //    Damage newDamage = new Damage(Damage.DamageType.Arcane, mainDamage.amount);

    //    foreach (Target target in package.targets)
    //    {
    //        SS.Item.Weapon.CreateWeapon(0, 0, range, null, newDamage,
    //            true, target.GetComponent<CharacterStats>().meleeAttack, "Channel Bolt", null, null, 0, 0);
    //    }
    //}

    //public override void BehaviorWhenDelivered_Possession(BehaviorPackage package)
    //{
    //    base.BehaviorWhenDelivered_Possession(package);

    //    Damage newDamage = new Damage(Damage.DamageType.Arcane, mainDamage.amount);

    //    foreach (Target target in package.targets)
    //    {
    //        SS.Item.Weapon.CreateWeapon(0, 0, range, null, newDamage,
    //            true, target.GetComponent<CharacterStats>().meleeAttack, "Channel Bolt", null, null, 0, 0);
    //    }
    //}

    //public override void BehaviorWhenDelivered_Projection(BehaviorPackage package)
    //{
    //    base.BehaviorWhenDelivered_Projection(package);

    //    package.effect.AddToMainDamageList(mainDamage);
    //}

    //public override void BehaviorWhenDelivered_Summoning(BehaviorPackage package)
    //{
    //    base.BehaviorWhenDelivered_Summoning(package);

    //    Effect_Summoning.BehaviorPackageSummoning bps =
    //package as Effect_Summoning.BehaviorPackageSummoning;

    //    Damage newDamage = new Damage(Damage.DamageType.Arcane, mainDamage.amount);

    //    SS.Item.Weapon.CreateWeapon(0, 0, range, null, newDamage,
    //        true, bps.toSummon.GetComponent<CharacterStats>().meleeAttack, "Channel Bolt", null, null, 0, 0);
    //}




    public override void BehaviorWhenDelivered_Enchanting(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Enchanting(package);

        foreach (Target target in package.targets)
        {
            SS.Util.TargetInterface.DamageTarget(target, mainDamage, this);
        }
    }

    public override void BehaviorWhenDelivered_Mutation(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Mutation(package);

        foreach (Target target in package.targets)
        {
            SS.Util.TargetInterface.DamageTarget(target, mainDamage, this);
        }
    }

    public override void BehaviorWhenDelivered_Possession(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Possession(package);

        foreach (Target target in package.targets)
        {
            SS.Util.TargetInterface.DamageTarget(target, mainDamage, this);
        }
    }

    public override void BehaviorWhenDelivered_Projection(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Projection(package);

        foreach (Target target in package.targets)
        {
            SS.Util.TargetInterface.DamageTarget(target, mainDamage, this);
        }
    }

    public override void BehaviorWhenDelivered_Summoning(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Summoning(package);

        Effect_Summoning.BehaviorPackageSummoning bps =
            package as Effect_Summoning.BehaviorPackageSummoning;

        SS.Util.TargetInterface.DamageTarget(bps.toSummon, mainDamage, this);
    }

    public virtual void HandleDeliveredAndTargeting(List<Target> targets)
    {
        BuildPackage_Projection(targets);

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
