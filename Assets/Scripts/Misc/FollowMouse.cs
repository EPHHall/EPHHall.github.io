using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Misc
{
    public class FollowMouse : MonoBehaviour
    {
        private void Start()
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        void Update()
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
