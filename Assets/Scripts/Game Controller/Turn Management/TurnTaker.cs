using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.GameController
{
    public class TurnTaker : MonoBehaviour
    {
        //These damage handling variables and functions should be in Target instead. TODO
        //Also, DamageStuff should probably be in Damage or TargetInterface, that would make more sense.

        public bool dontAutomaticallyAdd = false;

        public virtual void Awake()
        {
        }
        
        public virtual void Start()
        {
        }

        public virtual void EndTurn()
        {
            SS.Util.SpawnRange.DespawnRange();

            GetComponent<Spells.Target>().InflictEndOfTurnDamage();
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
