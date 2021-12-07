using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AI
{
    public class Faction : MonoBehaviour
    {
        public enum FactionName
        {
            PlayerFaction,
            PlayerEnemyFaction
        }

        public FactionName factionName;
    }
}
