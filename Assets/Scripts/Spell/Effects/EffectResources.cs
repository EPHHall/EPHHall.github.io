using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.PlayerMovement;

namespace SS.Spells
{
    public class EffectResources : MonoBehaviour
    {
        [SerializeField]
        private GameObject castingTilePrefab;
        [SerializeField]
        private GameObject moveTilePrefab;
        [SerializeField]
        private GameObject wallTilePrefab;
        [SerializeField]
        private GameObject teleportTilePrefab;
        [SerializeField]
        private Target player;
        [SerializeField]
        private UI.UpdateText playerUpdateText;

        private void Start()
        {
            //player = GameObject.Find("Player").GetComponent<Target>();
            //playerUpdateText = GameObject.Find("Player Update Text").GetComponent<UI.UpdateText>();
        }

        public GameObject GetCastingTile() { return castingTilePrefab; }
        public GameObject GetMoveTile() { return moveTilePrefab; }
        public GameObject GetWallTile() { return wallTilePrefab; }
        public GameObject GetTeleportTile() { return teleportTilePrefab; }
        public Target GetPlayer() { return player; }
        public UI.UpdateText GetPlayerUpdateText() { return playerUpdateText; }
    }
}
