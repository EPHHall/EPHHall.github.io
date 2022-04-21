using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.LevelDesign
{
    public class BoostPlayer_Confirmation : BoostPlayer
    {

        [Space(10)]
        [Header("BoostPlayer_Confirmation")]
        UI.ConfirmationBoxManager conBoxManager;
        public string content;
        ConfirmationAction_BoostConfirmation action;

        public override void Start()
        {
            base.Start();

            conBoxManager = FindObjectOfType<UI.ConfirmationBoxManager>();
            action = new ConfirmationAction_BoostConfirmation(this);
        }

        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            { 
                if (conBoxManager == null)
                {
                    base.OnTriggerEnter2D(collision);
                }
                else
                {
                    Debug.Log(action);
                    conBoxManager.BringUpBox(content, null, action, true);
                }
            }
        }
    }
}
