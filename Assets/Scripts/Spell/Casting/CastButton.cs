using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.Spells
{
    public class CastButton : MonoBehaviour
    {
        void Update()
        {
            if (SpellManager.activeSpell != null && SS.GameController.TurnManager.instance.CurrentTurnTaker.tag == "Player")
            {
                GetComponent<Image>().enabled = true;
            }
            else
            {
                GetComponent<Image>().enabled = false;
            }

            //if (SS.GameController.TurnManager.instance.CurrentTurnTaker.tag == "Player" && Target.selectedTargets.Count > 0)
            //{
            //    GetComponent<Image>().enabled = true;
            //}
            //else
            //{
            //    GetComponent<Image>().enabled = false;
            //}
        }
    }
}
