using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.Sound
{
    public class UISoundEffect : MonoBehaviour
    {
        AudioSource source;

        private void Start()
        {
            source = GetComponent<AudioSource>();

            foreach (Button button in FindObjectsOfType<Button>())
            {
                button.onClick.AddListener(PlaySound);
            }
        }

        void PlaySound()
        {
            source.Play();
        }
    }
}
