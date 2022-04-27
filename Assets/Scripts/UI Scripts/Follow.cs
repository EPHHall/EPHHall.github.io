using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.UI
{
    public class Follow : MonoBehaviour
    {
        [Space(5)]
        [Header("Only 1 / Only the first 1 applies")]
        public bool followTurnTaker;

        public Transform toFollow;

        [Space(5)]
        [Header("Don't Touch")]
        private RectTransform rectTransform;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();

            if(toFollow == null)
            {
                toFollow = transform.parent;
            }
        }

        void Update()
        {
            if (followTurnTaker)
            {
                toFollow = SS.GameController.TurnManager.currentTurnTaker.transform;
            }

            if (toFollow == null)
            {
                if (GetComponent<SS.Util.ID>() != null)
                {
                    SS.GameController.DestroyedTracker.instance.TrackDestroyedObject(GetComponent<SS.Util.ID>().id);
                }

                Destroy(gameObject);
            }

            if (toFollow != null)
            {
                transform.position = toFollow.transform.position;
            }
        }
    }
}
