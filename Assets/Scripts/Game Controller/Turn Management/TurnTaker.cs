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
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, .1f);
            bool shouldDestroy = false;
            foreach (Collider2D col in cols)
            {
                if (col.tag == "Pit")
                {
                    shouldDestroy = true;
                }
            }

            if (shouldDestroy)
            {
                foreach(Collider2D col in cols)
                {
                    if(col.tag == "Bridge")
                    {
                        shouldDestroy = false;
                    }
                }
            }

            if(shouldDestroy)
            {
                if (GetComponent<Util.ID>() != null && GameController.DestroyedTracker.instance != null)
                {
                    GameController.DestroyedTracker.instance.TrackDestroyedObject(GetComponent<Util.ID>().id);
                }

                if (GetComponent<Character.CharacterStats>() != null)
                {
                    GetComponent<Character.CharacterStats>().hp = 0;
                }

                return;
            }

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

            if (GetComponent<Util.ID>() != null)
            {
                if(GameController.DestroyedTracker.instance != null)
                    DestroyedTracker.instance.TrackDestroyedObject(GetComponent<Util.ID>().id);
            }
        }
    }
}
