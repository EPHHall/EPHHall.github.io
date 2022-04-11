using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Util
{
    public class CheckIfWayIsClear : MonoBehaviour
    {
        public static bool Check(Collider2D[] hits, bool ignorePlayer)
        {
            bool result = false;
            bool preventSettingTrueByPit = false;

            foreach (Collider2D collider2D in hits)
            {
                if (collider2D != null && collider2D.tag == "Player" && ignorePlayer)
                {
                    return false;
                }

                if (collider2D == null)
                {
                    continue;
                }

                if (collider2D.tag == "Bridge")
                {
                    //Debug.Log("Hit Bridge");

                    result = false;
                    preventSettingTrueByPit = true;
                }
                else if (collider2D.tag == "Pit")
                {
                    if (!preventSettingTrueByPit)
                    {
                        result = true;
                    }
                }
                else if (collider2D.tag == "Player")
                {
                    //Debug.Log("Hit Player");

                    result = false;
                    break;
                }
                else if (collider2D.tag == "Can Walk Through")
                {
                    result = false;
                }
                else //The collider isn't null and also isnn't one of the possible exceptions to being aa wall
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public static bool Check(RaycastHit2D[] rays, bool ignorePlayer)
        {
            bool result = true;
            bool preventSettingFalseByPit = false;

            foreach (RaycastHit2D ray in rays)
            {
                if (ray.collider != null && ray.collider.tag == "Player" && ignorePlayer)
                {
                    return true;
                }

                if (ray.collider == null)
                {
                    continue;
                }


                if (ray.collider.tag == "Bridge")
                {
                    result = true;
                    preventSettingFalseByPit = true;
                }
                else if (ray.collider.tag == "Pit")//Doing the if like this rather than just making it an and statement makes sure that the final else statemennt wont catch the tag = pit case
                {
                    if (!preventSettingFalseByPit)
                    {
                        result = false;
                    }
                }
                else if (ray.collider.tag == "Can Walk Through")
                {
                    result = true;
                }
                else if (ray.collider != null)
                { 
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}
