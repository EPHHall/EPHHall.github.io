using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.PlayerMovement
{
    public class SS_PlayerMoveRange : MonoBehaviour
    {
        private static SS_PlayerMoveRange mainPlayerMoveRange;

        public GameObject wallTile;
        public GameObject moveTile;
        public int moveRange;
        public Vector2 origin;
        public Transform player;

        public bool spawnMoveRange = false;
        public bool firstTurn = true;

        public LevelDesign.Room room;

        [Space(5)]
        [Header("Debug")]
        public bool playerPositionIsOrigin;
        public bool showMainMoveRange;

        private void Start()
        {
            if (mainPlayerMoveRange == null && this.tag == "Player")
            {
                mainPlayerMoveRange = this;
            }
            else
            {
                room = mainPlayerMoveRange.room;
            }

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

        public void Initialize()
        {
            if (GetComponent<Character.CharacterStats>() != null && GetComponent<Character.CharacterStats>().speed <= 0)
            {
                GetComponent<Character.CharacterStats>().speed = 5;
            }

            wallTile = mainPlayerMoveRange.wallTile;
            moveTile = mainPlayerMoveRange.moveTile;
        }

        private void Update()
        {
            if (showMainMoveRange)
            {
                showMainMoveRange = false;
                Debug.Log(mainPlayerMoveRange, mainPlayerMoveRange.gameObject);
            }

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

            GetComponent<SS_PlayerController>().pauseMovementBecauseRangeWasShown = false;

            Util.SpawnRange.DespawnRange();

            if (room != null && !room.cleared)
            {
                if(playerPositionIsOrigin)
                    SS.Util.SpawnRange.SpawnMovementRange(player.position, moveRange + 1, moveTile, wallTile, true);
                else
                    SS.Util.SpawnRange.SpawnMovementRange(origin, moveRange + 1, moveTile, wallTile, true);
            }
        }

        public void ResetMoveRange(Vector2 origin)
        {
            Debug.Log("MoveRange ResetMoveRange");

            spawnMoveRange = true;
            this.origin = origin;

            SpawnRange();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<LevelDesign.Room>() != null)
            {
                Debug.Log("MoveRange OnTrigger");

                room = collision.GetComponent<LevelDesign.Room>();
            }
        }
    }
}
