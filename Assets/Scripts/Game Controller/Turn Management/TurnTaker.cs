using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.GameController
{
    public class TurnTaker : MonoBehaviour
    {
        public bool dontAutomaticallyAdd = false;

        public void Start()
        {
        }

        public virtual void EndTurn()
        {
            SS.Util.SpawnRange.DespawnRange();
        }

        public virtual void StartTurn()
        {
            SS.Util.SpawnRange.DespawnRange();

            SS.Spells.Target target = null;
            if (TryGetComponent<SS.Spells.Target>(out target))
            {
                //target.TurnChange(false);
            }
        }

        public virtual void OnDestroy()
        {
            if (transform.Find("Turn Marker"))
            {
                transform.Find("Turn Marker").parent = null;
            }

            TurnManager.tm.RemoveTurnTakerFromPlay(this);
        }
    }
}
