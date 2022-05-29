using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Character
{
    public class FillHPAtEndOfTurn : MonoBehaviour
    {
        GameController.TurnTaker tt;
        CharacterStats stats;
        bool wasActive;

        private void Start()
        {
            tt = GetComponent<GameController.TurnTaker>();
            stats = GetComponent<CharacterStats>();
        }

        // Update is called once per frame
        void Update()
        {
            if (tt != null)
            {
                if (GameController.TurnManager.instance.CurrentTurnTaker == tt)
                {
                    wasActive = true;
                }
                else if(wasActive)
                {
                    wasActive = false;

                    if (stats != null)
                    {
                        stats.ResetHealth();
                    }
                }
            }
        }
    }
}
