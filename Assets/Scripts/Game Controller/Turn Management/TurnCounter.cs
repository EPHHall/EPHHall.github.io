using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.GameController
{
    public class TurnCounter : MonoBehaviour
    {
        public class CountdownAndEffect
        {
            public int countdown = 0;
            public SS.Spells.Effect callbackEffect = null;

            public bool finished = false;

            public CountdownAndEffect(int countDown, SS.Spells.Effect callbackEffect)
            {
                this.countdown = countDown;
                this.callbackEffect = callbackEffect;
            }
            public CountdownAndEffect(int countDown)
            {
                this.countdown = countDown;
            }

            public bool DecrementCountdown()
            {
                countdown--;
                if (countdown <= 0)
                {
                    finished = true;

                    if (callbackEffect != null)
                    {
                        callbackEffect.EndEffect();
                    }
                    return true;
                }

                return false;
            }
        }

        public List<CountdownAndEffect> countDowns = new List<CountdownAndEffect>();

        public void DecrementCountdowns()
        {
            for (int i = 0; i < countDowns.Count; i++)
            {
                if (countDowns[i].DecrementCountdown())
                {
                    countDowns.Remove(countDowns[i]);
                }
            }

            if (countDowns.Count <= 0)
            {
                Destroy(this);
            }
        }
    }
}
