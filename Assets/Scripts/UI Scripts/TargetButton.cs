using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.UI
{
    public class TargetButton : MonoBehaviour
    {
        public Text text;
        public SS.Spells.Target associatedTarget;

        [Space(5)]
        [Header("Colors/Selected Stuff")]
        Color defaultColor;
        public Color selectedColor;
        Image image;

        public void Start()
        {
            text = transform.GetComponentInChildren<Text>();
            image = GetComponent<Image>();
            defaultColor = image.color;
        }

        public void SetAssociatedTarget(SS.Spells.Target newTarget)
        {
            associatedTarget = newTarget;
            text.text = newTarget.name;

            GetComponent<Button>().onClick.RemoveAllListeners();
            GetComponent<Button>().onClick.AddListener(associatedTarget.SelectThisButton);
        }

        private void Update()
        {
            if (SS.Spells.SpellManager.activeSpell.main.normallyValid.
                DoesGTETOneTypeMatch(associatedTarget.targetType))
            {
                GetComponent<Button>().interactable = true;
            }
            else
            {
                GetComponent<Button>().interactable = false;
            }

            if (SS.Spells.Target.selectedTargets.Contains(associatedTarget))
            {
                image.color = selectedColor;
            }
            else
            {
                image.color = defaultColor;
            }
        }

        private void OnDestroy()
        {
            //Debug.Log("Lol Destroyed");
        }
    }
}
