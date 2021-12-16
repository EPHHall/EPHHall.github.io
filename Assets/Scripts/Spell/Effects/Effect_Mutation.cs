using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;

public class Effect_Mutation : Effect
{
    public BehaviorPackage package;

    public override void Awake()
    {
        base.Awake();
    }

    public void BuildPackage_Mutation(List<Target> t)
    {
        package = new BehaviorPackage();
        package.status = mainStatus;
        package.effect = this;
        package.targets = t;
    }

    public override void BehaviorWhenDelivered_Enchanting(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Enchanting(package);
    }

    public override void BehaviorWhenDelivered_Mutation(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Mutation(package);
    }

    public override void BehaviorWhenDelivered_Possession(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Possession(package);
    }

    public override void BehaviorWhenDelivered_Projection(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Projection(package);
    }

    public override void BehaviorWhenDelivered_Summoning(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Summoning(package);
    }




    public override void BehaviorWhenTargeting_Enchanting(BehaviorPackage package)
    {
        base.BehaviorWhenTargeting_Enchanting(package);
    }

    public override void BehaviorWhenTargeting_Mutation(BehaviorPackage package)
    {
        base.BehaviorWhenTargeting_Mutation(package);
    }

    public override void BehaviorWhenTargeting_Possession(BehaviorPackage package)
    {
        base.BehaviorWhenTargeting_Possession(package);
    }

    public override void BehaviorWhenTargeting_Projection(BehaviorPackage package)
    {
        base.BehaviorWhenTargeting_Projection(package);
    }

    public override void BehaviorWhenTargeting_Summoning(BehaviorPackage package)
    {
        base.BehaviorWhenTargeting_Summoning(package);
    }

    public virtual void HandleDeliveredAndTargeting(List<Target> targets)
    {
        BuildPackage_Mutation(targets);

        foreach (Effect e in targetMeEffects)
        {
            if (e == null) continue;

            e.BehaviorWhenTargeting(package);
        }
        foreach (Effect e in deliveredEffects)
        {
            if (e == null) continue;

            e.BehaviorWhenDelivered(package);
        }
    }
}
