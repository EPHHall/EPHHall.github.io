using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.UI
{
    public class StatMeterActionPoints : StatMeter
    {
        public override void Start()
        {
            base.Start();

            SetSliderMax(stats.actionPoints);
        }

        public override void Update()
        {
            base.Update();

            SetSliderValue(stats.actionPoints);
            SetText();
        }
    }
}
