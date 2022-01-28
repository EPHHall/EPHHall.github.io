using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.UI
{
    public class TileResources : MonoBehaviour
    {
        public StatsCard statsCard;

        [SerializeField]
        private Material lineMaterial;

        void Start()
        {
            statsCard = FindObjectOfType<StatsCard>();
        }

        public Material GetLineMaterial() { return lineMaterial; }
    }
}
