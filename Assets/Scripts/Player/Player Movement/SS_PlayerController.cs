using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.PlayerMovement
{
    public class SS_PlayerController : MonoBehaviour
    {
        private static SS_PlayerController mainController;

        public LayerMask movementMask;
        public Rigidbody2D rb;

        public bool pauseMovementForCutscene = false;
        public bool pauseMovementBecauseRangeWasShown = false;
        public bool pauseMovementForConfirmation = false;

        public float cooldownBeforeNextMove;
        public float firstCooldownBeforeNextMove;
        private float timer = -1;
        private float cooldownCounter;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            if (mainController == null && this.tag == "Player")
            {
                mainController = this;
            }
        }

        public void Initialize()
        {
            movementMask = mainController.movementMask;

            cooldownBeforeNextMove = mainController.cooldownBeforeNextMove;
            firstCooldownBeforeNextMove = mainController.firstCooldownBeforeNextMove;
            timer = mainController.timer;
            cooldownCounter = mainController.cooldownCounter;
        }

        private bool DetectIfMovementIsPossible(Vector2 origin, Vector2 destination)
        {
            RaycastHit2D[] rays = Physics2D.LinecastAll(origin, destination, movementMask);

            bool canMove = Util.CheckIfWayIsClear.Check(rays, true);

            return canMove;
        }

        void Update()
        {

            bool result = !pauseMovementForCutscene && !pauseMovementBecauseRangeWasShown && !pauseMovementForConfirmation;

            if (tag == "Player")
            {
                result = result && SS.GameController.TurnManager.instance.CurrentTurnTaker == GetComponent<SS.GameController.TurnTaker>();
            }
            else
            {
                result = result && SS.GameController.TurnManager.instance.CurrentTurnTaker == GetComponent<SS.GameController.TurnTakerControlledObject>();
            }

            if (result)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
                    Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    timer = -1;
                }


                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    bool canMove = DetectIfMovementIsPossible(transform.position, (Vector2)transform.position + Vector2.up);

                    if (timer == -1)
                    {
                        timer = 0;
                        cooldownCounter = 0;
                    }

                    if (canMove && timer >= cooldownCounter)
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
                    bool canMove = DetectIfMovementIsPossible(transform.position, (Vector2)transform.position + Vector2.left);

                    if (timer == -1)
                    {
                        timer = 0;
                        cooldownCounter = 0;
                    }

                    if (canMove && timer >= cooldownCounter)
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
                    bool canMove = DetectIfMovementIsPossible(transform.position, (Vector2)transform.position + Vector2.down);

                    if (timer == -1)
                    {
                        timer = 0;
                        cooldownCounter = 0;
                    }

                    if (canMove && timer >= cooldownCounter)
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
                    bool canMove = DetectIfMovementIsPossible(transform.position, (Vector2)transform.position + Vector2.right);

                    if (timer == -1)
                    {
                        timer = 0;
                        cooldownCounter = 0;
                    }

                    if (canMove && timer >= cooldownCounter)
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
        public void PauseMovement_Confirmation()
        {
            pauseMovementForConfirmation = true;
        }
        public void UnPauseMovement_Confirmation()
        {
            pauseMovementForConfirmation = false;
        }
    }
}
