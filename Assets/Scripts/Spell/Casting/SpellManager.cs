using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    public class SpellManager: MonoBehaviour
    {
        public static Spell activeSpell;
        public bool displayActiveSpell;

        public static void CastActiveSpell()
        {
            if (activeSpell != null && Target.selectedTargets.Count > 0)
            {
                activeSpell.CastSpell(false);

                GameObject.FindObjectOfType<SS.UI.TargetMenu>().DeactivateMenu();
            }
        }

        public void Update()
        {
            if (displayActiveSpell)
            {
                displayActiveSpell = false;

                Debug.Log(activeSpell.name);
            }
        }
    }
}
