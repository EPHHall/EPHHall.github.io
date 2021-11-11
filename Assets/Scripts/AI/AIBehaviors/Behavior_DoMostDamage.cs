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
            agent.spellToCast = null;

            base.InvokeBehavior(spells);

            foreach (Spell spell in spells)
            {
                agent.currentTarget = null;

                //Find all target positions
                agent.targets = AI_Utility.DetectTargetsWithinRange(agent.transform.position, spell.range);

                //Find main target in those positions
                agent.currentTarget = AI_Utility.FindTarget(agent.targets, agent.mainTarget);

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
