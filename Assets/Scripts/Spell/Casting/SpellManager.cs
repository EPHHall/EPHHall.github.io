using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    public class SpellManager: MonoBehaviour
    {
        public static Spell activeSpell;
        public bool displayActiveSpell;

        public static List<Target> currentTargets = new List<Target>();
        public static Transform caster;

        public static void CastActiveSpell()
        {
            if (activeSpell != null && Target.selectedTargets.Count > 0)
            {
                activeSpell.CastSpell(false);

                if(GameObject.FindObjectOfType<SS.UI.TargetMenu>() != null)
                    GameObject.FindObjectOfType<SS.UI.TargetMenu>().DeactivateMenu();
            }
        }

        public static void SetTargetsList(List<Target> targets)
        {
            currentTargets.Clear();
            currentTargets = new List<Target>(targets);
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
