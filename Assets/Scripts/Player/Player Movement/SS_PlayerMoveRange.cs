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

        private Vector2 _origin;
        private bool _spawnMoveRange = false;
        private bool _firstTurn = true;

        private LevelDesign.Room _room;

        [Space(5)]
        [Header("Debug")]
        public bool playerPositionIsOrigin;
        public bool showMainMoveRange;

        private void Start()
        {
            _origin = transform.position;
        }

        private void Update()
        {
            if (_firstTurn)
            {
                _spawnMoveRange = false;
                _firstTurn = false;
            }

            if (_spawnMoveRange)
            {

                SpawnRange("PlayerMoveRange, Update");
                _spawnMoveRange = false;
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

        public void SpawnRange(string caller)
        {
            //Debug.Log("MoveRange SpawnRange, Name fo caller = " + caller);

            GetComponent<SS_PlayerController>().pauseMovementBecauseRangeWasShown = false;

            Util.SpawnRange.DespawnRange();

            if (_room != null && !_room.cleared)
            {
                if(playerPositionIsOrigin)
                    SS.Util.SpawnRange.SpawnMovementRange(transform.position, moveRange + 1, moveTile, wallTile, true);
                else
                    SS.Util.SpawnRange.SpawnMovementRange(_origin, moveRange + 1, moveTile, wallTile, true);
            }
        }

        public void ResetMoveRange(Vector2 origin, string caller)
        {
            _spawnMoveRange = true;
            _origin = origin;

            SpawnRange(caller);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<LevelDesign.Room>() != null)
            {
                _room = collision.GetComponent<LevelDesign.Room>();
            }
        }
    }
}
