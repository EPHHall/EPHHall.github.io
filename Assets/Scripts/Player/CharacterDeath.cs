using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Character
{
    public class CharacterDeath : MonoBehaviour
    {
        public Tutorial.TutorialBox textBox;
        public Vector2 textBoxPosition;
        public string deathMessage;
        public string deathTitle;

        public bool respawn;
        public Vector2 respawnPoint;

        public virtual void Death(CharacterStats stats)
        {
            if (textBox != null)
            {
                textBox.gameObject.SetActive(true);
                textBox.SetPos(textBoxPosition);
                textBox.SetText(deathMessage);
                textBox.SetTitle(deathTitle);
                textBox.HandleArrow(Tutorial.TutorialBox.ArrowOrientation.None, Tutorial.TutorialBox.ArrowPos.Bottom);
                textBox.ActivateButtons();
            }

            if (respawn)
            {
                stats.transform.position = respawnPoint;
                stats.hp = stats.hpMax;
            }
            else
            {
                Destroy(stats.gameObject);
            }
        }
    }
}
