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
            base.SetStats(newStats);

            SetSliderMax(stats.hpMax);
        }

        public override void Update()
        {
            base.Update();

            SetSliderValue(stats.hp);
            SetText();
        }

    }
}
