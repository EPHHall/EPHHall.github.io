using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Character
{
    public class CharacterDeath_TutorialDeath : CharacterDeath
    {
        public override void Death(CharacterStats stats)
        {
            base.Death(stats);
        }

        public override void ActivateInteractable()
        {
            LevelDesign.Room.currentlyActive.Reset();
        }
    }
}
