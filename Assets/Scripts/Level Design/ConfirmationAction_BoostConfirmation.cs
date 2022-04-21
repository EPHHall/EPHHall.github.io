using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.LevelDesign
{
    public class ConfirmationAction_BoostConfirmation : ConfirmationAction
    {
        [Space(10)]
        [Header("ConfirmationAction_BoostConfirmation")]
        private BoostPlayer boostPlayer;

        public ConfirmationAction_BoostConfirmation(BoostPlayer boostPlayer)
        {
            this.boostPlayer = boostPlayer;
        }

        public override void Confirm()
        {
            base.Confirm();

            boostPlayer.Boost();
        }

        public override void Deny()
        {
            base.Deny();
            Debug.Log("Deny");

            boostPlayer.Repel();
        }
    }
}
