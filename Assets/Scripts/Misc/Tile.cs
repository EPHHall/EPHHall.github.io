using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS
{
    public class Tile : MonoBehaviour
    {
        public bool dontDespawn = false;

        private void OnDisable()
        {
            if (!dontDespawn)
            {
                Destroy(gameObject, .1f);
            }
        }
    }
}
