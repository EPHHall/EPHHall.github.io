using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.CharacterMovement
{
    public class SpawnMovementRange
    {
        private LayerMask _whatIsWall;
        private LayerMask _whatCantBeWalkedThrough;

        private GameObject _movementTilePrefab;
        private GameObject _wallTilePrefab;

        public SpawnMovementRange(GameObject movementTilePrefab, GameObject wallTilePrefab)
        {
            _movementTilePrefab = movementTilePrefab;
            _wallTilePrefab = wallTilePrefab;

            _whatIsWall = LayerMask.GetMask("Default", "Wall");
            _whatCantBeWalkedThrough = LayerMask.GetMask("Default", "Wall", "Character");
        }

        public void Spawn(Vector2 initialPosition, int range)
        {
            Despawn();

            List<Vector2> initialPositions = new List<Vector2>();
            initialPositions.Add(initialPosition);

            List<Vector2> previousPositions = new List<Vector2>();

            initialPositions = Util.BreadthFirstSearch.BFS(initialPositions, previousPositions, _whatCantBeWalkedThrough, range);

            List<Vector2> takenPositions = new List<Vector2>();
            foreach(Vector2 position in previousPositions)
            {
                if (takenPositions.Contains(position)) continue;

                GameObject.Instantiate(_movementTilePrefab, position, Quaternion.identity);
                takenPositions.Add(position);
            }
            foreach(Vector2 position in initialPositions)
            {
                if (takenPositions.Contains(position)) continue;

                GameObject.Instantiate(_movementTilePrefab, position, Quaternion.identity);
                takenPositions.Add(position);
            }

            List<Vector2> wallPositions = Util.BreadthFirstSearch.BFS(initialPositions, previousPositions, _whatIsWall);
            foreach(Vector2 wallPosition in wallPositions)
            {
                GameObject.Instantiate(_wallTilePrefab, wallPosition, Quaternion.identity);
            }
        }

        public void Despawn()
        {
            foreach (Tile tile in GameObject.FindObjectsOfType<Tile>())
            {
                tile.gameObject.SetActive(false);
            }
        }
    }
}
