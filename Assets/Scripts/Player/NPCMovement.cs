using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Character
{
    public class NPCMovement : MonoBehaviour
    {
        public float interval = 0.145f;

        private SS.AI.Behavior_WaitForMovementAnim behaviorToSet;

        public void StartMovement(List<Pathfinding.Node> path, AI.Behavior_WaitForMovementAnim behavior, int speed)
        {
            behaviorToSet = behavior;

            StartCoroutine(Move(path, speed));
        }

        IEnumerator Move(List<Pathfinding.Node> path, int speed)
        {
            if (path == null)
            {
                behaviorToSet.FinishMovement();

                yield break;
            }

            int limit = speed;
            if (limit > path.Count)
            {
                limit = path.Count;
            }

            for (int i = 0; i < limit; i++)
            {
                transform.position = path[i].position;

                yield return new WaitForSeconds(interval);
            }

            behaviorToSet.FinishMovement();
        }
    }
}
