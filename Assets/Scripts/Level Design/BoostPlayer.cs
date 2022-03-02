using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.LevelDesign
{
    public class BoostPlayer : MonoBehaviour
    {
        public bool vertical;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                //Debug.Log("Buh");

                if (vertical)
                {
                    if (collision.transform.position.y < transform.position.y)
                    {
                        collision.transform.position += new Vector3(0, 3, 0);
                    }
                    else if (collision.transform.position.y > transform.position.y)
                    {
                        collision.transform.position += new Vector3(0, -3, 0);
                    }
                }
                else
                {
                    if (collision.transform.position.x < transform.position.x)
                    {
                        collision.transform.position += new Vector3(3, 0, 0);
                    }
                    else if (collision.transform.position.x > transform.position.x)
                    {
                        collision.transform.position += new Vector3(-3, 0, 0);
                    }
                }
            }
        }
    }
}
