using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Character
{
    public class RefillStats : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CharacterStats>() != null)
            {
                Refill(collision.GetComponent<CharacterStats>());
            }
        }

        public void Refill(CharacterStats stats)
        {
            SS.Util.CharacterStatsInterface.ResetAP(stats);
            SS.Util.CharacterStatsInterface.ResetMana(stats);
            SS.Util.CharacterStatsInterface.ResetHealth(stats);
        }
    }
}
