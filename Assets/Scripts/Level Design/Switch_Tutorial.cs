using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.LevelDesign
{
    public class Switch_Tutorial : Switch
    {
        public override void ActivateInteractable()
        {
            base.ActivateInteractable();

            Tutorial.TutorialHandler.switchWasActivated = true;
        }
    }
}
