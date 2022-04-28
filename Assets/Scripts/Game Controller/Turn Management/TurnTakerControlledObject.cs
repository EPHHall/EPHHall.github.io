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

        private bool wasObject;

        public void Initialize(bool appliedByEnemy)
        {
            enemyVersion = appliedByEnemy;

            if (GetComponent<Spells.Target>() != null)
            {
                wasObject = GetComponent<Spells.Target>().targetType.obj;

                GetComponent<Spells.Target>().targetType.creature = true;
                GetComponent<Spells.Target>().targetType.obj = false;
            }

            if (enemyVersion)
            {
                agent = GetComponent<AI.Agent>();

                agent.enabled = true;

                if (agent.mainTarget == null)
                {
                    agent.mainTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Spells.Target>();
                }

                agent.Start();
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
                moveRange.ResetMoveRange(transform.position, "TurnTakerControlledObject, StartTurn");
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

            if (GetComponent<Spells.Target>() != null)
            {
                if (wasObject)
                {
                    GetComponent<Spells.Target>().targetType.creature = false;
                    GetComponent<Spells.Target>().targetType.obj = false;
                }
            }

            return previousIndex;
            //Destroy(this);
        }
    }
}
