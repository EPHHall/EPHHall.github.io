using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.UI
{
    public class SpawnFollowingUI : MonoBehaviour
    {
        public GameObject toSpawn;

        private void Awake()
        {
            GameObject canvas = GameObject.Find("Main Canvas");

            GameObject gameObject = Instantiate(toSpawn, canvas.transform);
            gameObject.GetComponent<Follow>().toFollow = transform;
            gameObject.name = name + " Follower";
        }
    }
}
