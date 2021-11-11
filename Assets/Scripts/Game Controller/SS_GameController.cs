using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.UI;

namespace SS.GameController
{
    public class SS_GameController : MonoBehaviour
    {
        public PauseScreen pauseScreen;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePauseScreen();
            }
        }

        public void TogglePauseScreen()
        {
            if (pauseScreen != null)
            {
                pauseScreen.Toggle();
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
