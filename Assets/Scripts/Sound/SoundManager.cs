using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Sound
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource source;

        public AudioClip inflameSound;
        public AudioClip enchantSound;

        private void Start()
        {
            source = GetComponent<AudioSource>();
        }
    }
}
