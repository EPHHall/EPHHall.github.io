using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Util
{
    public class SetChildrenInactive : MonoBehaviour
    {
        private void Start()
        {
            foreach (Transform child in transform.GetComponentInChildren<Transform>())
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
