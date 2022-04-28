using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Character
{
    public class TopUpHP : MonoBehaviour
    {
        CharacterStats stats;

        // Start is called before the first frame update
        void Start()
        {
            stats = GetComponent<CharacterStats>();
        }

        // Update is called once per frame
        void Update()
        {
            if (stats != null)
            {
                stats.ResetHealth();
            }
        }
    }
}
