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
                if (GetComponent<SS.Util.ID>() != null)
                {
                    SS.GameController.DestroyedTracker.instance.TrackDestroyedObject(GetComponent<SS.Util.ID>().id);
                }

                Destroy(gameObject, .1f);
            }
        }
    }
}
