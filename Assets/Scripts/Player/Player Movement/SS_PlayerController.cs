using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.PlayerMovement
{
    public class SS_PlayerController : MonoBehaviour
    {
        public LayerMask movementMask;
        public Rigidbody2D rb;

        public bool pauseMovementForCutscene = false;
        public bool pauseMovementBecauseRangeWasShown = false;

        public float cooldownBeforeNextMove;
        public float firstCooldownBeforeNextMove;
        private float timer = -1;
        private float cooldownCounter;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (!pauseMovementForCutscene && !pauseMovementBecauseRangeWasShown && SS.GameController.TurnManager.currentTurnTaker == GetComponent<SS.GameController.TurnTaker>())
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
                    Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    timer = -1;
                }

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    RaycastHit2D ray = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.up, movementMask);

                    if (timer == -1)
                    {
                        timer = 0;
                        cooldownCounter = 0;
                    }

                    if (ray.collider == null && timer >= cooldownCounter)
                    {
                        transform.Translate(Vector2.up);

                        if (cooldownCounter == 0)
                        {
                            cooldownCounter += firstCooldownBeforeNextMove;
                        }
                        else
                        {
                            cooldownCounter += cooldownBeforeNextMove;
                        }
                    }
                }
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    RaycastHit2D ray = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.left, movementMask);

                    if (timer == -1)
                    {
                        timer = 0;
                        cooldownCounter = 0;
                    }

                    if (ray.collider == null && timer >= cooldownCounter)
                    {
                        transform.Translate(Vector2.left);

                        if (cooldownCounter == 0)
                        {
                            cooldownCounter += firstCooldownBeforeNextMove;
                        }
                        else
                        {
                            cooldownCounter += cooldownBeforeNextMove;
                        }
                    }
                }
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    RaycastHit2D ray = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.down, movementMask);

                    if (timer == -1)
                    {
                        timer = 0;
                        cooldownCounter = 0;
                    }

                    if (ray.collider == null && timer >= cooldownCounter)
                    {
                        transform.Translate(Vector2.down);

                        if (cooldownCounter == 0)
                        {
                            cooldownCounter += firstCooldownBeforeNextMove;
                        }
                        else
                        {
                            cooldownCounter += cooldownBeforeNextMove;
                        }
                    }
                }
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    RaycastHit2D ray = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.right, movementMask);

                    if (timer == -1)
                    {
                        timer = 0;
                        cooldownCounter = 0;
                    }

                    if (ray.collider == null && timer >= cooldownCounter)
                    {
                        transform.Translate(Vector2.right);

                        if (cooldownCounter == 0)
                        {
                            cooldownCounter += firstCooldownBeforeNextMove;
                        }
                        else
                        {
                            cooldownCounter += cooldownBeforeNextMove;
                        }
                    }
                }

                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) ||
                    Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    timer = -1;
                }

                if (timer != -1)
                {
                    timer += Time.deltaTime;
                }
            }

        }//Update

        public void PauseMovement_RangeShown()
        {
            pauseMovementBecauseRangeWasShown = true;
        }
        public void UnPauseMovement_RangeShown()
        {
            pauseMovementBecauseRangeWasShown = false;
        }
        public void PauseMovement_ForCutscene()
        {
            pauseMovementForCutscene = true;
        }
        public void UnPauseMovement_ForCutscene()
        {
            pauseMovementForCutscene = false;
        }
    }
}
