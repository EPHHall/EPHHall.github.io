using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.GameController
{
    public class TurnTakerControlledObject : TurnTaker
    {
        PlayerMovement.SS_PlayerController controller;
        PlayerMovement.SS_PlayerMoveRange moveRange;
        AI.Agent agent;

        public bool enemyVersion;

        public void Initialize(bool appliedByEnemy)
        {
            enemyVersion = appliedByEnemy;

            if (enemyVersion)
            {
                agent = GetComponent<AI.Agent>();

                agent.enabled = true;

                if (agent.mainTarget == null)
                {
                    agent.mainTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Spells.Target>();
                }

                agent.Awake();
            }
            else
            {
                controller = GetComponent<SS.PlayerMovement.SS_PlayerController>();
                moveRange = GetComponent<SS.PlayerMovement.SS_PlayerMoveRange>();

                if (moveRange == null)
                {
                    moveRange = gameObject.AddComponent<SS.PlayerMovement.SS_PlayerMoveRange>();
                }

                if (controller == null)
                {
                    controller = gameObject.AddComponent<SS.PlayerMovement.SS_PlayerController>();
                }

                controller.Initialize();

                moveRange.Initialize();
            }
        }

        public override void EndTurn()
        {
            base.EndTurn();
        }

        public override void StartTurn()
        {
            base.StartTurn();

            if (!enemyVersion)
            {
                moveRange.ResetMoveRange(transform.position);
            }
            else
            {
                agent.StartTurn();
            }
        }

        public int EndControl()
        {
            int previousIndex = -1;

            if (TurnManager.tm.turnTakers.Contains(this))
            {
                previousIndex = TurnManager.tm.turnTakers.IndexOf(this);
                TurnManager.tm.turnTakers.Remove(this);
            }

            if (enemyVersion)
            {
                agent.enabled = false;
            }

            return previousIndex;
            //Destroy(this);
        }
    }
}
