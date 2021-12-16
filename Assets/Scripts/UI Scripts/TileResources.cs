using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.UI
{
    public class TileResources : MonoBehaviour
    {
        public StatsCard statsCard;

        void Start()
        {
            statsCard = FindObjectOfType<StatsCard>();
        }
    }
}
