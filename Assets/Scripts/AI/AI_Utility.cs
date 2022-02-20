using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;
using SS.Util;

namespace SS.AI
{
    public class AI_Utility
    {
        //Sense environment
        public static List<Vector2> GetPotentialTargetPositions(Vector2 initialPosition, int range)
        {
            SS.Util.SS_AStar aStar = new SS_AStar();
            List<Vector2> visitedPositions = new List<Vector2>();
            List<Vector2> nextPositions = new List<Vector2>();
            nextPositions.Add(initialPosition);
            for (int i = 0; i < range; i++)
            {
                AddListToOriginal.AddList(visitedPositions, nextPositions);

                nextPositions = aStar.AStar(nextPositions, visitedPositions);
            }

            return visitedPositions;
        }
        public static List<Vector2> GetPotentialMovementPositions(Vector2 initialPosition, int range)
        {
            SS.Util.SS_AStar aStar = new SS_AStar();
            List<Vector2> visitedPositions = new List<Vector2>();
            List<Vector2> nextPositions = new List<Vector2>();
            nextPositions.Add(initialPosition);
            for (int i = 0; i < range; i++)
            {
                AddListToOriginal.AddList(visitedPositions, nextPositions);

                nextPositions = aStar.AStar_ForMovement(nextPositions, visitedPositions, false);
            }

            return visitedPositions;
        }

        public static List<Target> DetectTargetsWithinRange(Vector2 position, int range)
        {
            List<Vector2> positions = GetPotentialTargetPositions(position, range);

            return DetectTargetsWithinRange(positions);
        }
        public static List<Target> DetectTargetsWithinRange(List<Vector2> positions)
        {
            List<Target> targetsFound = new List<Target>();

            AddListToOriginal.AddList(targetsFound, CheckPositionsForTargets.Check(positions));

            return targetsFound;
        }

        // Reason/Analysis
        public static List<Target> FilterTargets(List<Target> targets, TargetType filter)
        {
            List<Target> filteredTargets = new List<Target>();

            foreach (Target target in targets)
            {
                if (target.targetType.DoesGTETOneTypeMatch(filter))
                {
                    filteredTargets.Add(target);
                }
            }

            return filteredTargets;
        }
        public static Target FindTarget(List<Target> targets, Target toFind)
        {
            if (toFind != null && targets.Contains(toFind))
            {
                return toFind;
            }
            else
            {
                return null;
            }
        }

        public static void GetCloserPositions(List<Vector2> positionsCloserToTarget, List<Vector2> positions, Vector2 origin, Vector2 target)
        {
            positionsCloserToTarget.Clear();
            foreach (Vector2 position in positions)
            {
                if (AI_Utility.IsPositionCloserToTarget(origin, position, target))
                {
                    positionsCloserToTarget.Add(position);
                }
            }
        }

        public static bool IsPositionCloserToTarget(Vector2 myPos, Vector2 potentialPos, Vector2 targetPos)
        {
            bool result = false;

            result = Vector2.Distance(myPos, targetPos) > Vector2.Distance(potentialPos, targetPos);

            //Debug.Log(result);

            return result;
        }

        public static Vector2 FindClosestPosition(List<Vector2> positions, Vector2 targetPos)
        {
            Vector2 closest = Vector2.positiveInfinity;

            foreach (Vector2 position in positions)
            {
                if (Vector2.Distance(position, targetPos) < Vector2.Distance(closest, targetPos))
                {
                    closest = position;
                }
            }

            return closest;
        }
    }
}
