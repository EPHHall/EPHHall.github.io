using System.Collections;
using System.Collections.Generic;
using SS.Spells;
using UnityEngine;

namespace SS.AI
{
    public class Behavior_AnimateObjects : AIBehavior
    {
        public int objectsMax;
        public List<Target> objectsControlled = new List<Target>();
        public List<Target> objectsToControl;

        public Behavior_AnimateObjects(Agent agent, List<Target> objectsToControl) : base(agent)
        {
            this.objectsToControl = objectsToControl;
        }

        public override void InvokeBehavior(List<Spell> spells)
        {
            base.InvokeBehavior(spells);

            agent.spellToCast = null;
            foreach (Spell spell in spells)
            {
                if (spell.main is ControlObject)
                {
                    agent.spellToCast = spell;
                    break;
                }
            }

            if (agent.spellToCast != null)
            {
                if (agent.characterStats.mana < agent.spellToCast.manaCost || agent.characterStats.actionPoints < agent.spellToCast.apCost)
                {
                    return;
                }

                //go through, in order, objectsToControl, move to those objects, and cast ControlObject on them.
                for (int i = 0; i < objectsToControl.Count; i++)
                {
                    //End if the agent doesn't have enough mana/action points
                    if (agent.characterStats.mana < agent.spellToCast.manaCost || agent.characterStats.actionPoints < agent.spellToCast.apCost)
                    {
                    //Debug.Log("Reahced the for 1");
                        break;
                    }

                    //skip if the previous object is still around and not yet controlled
                    if (i != 0 && !objectsControlled.Contains(objectsToControl[i - 1]) && objectsToControl[i - 1] != null)
                    {
                    //Debug.Log("Reahced the for 2");
                        continue;
                    }

                    //skip if teh object is already controlled or is null
                    if(objectsControlled.Contains(objectsToControl[i]) || objectsToControl[i] == null)
                    {
                    //Debug.Log("Reahced the for 3");
                        continue;
                    }

                    Behavior_PathfindToTarget pathfind = new Behavior_PathfindToTarget(agent, GameObject.FindObjectOfType<Pathfinding.Grid>(), GameObject.FindObjectOfType<Pathfinding.AStar>(), 
                        objectsToControl[i].transform);
                    pathfind.InvokeBehavior(spells);

                    //Now find the target, if it's in range
                    Target target = null;
                    List<Vector2> positions = new List<Vector2>();
                    Vector2 seedPos = agent.transform.position;
                    positions.Add(seedPos);

                    positions = Util.SS_AStar.GetAllPositionsWithinRadius(positions, agent.spellToCast.range + 1);
                    List<Target> targets = Util.GetOnlyTargets.GetTargets(positions);
                    foreach (Target t in targets)
                    {
                        if (t == objectsToControl[i])
                        {
                            target = t;
                            break;
                        }
                    }

                    if (target != null)
                    {
                        Target.ClearSelectedTargets();
                        Target.selectedTargets.Add(target);
                        agent.spellToCast.CastSpell(true);

                        objectsControlled.Add(target);
                    }
                }
            }
        }

        public override bool WasBehaviorFulfilled()
        {
            bool result = false;

            int counter = 0;
            foreach (Target target in objectsToControl)
            {
                if (target == null || objectsControlled.Contains(target))
                {
                    counter++;
                }
            }

            if (counter >= objectsToControl.Count)
            {
                result = true;
            }

            return result;
        }
    }
}
