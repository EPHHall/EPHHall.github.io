//This script executes after default time so that scripts can have a chance to read its value before it gets reset.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Util
{
    public class EventBool : MonoBehaviour
    {
        private const int RESET_TIMER = 1;

        [SerializeField]
        private bool value;

        [SerializeField]
        private int reset = -1;

        private void LateUpdate()
        {
            if (reset == RESET_TIMER)
            {
                value = false;
                reset = -1;
            }

            if (reset != -1)
            {
                reset += 1;
            }
        }

        public void Set(bool newBool)
        {
            value = newBool;

            if (value)
                reset = 0;
            else
                reset = -1;
        }

        public bool Get()
        {
            return value;
        }

        public bool Compare(EventBool b)
        {
            return value && b.Get();
        }
    }
}
