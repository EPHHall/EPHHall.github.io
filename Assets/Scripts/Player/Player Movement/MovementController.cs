using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.GameController;

namespace SS.CharacterMovement
{
    public class MovementController : TurnTaker
    {
        public GameObject movementTilePrefab;
        public GameObject wallTilePrefab;

        public bool CanMove { get; set; }

        private CharacterMovement _characterToMove;
        private SpawnMovementRange _spawnMovementRange;

        public override void Start()
        {
            _characterToMove = GetComponent<CharacterMovement>();
            if (_characterToMove == null) Debug.LogError("MovementController " + name + " does not have a CharacterMovement component", gameObject);

            _spawnMovementRange = new SpawnMovementRange(movementTilePrefab, wallTilePrefab);
        }

        public override void TurnTakerUpdate()
        {
            base.TurnTakerUpdate();
        }

        public override void TurnBeginning()
        {
            base.TurnBeginning();


            _spawnMovementRange.Spawn(transform.position, _characterToMove.speed);

            _characterToMove.CheckDirections();
            State = TurnTakerState.TurnBody;
        }

        public override void TurnEnd()
        {
            base.TurnEnd();
        }

        public override void TurnBody()
        {
            base.TurnBody();

            if (Input.GetKeyDown(KeyCode.W) && _characterToMove.Up)
            {
                _characterToMove.Move(Vector2.up);
                State = TurnTakerState.TurnEnd;
            }
            else if (Input.GetKeyDown(KeyCode.A) && _characterToMove.Left)
            {
                _characterToMove.Move(Vector2.left);
                State = TurnTakerState.TurnEnd;
            }
            else if (Input.GetKeyDown(KeyCode.S) && _characterToMove.Down)
            {
                _characterToMove.Move(Vector2.down);
                State = TurnTakerState.TurnEnd;
            }
            else if (Input.GetKeyDown(KeyCode.D) && _characterToMove.Right)
            {
                _characterToMove.Move(Vector2.right);
                State = TurnTakerState.TurnEnd;
            }
        }
    }
}
