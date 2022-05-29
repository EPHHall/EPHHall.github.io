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

        [Space(10)]
        [Header("Debug")]
        public bool D_CanMove;

        public override void Start()
        {
            base.Start();

            _characterToMove = GetComponent<CharacterMovement>();
            if (_characterToMove == null) Debug.LogError("MovementController " + name + " does not have a CharacterMovement component", gameObject);

            _spawnMovementRange = new SpawnMovementRange(movementTilePrefab, wallTilePrefab);
        }

        public override void HandleDebug()
        {
            base.HandleDebug();
            D_CanMove = CanMove;
        }

        public override void TurnTakerUpdate()
        {
            base.TurnTakerUpdate();
        }

        public override void TurnBeginning()
        {
            base.TurnBeginning();

            if (TurnManager.instance.State != TurnManager.TurnManagerState.Standby)
            {
                _spawnMovementRange.Spawn(transform.position, _characterToMove.speed);
            }
            else
            {
                _spawnMovementRange.Despawn();
            }

            State = TurnTakerState.TurnBody;
        }

        public override void TurnEnding()
        {
            base.TurnEnding();
        }

        public override void TurnBody()
        {
            base.TurnBody();

            _characterToMove.CheckDirections();

            if (Input.GetKeyDown(KeyCode.W) && _characterToMove.Up)
            {
                _characterToMove.Move(Vector2.up, true);
            }
            else if (Input.GetKeyDown(KeyCode.A) && _characterToMove.Left)
            {
                _characterToMove.Move(Vector2.left, true);
            }
            else if (Input.GetKeyDown(KeyCode.S) && _characterToMove.Down)
            {
                _characterToMove.Move(Vector2.down, true);
            }
            else if (Input.GetKeyDown(KeyCode.D) && _characterToMove.Right)
            {
                _characterToMove.Move(Vector2.right, true);
            }
        }
    }
}
