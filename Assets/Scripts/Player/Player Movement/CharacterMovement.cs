using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.CharacterMovement
{
    public class CharacterMovement : MonoBehaviour
    {
        public int speed;
        public LayerMask whatIsWall;

        public bool Up { get; private set; }
        public bool Down { get; private set; }
        public bool Left { get; private set; }
        public bool Right { get; private set; }

        private int _movementUsed;

        [Space(10)]
        [Header("Debug")]
        public bool D_Up;
        public bool D_Down;
        public bool D_Left;
        public bool D_Right;

        public void ResetMovement()
        {
            _movementUsed = 0;
        }

        private void Update()
        {
            HandleDebug();
        }

        public void HandleDebug()
        {
            D_Up = Up;
            D_Down = Down;
            D_Left = Left;
            D_Right = Right;
        }

        /// <summary>
        /// Returns true if the move was successfully completed and false if not, ie) if the movement used is at or exceeds the speed.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool Move(Vector2 direction)
        {
            if (_movementUsed >= speed) return false;

            transform.Translate(direction);
            _movementUsed++;

            return true;
        }
        /// <summary>
        /// Returns true, and does not increase the movement counter. Use this when it isn't important to keep track of how much the character has 
        /// moved ie) player movement (confined by wall tiles, movement used isn't really a factor)
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="overrideMovementUsed"></param>
        /// <returns></returns>
        public bool Move(Vector2 direction, bool overrideMovementUsed)
        {
            transform.Translate(direction);

            return true;
        }

        public void CheckDirections()
        {
            Up = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.up, whatIsWall).collider == null;
            Down = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.down, whatIsWall).collider == null;
            Left = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.left, whatIsWall).collider == null;
            Right = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.right, whatIsWall).collider == null;
        }
    }
}
