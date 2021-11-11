using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.PlayerMovement
{
    public class SS_PlayerController : MonoBehaviour
    {
        public LayerMask movementMask;
        public Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (SS.GameController.TurnManager.currentTurnTaker == GetComponent<SS.GameController.TurnTaker>())
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    RaycastHit2D ray = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.up, movementMask);

                    if (ray.collider == null)
                    {
                        transform.Translate(Vector2.up);
                    }
                    else
                    {
                        //Debug.Log("Fuck", ray.collider.gameObject);
                    }
                }
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    RaycastHit2D ray = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.left, movementMask);
                    if (ray.collider == null)
                    {
                        transform.Translate(Vector2.left);
                    }
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    RaycastHit2D ray = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.down, movementMask);
                    if (ray.collider == null)
                    {
                        transform.Translate(Vector2.down);
                    }
                }
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    RaycastHit2D ray = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.right, movementMask);
                    if (ray.collider == null)
                    {
                        transform.Translate(Vector2.right);
                    }
                }
            }
        }
    }
}
