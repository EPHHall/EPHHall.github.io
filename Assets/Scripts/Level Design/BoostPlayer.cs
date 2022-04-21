using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.LevelDesign
{
    public class BoostPlayer : MonoBehaviour
    {
        public bool vertical;
        private Transform player;

        public virtual void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                Boost();
            }
        }

        public virtual void Boost()
        {
            if (vertical)
            {
                BoostVertical();
            }
            else
            {
                BoostHorizontal();
            }
        }

        public virtual void Repel()
        {
            if (vertical)
            {
                BoostVertical();
            }
            else
            {
                BoostHorizontal();
            }
        }

        public virtual void BoostVertical()
        {
            if (player.position.y < transform.position.y)
            {
                player.position += new Vector3(0, 3, 0);
            }
            else if (player.position.y > transform.position.y)
            {
                player.position += new Vector3(0, -3, 0);
            }
        }

        public virtual void BoostHorizontal()
        {
            if (player.position.x < transform.position.x)
            {
                player.position += new Vector3(3, 0, 0);
            }
            else if (player.position.x > transform.position.x)
            {
                player.position += new Vector3(-3, 0, 0);
            }
        }

        public virtual void RepelVertical()
        {
            if (player.position.y < transform.position.y)
            {
                player.position += new Vector3(0, -1, 0);
            }
            else if (player.position.y > transform.position.y)
            {
                player.position += new Vector3(0, 1, 0);
            }
        }

        public virtual void RepelHorizontal()
        {
            if (player.position.x < transform.position.x)
            {
                player.position += new Vector3(-1, 0, 0);
            }
            else if (player.position.x > transform.position.x)
            {
                player.position += new Vector3(1, 0, 0);
            }
        }
    }
}
