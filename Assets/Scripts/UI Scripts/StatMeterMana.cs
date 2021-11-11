using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.UI
{
    public class StatMeterMana : StatMeter
    {
        public override void Start()
        {
            base.Start();

            SetSliderMax(stats.mana);
        }

        public override void Update()
        {
            base.Update();

            SetSliderValue(stats.mana);
            SetText();
        }
    }
}
