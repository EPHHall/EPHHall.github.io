using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;

namespace SS.Misc
{
    public class Pointer : MonoBehaviour
    {
        //I dont expect there to be casting tiles stacked on top of eachother, which is basically what this dictionary (and the lists later) allows for; 
        //for the most part (I think) this dictionary will only have 1 entry.
        private Dictionary<CastingTile, bool> castingTilesDetected = new Dictionary<CastingTile, bool>();

        public SS.UI.StatsCard statsCard;

        private void FixedUpdate()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, transform.lossyScale.x);
            foreach (Collider2D hit in hits)
            {
                if (statsCard != null)
                {
                    if (hit.GetComponent<CastingTile>() && !statsCard.pointerIsOver)
                    {
                        CastingTile tile = hit.GetComponent<CastingTile>();
                        tile.PointerEntered();

                        if (castingTilesDetected.ContainsKey(tile))
                        {
                            castingTilesDetected[tile] = true;
                        }
                        else
                        {
                            castingTilesDetected.Add(tile, true);
                        }
                    }
                }
                
                //TODO: Display the range that the player would need to enter to cause an enemy to move
                //AI.Package_IdleThenMoveAndAttack package = hit.GetComponent<AI.Package_IdleThenMoveAndAttack>();
                //if (package != null)
                //{

                //}
            }

            List<CastingTile> keysToRemove = new List<CastingTile>();
            List<CastingTile> keysToSetFalse = new List<CastingTile>();
            foreach (CastingTile tile in castingTilesDetected.Keys)
            {
                if (castingTilesDetected[tile])
                {
                    keysToSetFalse.Add(tile);
                }
                else
                {
                    keysToRemove.Add(tile);
                }
            }

            foreach (CastingTile key in keysToRemove)
            {
                key.PointerExited();
                castingTilesDetected.Remove(key);
            }
            foreach (CastingTile key in keysToSetFalse)
            {
                castingTilesDetected[key] = false ;
            }
        }
    }
}
