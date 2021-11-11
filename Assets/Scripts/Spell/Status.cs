using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Spells
{
    [System.Serializable]
    public class Status
    {
        public string status;
        public int magnitude;

        public Status(string status, int magnitude)
        {
            this.status = status;
            this.magnitude = magnitude;
        }
    }
}
