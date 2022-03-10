using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.StatusSpace;

namespace SS.Spells
{
    [ExecuteAlways]
    public class Teleport : Effect_Summoning
    {
        public bool reset;
        public int resetCounter = 0;

        public override void Awake()
        {
            base.Awake();

            manaCost = 5;
            spellPointCost = 4;
            baseDamage = 0;

            range = 6;
            radius = 1;

            actionPointCost = 1;

            duration = 2;

            normallyValid = new TargetType(true, true, true, true);

            originalDamageList.Clear();
            ResetMainDamageList();

            originalStatusList.Clear();
            ResetMainStatusList();

            style = Style.Utility;
        }

        public override void Update()
        {
            base.Update();

            if (reset)
            {
                if (resetCounter >= 1)
                {
                    resources.GetPlayer().GetComponent<PlayerMovement.SS_PlayerMoveRange>().SpawnRange();
                    reset = false;
                }

                resetCounter++;
            }
            else
            {
                resetCounter = 0;
            }
        }

        // There are 2 parts to this: Selecting the target to move, and then actually moving them. The first part can be done simply by
        // casting the spell, but the second part also requires user input, to choose where the target actually ends up.
        // So maybe, when the spell is cast, the player chooses the target to move. After that, a new range is spawned and the player is
        // prompted to select the square to move the target to, probably just using the message class already in place that appears above
        // the player's head. Also disable the spell buttons, OR make it so that trying to cast another spell simply ends this one. To that end,
        // see if we can only take mana when the teleport actually happens.
        public override void InvokeEffect(List<Target> targets)
        {
            base.InvokeEffect(targets);

            Target toTeleport;
            toTeleport = GetTeleportee(targets);

            HandleDeliveredAndTargeting(targets, toTeleport.gameObject);

            ChooseWhereToTeleport(toTeleport);

            EndInvoke();
        }

        public Target GetTeleportee(List<Target> targets)
        {
            return targets[0];
        }

        public void ChooseWhereToTeleport(Target toTeleport)
        {
            ChooseWhereToTeleport(toTeleport, transform.position);
        }

        public void ChooseWhereToTeleport(Target toTeleport, Vector2 origin)
        {
            resources.GetPlayerUpdateText().SetMessage("Choose new location");

            Util.SpawnRange.DespawnRange();

            List<TeleportTile> teleportTiles = Util.SpawnRange.SpawnTargetingRange<TeleportTile>(origin, spellAttachedTo.range, resources.GetTeleportTile(), resources.GetTeleportTile());
            foreach (TeleportTile teleportTile in teleportTiles)
            {
                teleportTile.toTeleport = toTeleport.transform;
                teleportTile.teleport = this;
            }
        }
    }
}
