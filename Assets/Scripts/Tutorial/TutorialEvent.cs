using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TutorialEvent : MonoBehaviour
    {
        public bool trigger;
        public TutorialHandler handler;

        private void Start()
        {
            handler = FindObjectOfType<TutorialHandler>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (trigger && collision.tag == "Player")
            {
                handler.StartCurrentScene(0);
            }
        }
    }
}
