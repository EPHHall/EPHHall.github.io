using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    public class MagicPickup : MonoBehaviour
    {
        public SS.UI.SpellInventory spellInventory;
        public SS.UI.UpdateText updateText;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                updateText = GameObject.FindObjectOfType<SS.UI.UpdateText>();

                BePickedUp();
            }
        }

        public virtual void BePickedUp()
        {
            spellInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<SS.UI.SpellInventory>();
        }

        public virtual void DisplayMessage(string message)
        {
            updateText.SetMessage(message);
        }
    }
}
