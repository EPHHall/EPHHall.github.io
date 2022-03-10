using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SS.Util
{
    [ExecuteAlways]
    public class ReplaceByTag : MonoBehaviour
    {
        public bool shouldReplace = false;

        public string tagOfObjectsToReplace;
        public GameObject replaceWith;

        void Update()
        {
            if (shouldReplace)
            {
                shouldReplace = false;

                if (replaceWith == null) return;

                GameObject[] toReplace = GameObject.FindGameObjectsWithTag(tagOfObjectsToReplace);
                for(int i = 0; i < toReplace.Length; i++)
                {
                    GameObject currentObject = toReplace[i];
                    GameObject newObject = PrefabUtility.InstantiatePrefab(replaceWith, currentObject.transform) as GameObject;
                    newObject.transform.localPosition = Vector2.zero;
                    newObject.transform.parent = currentObject.transform.parent;

                    DestroyImmediate(currentObject);
                }
            }
        }
    }
}
