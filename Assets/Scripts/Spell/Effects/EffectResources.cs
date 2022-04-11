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
        [SerializeField]
        private AudioClip spellAudio_Default;
        [SerializeField]
        private AudioClip spellAudio_Attack;
        [SerializeField]
        private AudioClip spellAudio_ArcaneBolt;
        [SerializeField]
        private AudioClip spellAudio_Enchant;
        [SerializeField]
        private AudioClip spellAudio_Inflame;
        [SerializeField]
        private AudioClip spellAudio_ControlObject;
        [SerializeField]
        private AudioClip spellAudio_Possess;
        [SerializeField]
        private AudioClip spellAudio_Teleport;
        [SerializeField]
        private AudioClip spellAudio_Summon;

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
        public AudioClip GetDefaultSpellAudio() { return spellAudio_Default; }
        public AudioClip GetArcaneBoltAudio() { return spellAudio_ArcaneBolt; }
        public AudioClip GetAttackAudio() { return spellAudio_Attack; }
        public AudioClip GetEnchantAudio() { return spellAudio_Enchant; }
        public AudioClip GetInflameAudio() { return spellAudio_Inflame; }
        public AudioClip GetControlObjectAudio() { return spellAudio_ControlObject; }
        public AudioClip GetPossessAudio() { return spellAudio_Possess; }
        public AudioClip GetTeleportAudio() { return spellAudio_Teleport; }
        public AudioClip GetSummonAudio() { return spellAudio_Summon; }
    }
}
