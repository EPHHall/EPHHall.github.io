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
            foreach(Tile tile in GameObject.FindObjectsOfType<Tile>())
            {
                tile.gameObject.SetActive(false);
            }

            List<Vector2> initialPostions = new List<Vector2>();
            initialPostions.Add(initialPosition);

            List<Vector2> previousPositions = new List<Vector2>();

            initialPostions = Util.BreadthFirstSearch.BFS(initialPostions, previousPositions, _whatCantBeWalkedThrough, range);

            foreach(Vector2 position in initialPostions)
            {
                GameObject.Instantiate(_movementTilePrefab, position, Quaternion.identity);
            }

            List<Vector2> wallPositions = Util.BreadthFirstSearch.BFS(initialPostions, previousPositions, _whatIsWall);
            foreach(Vector2 wallPosition in wallPositions)
            {
                GameObject.Instantiate(_wallTilePrefab, wallPosition, Quaternion.identity);
            }
        }
    }
}
