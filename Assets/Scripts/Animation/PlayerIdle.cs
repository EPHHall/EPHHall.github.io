using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Animation
{
    public class PlayerIdle : ScriptedAnimation_Idle
    {
        public void SetSprites(Sprite s1, Sprite s2)
        {
            sprite2 = s2;
            sprite1 = s1;
        }
    }
}
