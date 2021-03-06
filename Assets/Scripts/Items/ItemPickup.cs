using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Item
{
    public class ItemPickup : MonoBehaviour
    {
        public Spells.Spell_Attack parent;
        public SS.UI.UpdateText updateText;
        public string message;
        public Weapon toPickUp;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                BePickedUp();

                if (GetComponent<Util.ID>() != null)
                {
                    GameController.DestroyedTracker.instance.TrackDestroyedObject(GetComponent<Util.ID>().id);
                }

                Destroy(gameObject);
            }
        }

        public void BePickedUp()
        {
            Debug.Log(toPickUp.spellToCast);

            Weapon.CreateWeapon(toPickUp, parent, toPickUp.weaponName);

            DisplayMessage();
        }

        public void DisplayMessage()
        {
            updateText.SetMessage(message);
        }
    }
}
