using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.LevelDesign
{
    public class Switch : Interactable
    {
        public List<GameObject> toDeactivate;
        public List<GameObject> toActivate;

        public Sprite[] sprites = new Sprite[2];
        private SpriteRenderer sr;

        public bool switchActive = false;

        public PlayerMovement.SS_PlayerMoveRange playerMoveRange;

        public override void Start()
        {
            base.Start();

            sr = GetComponent<SpriteRenderer>();

            if (switchActive)
            {
                switchActive = false;
                ActivateInteractable();
            }
            else
            {
                DeactivateInteractable();
            }

            //playerMoveRange = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement.SS_PlayerMoveRange>();
        }

        public override void Update()
        {
            base.Update();

            if (switchActive)
            {
                sr.sprite = sprites[1];
            }
            else
            {
                sr.sprite = sprites[0];
            }
        }

        public override void ActivateInteractable()
        {
            base.ActivateInteractable();

            switchActive = !switchActive;

            if (!switchActive)
            {
                DeactivateInteractable();
            }
            else
            {
                foreach (GameObject go in toActivate)
                {
                    go.SetActive(true);
                }
                foreach (GameObject go in toDeactivate)
                {
                    go.SetActive(false);
                }
            }


            Debug.Log("Switch ActivateInteractable");
            if (playerMoveRange != null)
            {
                Debug.Log("Switch ActivateInteractable in If");
                playerMoveRange.SpawnRange();
            }

            Tutorial.TutorialHandler.switchWasActivated = true;
        }

        public override void DeactivateInteractable()
        {
            base.DeactivateInteractable();

            foreach (GameObject go in toActivate)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in toDeactivate)
            {
                go.SetActive(true);
            }

            playerMoveRange.SpawnRange();
        }
    }
}
