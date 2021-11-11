using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.UI
{
    public class SS_FollowMouse : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Input.mousePosition;
        }
    }
}
