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

        public void InvokeModifier()
        {

        }

        public void InvokeModifier(Target target)
        {

        }
    }
}
