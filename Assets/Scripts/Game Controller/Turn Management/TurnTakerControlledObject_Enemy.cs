using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.GameController
{
    public class TurnTakerControlledObject_Enemy : TurnTaker
    {
        AI.Agent agent;

        public void Initialize()
        {
            agent = GetComponent<AI.Agent>();

            agent.enabled = true;
            agent.Start();
        }

        public int EndControl()
        {
            int previousIndex = -1;

            if (TurnManager.instance.TurnTakers.Contains(this))
            {
                previousIndex = TurnManager.instance.TurnTakers.IndexOf(this);
                TurnManager.instance.TurnTakers.Remove(this);
            }


            return previousIndex;
            //Destroy(this);
        }
    }
}
