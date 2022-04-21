using System.Collections;
using System.Collections.Generic;
using SS.Spells;
using UnityEngine;

namespace SS.AI
{
    public class Behavior_AnimateObjects : AIBehavior
    {
        public bool handleMovement;

        public int objectsMax;
        public static List<Target> objectsControlled = new List<Target>();
        public List<Target> objectsToControl;

        public Target currentTarget;

        public Pathfinding.Grid grid;

        public Behavior_AnimateObjects(Agent agent, List<Target> objectsToControl, bool handleMovement) : base(agent)
        {
            this.objectsToControl = objectsToControl;
            this.handleMovement = handleMovement;

            grid = GameObject.FindObjectOfType<Pathfinding.Grid>();
        }

        public override void InvokeBehavior(List<Spell> spells)
        {
            base.InvokeBehavior(spells);

            //Debug.Log("AnimateObjects.Invoke");

            agent.spellToCast = null;
            foreach (Spell spell in spells)
            {
                if (spell.main is ControlObject)
                {
                    agent.spellToCast = spell;
                    break;
                }
            }

            currentTarget = null;
            for (int i = 0; i < objectsToControl.Count; i++)
            {
                //skip if the previous object is still around and not yet controlled
                if (i != 0 && !objectsControlled.Contains(objectsToControl[i - 1]) && objectsToControl[i - 1] != null)
                {
                    continue;
                }

                //skip if teh object is already controlled or is null
                if (objectsControlled.Contains(objectsToControl[i]) || objectsToControl[i] == null)
                {
                    continue;
                }

                currentTarget = objectsToControl[i];
            }

            if (currentTarget == objectsToControl[1])
            {
                //Debug.Log(currentTarget, currentTarget.gameObject);
            }
            if (agent.spellToCast != null && currentTarget != null)
            {
                if (agent.characterStats.mana < agent.spellToCast.manaCost || agent.characterStats.actionPoints < agent.spellToCast.apCost)
                {
                    agent.FinishedOverride();
                    return;
                }

                if (handleMovement)
                {
                    Behavior_PathfindToTarget pathfind = new Behavior_PathfindToTarget(agent, GameObject.FindObjectOfType<Pathfinding.Grid>(), GameObject.FindObjectOfType<Pathfinding.AStar>(),
                        currentTarget.transform);
                    pathfind.InvokeBehavior(spells);
                    //Debug.Log("In Thing, path = " + grid.path);
                }
                else
                {
                    //Now find the target, if it's in range
                    Target target = null;
                    List<Vector2> positions = new List<Vector2>();
                    Vector2 seedPos = agent.transform.position;
                    positions.Add(seedPos);

                    positions = Util.SS_AStar.GetAllPositionsWithinRadius(positions, agent.spellToCast.range + 1);
                    List<Target> targets = Util.GetOnlyTargets.GetTargets(positions);
                    foreach (Target t in targets)
                    {
                        if (t == currentTarget)
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

            if (handleMovement)
            {
                result = grid.path != null;
            }
            else
            {
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
            }

            return result;
        }
    }
}
