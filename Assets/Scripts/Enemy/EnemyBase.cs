using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public int hp;
        public int mana;
        public int manaRegen;
        public int speed;

        public List<string> statusEffects;

        //TODO: Inventory inventory = new ...
    }
}
