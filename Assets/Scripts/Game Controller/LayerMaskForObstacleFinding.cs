using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.GameController
{
    public class LayerMaskForObstacleFinding : MonoBehaviour
    {
        public static LayerMaskForObstacleFinding lmof;

        public LayerMask layerMask;

        private void Start()
        {
            if(lmof == null)
            {
                lmof = this;
            }
            if(lmof != this)
            {
                if(GetComponent<Util.ID>() != null)
                {
                    DestroyedTracker.instance.TrackDestroyedObject(GetComponent<Util.ID>().id);
                }

                Destroy(this);
            }
        }
    }
}
