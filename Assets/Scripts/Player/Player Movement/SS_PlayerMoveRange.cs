using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.PlayerMovement
{
    public class SS_PlayerMoveRange : MonoBehaviour
    {
        public GameObject wallTile;
        public GameObject moveTile;
        public int moveRange;
        public Vector2 origin;
        public Transform player;

        public bool spawnMoveRange = false;
        public bool firstTurn = true;

        [Space(5)]
        [Header("Debug")]
        public bool playerPositionIsOrigin;

        private void Start()
        {
            player = transform;
            origin = transform.position;

            if (wallTile == null)
            {
                wallTile = GameObject.Find("Wall Tile");
            }
            if (moveTile == null)
            {
                moveTile = GameObject.Find("Move Tile");
            }
        }

        private void Update()
        {
            if (firstTurn)
            {
                spawnMoveRange = false;
                firstTurn = false;
            }

            if (spawnMoveRange)
            {

                SpawnRange();
                spawnMoveRange = false;
            }

            if (SS.LevelDesign.Room.currentlyActive != null && SS.LevelDesign.Room.currentlyActive.cleared)
            {
                SS.Util.SpawnRange.DespawnMoveRange();
            }

            if (GetComponent<Character.CharacterStats>() != null)
            {
                moveRange = GetComponent<Character.CharacterStats>().speed;
            }
        }

        public void SpawnRange()
        {
            Util.SpawnRange.DespawnRange();


            if(playerPositionIsOrigin)
                SS.Util.SpawnRange.SpawnMovementRange(player.position, moveRange, moveTile, wallTile, true);
            else
                SS.Util.SpawnRange.SpawnMovementRange(origin, moveRange, moveTile, wallTile, true);
        }

        public void ResetMoveRange(Vector2 origin)
        {
            spawnMoveRange = true;
            this.origin = origin;
        }
    }
}
