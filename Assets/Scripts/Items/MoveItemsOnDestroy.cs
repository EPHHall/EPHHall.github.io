using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Item
{
    public class MoveItemsOnDestroy : MonoBehaviour
    {


        private void Start()
        {
            Transform toMoveParent = transform.Find("Move To Position");

            if (toMoveParent == null) return;

            foreach (Transform child in toMoveParent.GetComponentsInChildren<Transform>())
            {
                child.position = new Vector2(int.MaxValue, int.MaxValue);
            }
        }

        private void OnDestroy()
        {
            Transform toMoveParent = transform.Find("Move To Position");

            if (toMoveParent == null) return;

            foreach (Transform child in toMoveParent.GetComponentsInChildren<Transform>())
            {
                child.position = transform.position;
                child.parent = null;
            }

            if (GetComponent<Util.ID>() != null)
            {
                if(GameController.DestroyedTracker.instance != null)
                    GameController.DestroyedTracker.instance.TrackDestroyedObject(GetComponent<Util.ID>().id);
            }
        }
    }
}
