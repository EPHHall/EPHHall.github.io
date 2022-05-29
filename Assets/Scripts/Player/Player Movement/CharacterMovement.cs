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

        public void ResetMovement()
        {
            _movementUsed = 0;
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

        public void CheckDirections()
        {
            Up = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.up, whatIsWall).collider == null;
            Down = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.down, whatIsWall).collider == null;
            Left = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.left, whatIsWall).collider == null;
            Right = Physics2D.Linecast(transform.position, (Vector2)transform.position + Vector2.right, whatIsWall).collider == null;
        }
    }
}
