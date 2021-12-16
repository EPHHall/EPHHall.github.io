using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Util
{
    public class GetFollowers : MonoBehaviour
    {
        public static bool TargetsPresent(Collider2D[] list)
        {
            foreach (Collider2D c in list)
            {
                if (c.GetComponent<SS.UI.CharacterFollower>())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
