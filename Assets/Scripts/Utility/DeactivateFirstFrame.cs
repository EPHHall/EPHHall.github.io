using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateFirstFrame : MonoBehaviour
{
    bool deactivatedYet = false;

    void LateUpdate()
    {
        if (!deactivatedYet)
        {
            deactivatedYet = true;

            gameObject.SetActive(false);
        }
    }
}
