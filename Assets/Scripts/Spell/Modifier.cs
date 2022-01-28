using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    public class Modifier : MonoBehaviour
    {
        public int speed;
        public int manaCost;
        public int spellPointCost;
        public int actionPointCost;

        public int damage;
        public int range;
        public int duration;//in turns

        [Space(5)]
        [Header("Misc")]
        public Sprite icon;
        public bool inUse = false;
        public UI.MagicFrame inventoryFrame;
        public UI.MagicFrame activeFrame;
        public string modifierName;
        [TextArea(6, 6)]
        public string description;

        public void InvokeModifier()
        {

        }

        public void InvokeModifier(Target target)
        {

        }

        public void SetInUse(bool setTo, UI.MagicFrame frame)
        {
            inUse = setTo;

            if (!setTo)
            {
                activeFrame = null;
                inventoryFrame = frame;
            }
            else if (inventoryFrame == null)
                inventoryFrame = frame;
            else
                activeFrame = frame;
        }
    }
}
