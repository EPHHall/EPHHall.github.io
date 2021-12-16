using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;
using SS.StatusSpace;
using SS.Character;

public class Effect_Summoning : Effect
{
    public class BehaviorPackageSummoning : BehaviorPackage
    {
        public Target toSummon;
    }

    public BehaviorPackageSummoning package;
    public Target toSummon;
    public Vector2 overridePosition;
    public bool useOverridePosition;

    public override void Awake()
    {
        base.Awake();
    }

    public void BuildPackage_Summoning(List<Target> t, Target toSummon)
    {
        package = new BehaviorPackageSummoning();
        package.effect = this;
        package.targets = t;

        package.toSummon = toSummon;
    }

    //TODO: Maybe these behaviors could add a weapon that deals no damage butsummons a creature.
    public override void BehaviorWhenDelivered_Enchanting(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Enchanting(package);

        for (int i = 0; i < package.targets.Count; i++)
        {
            Target target = package.targets[i];

            List<Target> singleTarget = new List<Target>();
            singleTarget.Add(target);

            InvokeEffect(singleTarget);
        }
    }

    public override void BehaviorWhenDelivered_Mutation(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Mutation(package);

        for (int i = 0; i < package.targets.Count; i++)
        {
            Target target = package.targets[i];

            List<Target> singleTarget = new List<Target>();
            singleTarget.Add(target);

            InvokeEffect(singleTarget);
        }
    }

    public override void BehaviorWhenDelivered_Possession(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Possession(package);

        for (int i = 0; i < package.targets.Count; i++)
        {
            Target target = package.targets[i];

            List<Target> singleTarget = new List<Target>();
            singleTarget.Add(target);

            InvokeEffect(singleTarget);
        }
    }

    public override void BehaviorWhenDelivered_Projection(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Projection(package);

        for (int i = 0; i < package.targets.Count; i++)
        {
            Target target = package.targets[i];

            List<Target> singleTarget = new List<Target>();
            singleTarget.Add(target);

            InvokeEffect(singleTarget);
        }
    }

    public override void BehaviorWhenDelivered_Summoning(BehaviorPackage package)
    {
        base.BehaviorWhenDelivered_Summoning(package);

        Effect_Summoning.BehaviorPackageSummoning bps =
    package as Effect_Summoning.BehaviorPackageSummoning;

        List<Target> singleTarget = new List<Target>();
        singleTarget.Add(bps.toSummon);

        if ((bps.effect as Teleport) != null)
        {
            useOverridePosition = true;
            overridePosition = bps.toSummon.transform.position;
        }
        InvokeEffect(singleTarget);

        useOverridePosition = false;
    }




    public override void BehaviorWhenTargeting_Enchanting(BehaviorPackage package)
    {
        base.BehaviorWhenTargeting_Enchanting(package);

        for(int i = 0; i < package.targets.Count; i++)
        {
            Target target = package.targets[i];

            List<Target> singleTarget = new List<Target>();
            singleTarget.Add(target);

            InvokeEffect(singleTarget);
        }
    }

    public override void BehaviorWhenTargeting_Mutation(BehaviorPackage package)
    {
        base.BehaviorWhenTargeting_Mutation(package);

        for (int i = 0; i < package.targets.Count; i++)
        {
            Target target = package.targets[i];

            List<Target> singleTarget = new List<Target>();
            singleTarget.Add(target);

            InvokeEffect(singleTarget);
        }
    }

    public override void BehaviorWhenTargeting_Possession(BehaviorPackage package)
    {
        base.BehaviorWhenTargeting_Possession(package);

        for (int i = 0; i < package.targets.Count; i++)
        {
            Target target = package.targets[i];

            List<Target> singleTarget = new List<Target>();
            singleTarget.Add(target);

            InvokeEffect(singleTarget);
        }
    }

    public override void BehaviorWhenTargeting_Projection(BehaviorPackage package)
    {
        base.BehaviorWhenTargeting_Projection(package);

        //TODO Come back and make the projectile phase through walls. First, walls have to 
        //block of course

        for (int i = 0; i < package.targets.Count; i++)
        {
            Target target = package.targets[i];

            List<Target> singleTarget = new List<Target>();
            singleTarget.Add(target);

            InvokeEffect(singleTarget);
        }
    }

    public override void BehaviorWhenTargeting_Summoning(BehaviorPackage package)
    {
        base.BehaviorWhenTargeting_Summoning(package);

        Effect_Summoning.BehaviorPackageSummoning bps =
            package as Effect_Summoning.BehaviorPackageSummoning;

        List<Target> singleTarget = new List<Target>();
        singleTarget.Add(bps.toSummon);

        if ((bps.effect as Teleport) != null)
        {
            useOverridePosition = true;
            overridePosition = bps.toSummon.transform.position;
        }
        InvokeEffect(singleTarget);

        useOverridePosition = false;
    }

    public virtual void HandleDeliveredAndTargeting(List<Target> targets, GameObject summoned)
    {
        BuildPackage_Summoning(targets, summoned.GetComponent<Target>());
        Debug.Log("In Summoning Handler", gameObject);

        foreach (Effect e in targetMeEffects)
        {
            if (e == null) continue;

            Debug.Log("In Summoning target me", gameObject);
            e.BehaviorWhenTargeting(package);
        }
        foreach (Effect e in deliveredEffects)
        {
            if (e == null) continue;

            e.BehaviorWhenDelivered(package);
        }
    }
}
