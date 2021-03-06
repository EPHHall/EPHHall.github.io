using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SS.Spells
{
    public class TeleportTile : CastingTile
    {
        public Transform toTeleport;
        public Teleport teleport;

        public override void SelectTile()
        {
            if (toTeleport == null) return;

            toTeleport.position = transform.position;

            teleport.reset = true;
        }
    }
}
