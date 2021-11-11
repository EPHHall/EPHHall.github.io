using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.UI
{
    public class TargetButtonRadialMenu : MonoBehaviour
    {
        public SS.Spells.Target associatedTarget;

        [Space(5)]
        [Header("Selected Stuff")]
        public Transform highlight;

        public void SetAssociatedTarget(SS.Spells.Target newTarget)
        {
            associatedTarget = newTarget;

            GetComponent<Button>().onClick.RemoveAllListeners();

            //Sets up the button to select the associated target aand become highlighted after clicking it.
            GetComponent<Button>().onClick.AddListener(SS.Spells.Target.ClearSelectedTargets);
            GetComponent<Button>().onClick.AddListener(associatedTarget.SelectThisButton);
            GetComponent<Button>().onClick.AddListener(Highlight);
        }

        public void Highlight()
        {
            if (SS.Spells.Target.selectedTargets.Contains(associatedTarget))
            {
                highlight.gameObject.SetActive(true);

                highlight.position = transform.position;
            }
            else
            {
                highlight.gameObject.SetActive(false);
            }
        }
    }
}
