using System.Collections;
using System.Collections.Generic;
using SS.Spells;
using UnityEngine;

namespace SS.AI
{
    public class Behavior_DoMostDamage : AIBehavior
    {
        public override void InvokeBehavior(List<Spell> spells)
        {
            Debug.Log("In Do most damage");

            agent.spellToCast = null;

            base.InvokeBehavior(spells);

            foreach (Spell spell in spells)
            {
                agent.currentTarget = null;

                //Find all target positions
                agent.targets = AI_Utility.DetectTargetsWithinRange(agent.transform.position, spell.range);

                //Find main target in those positions
                agent.currentTarget = AI_Utility.FindTarget(agent.targets, agent.mainTarget);

                //If the main target was not in range or is just null, find a target of the right faction
                if (agent.currentTarget == null)
                {

                    foreach (Target target in agent.targets)
                    {
                        if (target != agent.GetComponent<Target>() && target.GetComponent<Faction>() != null && target.GetComponent<Faction>().factionName == agent.targetFaction)
                        {
                            agent.currentTarget = target;
                            break;
                        }
                    }
                }

                //If main target is not in positions, move on
                //Else, check how much damage the spell would do and keep track of teh highest damaging spell
                if (agent.currentTarget == null)
                {
                    continue;
                }
                else
                {
                    if (agent.spellToCast == null || agent.spellToCast.damage < spell.damage)
                    {
                        agent.spellToCast = spell;
                    }
                }
            }

            if (agent.spellToCast != null)
            {
                Target.ClearSelectedTargets();
                Target.selectedTargets.Add(agent.currentTarget);

                agent.spellToCast.CastSpell(true);
            }
        }

        public override bool WasBehaviorFulfilled()
        {
            if (agent.spellToCast != null)
            {
                if (agent.characterStats.actionPoints < agent.spellToCast.apCost)
                    return true;
                if (agent.characterStats.mana < agent.spellToCast.manaCost)
                    return true;

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
