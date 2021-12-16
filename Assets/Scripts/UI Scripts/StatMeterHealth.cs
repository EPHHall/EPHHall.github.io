using System.Collections;
using System.Collections.Generic;
using SS.Character;
using UnityEngine;

namespace SS.UI
{
    public class StatMeterHealth : StatMeter
    {
        public override void Start()
        {
            base.Start();

            SetSliderMax(stats.hpMax);
        }

        public override void SetStats(CharacterStats newStats)
        {
            if (newStats == null)
            {
                stats = null;
                SetSliderValue(0);
                SetSliderMax(0);

                return;
            }

            base.SetStats(newStats);

            SetSliderMax(stats.hpMax);
        }

        public override void Update()
        {
            base.Update();

            if (stats == null)
            {
                SetSliderValue(0);
            }
            else
            {
                SetSliderValue(stats.hp);
            }

            SetText();
        }

    }
}
