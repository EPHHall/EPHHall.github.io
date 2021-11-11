using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SS.Util
{
    public class SpawnRange
    {
        static UnityEvent spawnDespawnEvent;

        private static void Start()
        {
            spawnDespawnEvent = new UnityEvent();
            spawnDespawnEvent.AddListener(SS.Spells.Target.ClearSelectedTargets);
        }

        public static void SpawnTargetingRange(Vector2 initialPosition, int range,
            GameObject mainTile, GameObject lastTile)
        {
            Start();

            SS.Spells.SpellManager.activeSpell = null;

            foreach (Tile tile in GameObject.FindObjectsOfType<Tile>())
            {
                tile.gameObject.SetActive(false);
            }

            List<Vector2> tilePositions = new List<Vector2>();
            List<Vector2> takenPositions = new List<Vector2>();
            tilePositions.Add(initialPosition);

            for (int i = 0; i < range; i++)
            {
                foreach (Vector2 position in tilePositions)
                {
                    SpawnTile(position, mainTile);
                    takenPositions.Add(position);
                }

                List<Vector2> temp = tilePositions;
                tilePositions = new List<Vector2>();

                SS_AStar aStar = new Util.SS_AStar();
                foreach (Vector2 position in aStar.AStar(temp, takenPositions))
                {
                    tilePositions.Add(position);
                }
            }

            foreach (Vector2 position in tilePositions)
            {
                SpawnTile(position, lastTile);
            }

            spawnDespawnEvent.Invoke();
        }
        public static void SpawnTargetingRange(List<Vector2> initialPositions, List<Vector2> takenPositions, int range,
            GameObject mainTile, GameObject lastTile)
        {
            Start();

            List<Vector2> tilePositions = initialPositions;
            List<Vector2> dontSpawn = new List<Vector2>();

            for (int i = 0; i < range; i++)
            {
                foreach (Vector2 position in tilePositions)
                {
                    //When being used to show move/attack ranges, spawning tiles on iteration 0 causes move and action tiles to overlap.
                    if (i > 0)
                    {
                        SpawnTile(position, mainTile);
                    }

                    takenPositions.Add(position);
                }

                List<Vector2> temp = tilePositions;
                tilePositions = new List<Vector2>();

                SS_AStar aStar = new Util.SS_AStar();

                foreach (Vector2 position in aStar.AStar(temp, takenPositions))
                {
                    tilePositions.Add(position);
                }
            }

            foreach (Vector2 position in tilePositions)
            {
                if (lastTile != null)
                {
                    SpawnTile(position, lastTile);
                }
            }

            spawnDespawnEvent.Invoke();
        }

        public static List<Vector2>[] SpawnMovementRange(Vector2 initialPosition, int range,
            GameObject mainTile, GameObject lastTile)
        {
            Start();

            SS.Spells.SpellManager.activeSpell = null;

            foreach (Tile tile in GameObject.FindObjectsOfType<Tile>())
            {
                tile.gameObject.SetActive(false);
            }

            List<Vector2> tilePositions = new List<Vector2>();
            List<Vector2> takenPositions = new List<Vector2>();
            tilePositions.Add(initialPosition);

            for (int i = 0; i < range; i++)
            {
                foreach (Vector2 position in tilePositions)
                {
                    SpawnTile(position, mainTile);
                    takenPositions.Add(position);
                }

                List<Vector2> temp = tilePositions;
                tilePositions = new List<Vector2>();

                SS_AStar aStar = new Util.SS_AStar();
                foreach (Vector2 position in aStar.AStar_ForMovement(temp, takenPositions))
                {
                    tilePositions.Add(position);
                }
            }

            foreach (Vector2 position in tilePositions)
            {
                SpawnTile(position, lastTile);
            }

            spawnDespawnEvent.Invoke();

            List<Vector2>[] lists = new List<Vector2>[2];
            lists[0] = tilePositions;
            lists[1] = takenPositions;

            return lists;
        }

        static void SpawnTile(Vector2 position, GameObject tile)
        {
            //SetActive is necessary because currently the ControlledObject needs tiles that are actually in
            //the scene, and those get set to inactive.
            MonoBehaviour.Instantiate(tile, position, Quaternion.identity).SetActive(true);
        }

        public static void DespawnRange()
        {
            Start();

            foreach (Tile tile in GameObject.FindObjectsOfType<Tile>())
            {
                tile.gameObject.SetActive(false);
            }

            spawnDespawnEvent.Invoke();
        }
        public static void DespawnMoveRange()
        {
            Start();

            foreach (Tile tile in GameObject.FindObjectsOfType<Tile>())
            {
                if (tile.tag == "Player Move" || tile.tag == "Player Wall")
                {
                    tile.gameObject.SetActive(false);
                }
            }

            spawnDespawnEvent.Invoke();
        }
    }
}
