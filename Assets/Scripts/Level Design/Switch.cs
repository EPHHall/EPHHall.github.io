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
        }
    }
}
