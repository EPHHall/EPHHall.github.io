using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.UI;

namespace Tutorial
{
    /*
        The tutorial handler will hold a list of scenes, and the functions required to start those scenes.
    */
    public class TutorialHandler : MonoBehaviour
    {
        public GreyOutObjectScript greyOut;
        public List<TutorialScene> scenes;
        public int currentScene;

        private string buttonID = "";
        private static SS.Spells.Target selectedTarget = null;

        [Header("Debug")]
        public bool startScene = false;
        public bool progressScene = false;

        private void Start()
        {
            currentScene = 0;
        }

        private void Update()
        {
            if (startScene)
            {
                startScene = false;

                StartCurrentScene(-1);
            }
            if (progressScene)
            {
                progressScene = false;

                ProgressCurrentScene();
            }

            if (currentScene < scenes.Count && !scenes[currentScene].GetStarted() && 
                (scenes[currentScene].trigger_MustBeThisTurn == null || scenes[currentScene].trigger_MustBeThisTurn == SS.GameController.TurnManager.currentTurnTaker
                || scenes[currentScene].trigger_StartASAP))
            {
                if (scenes[currentScene].trigger_StartASAP)
                {
                    StartCurrentScene(1);
                }
                if (SS.Spells.Spell.Get_IfCastWasAttempted() && scenes[currentScene].trigger_SpellAttempted)
                {
                    StartCurrentScene(2);
                }
                if (SS.Spells.Spell.Get_IfCastWasSuccessful() && scenes[currentScene].trigger_SpellSuccessfullyCast)
                {
                    StartCurrentScene(3);
                }
                if (buttonID != TutorialScene.NO_BUTTON_ID && scenes[currentScene].trigger_ButtonPressed == buttonID)
                {
                    buttonID = TutorialScene.NO_BUTTON_ID;

                    StartCurrentScene(4);
                }
                if (selectedTarget != null && scenes[currentScene].trigger_TargetWasSelected == selectedTarget)
                {
                    selectedTarget = null;

                    StartCurrentScene(5);
                }
            }
        }

        public void ButtonEventHandler(string buttonID)
        {
            if (buttonID == TutorialScene.NO_BUTTON_ID)
                return;

            this.buttonID = buttonID;
        }

        public static void TargetSelectedEventHandler(SS.Spells.Target target)
        {
            selectedTarget = target;
        }

        public void StartCurrentScene(int calledBy)
        {
            Debug.Log(calledBy);

            if (currentScene >= scenes.Count)
                return;

            scenes[currentScene].StartScene(this);
        }

        public void ProgressCurrentScene()
        {
            if (currentScene >= scenes.Count)
                return;

            scenes[currentScene].ProgressScene();
        }

        public void SceneEnded()
        {
            if (currentScene >= scenes.Count)
                return;

            scenes[currentScene].ResetScene();

            currentScene++;
        }
    }
}

