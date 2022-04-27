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
        public static GameObject screenActivated;
        public static SS.UI.MagicFrame frameAddedTo;
        public static SS.Util.EventBool rangeWasShown;
        public static bool altarWasActivated;
        public static bool switchWasActivated;

        [Header("Debug")]
        public bool startScene = false;
        public bool progressScene = false;

        private void Start()
        {
            currentScene = 0;

            rangeWasShown = GameObject.Find("Enemy Range Shown?").GetComponent<SS.Util.EventBool>();

            scenes.Clear();
            foreach (TutorialScene scene in transform.GetComponentsInChildren<TutorialScene>())
            {
                scenes.Add(scene);
            }
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

            if (currentScene < scenes.Count && scenes[currentScene].GetStarted())
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ProgressCurrentScene();
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    RegressCurrentScene();
                }
            }

            if (currentScene < scenes.Count && !scenes[currentScene].GetStarted() && 
                (scenes[currentScene].trigger_MustBeThisTurn == null || scenes[currentScene].trigger_MustBeThisTurn == SS.GameController.TurnManager.currentTurnTaker
                || scenes[currentScene].trigger_StartASAP))
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    StartPreviousScene();
                }

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
                if (scenes[currentScene].trigger_Destroyed == null && scenes[currentScene].objectWasNotNull)
                {
                    StartCurrentScene(6);
                }
                if (Input.GetKeyDown(scenes[currentScene].trigger_KeyPress))
                {
                    StartCurrentScene(7);
                }
                if (screenActivated != null && scenes[currentScene].trigger_ScreenActivated == screenActivated)
                {
                    screenActivated = null;
                    StartCurrentScene(8);
                }
                if (frameAddedTo != null && scenes[currentScene].trigger_FrameAddedTo == frameAddedTo)
                {
                    frameAddedTo = null;
                    StartCurrentScene(9);
                }
                if (scenes[currentScene].trigger_RangeWasShown && rangeWasShown.Get())
                {
                    rangeWasShown.Set(false);
                    StartCurrentScene(10);
                }
                if (scenes[currentScene].trigger_AltarActivation && altarWasActivated)
                {
                    altarWasActivated = false;
                    StartCurrentScene(11);
                }
                if (scenes[currentScene].trigger_FramesToFill.Count > 0)
                {
                    int numberFilled = 0;
                    foreach (MagicFrame frame in scenes[currentScene].trigger_FramesToFill)
                    {
                        if (frame.content != null)
                        {
                            numberFilled++;
                        }
                    }

                    if(numberFilled >= scenes[currentScene].numberOfFramesMustBeFilled)
                    {
                        StartCurrentScene(12);
                    }
                }
                if (scenes[currentScene].trigger_SwitchActivation && switchWasActivated)
                {
                    switchWasActivated = false;
                    StartCurrentScene(13);
                }
                if (scenes[currentScene].trigger_ObjectsToBeDestroyed.Count > 0)
                {
                    int numberDestroyed = 0;
                    foreach (GameObject obj in scenes[currentScene].trigger_ObjectsToBeDestroyed)
                    {
                        if (obj == null)
                        {
                            numberDestroyed++;
                        }
                    }

                    if (numberDestroyed >= scenes[currentScene].numberOfObjectsMustBeDestroyed)
                    {
                        StartCurrentScene(14);
                    }
                }
                if (scenes[currentScene].trigger_TargetWithIDSelected != "" && selectedTarget != null && selectedTarget.id == scenes[currentScene].trigger_TargetWithIDSelected)
                {
                    selectedTarget = null;

                    StartCurrentScene(15);
                }
                if (scenes[currentScene].trigger_TargetWithIDDestroyed != "")
                {
                    if (SS.GameController.DestroyedTracker.instance.ListContainsID(scenes[currentScene].trigger_TargetWithIDDestroyed))
                    {
                        StartCurrentScene(16);
                    }
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
            //Debug.Log(calledBy);

            if (currentScene >= scenes.Count)
                return;

            scenes[currentScene].StartScene(this);
        }

        public void StartPreviousScene()
        {
            Debug.Log("1. Current Scene = " + currentScene);
            currentScene--;
            Debug.Log("2. Current Scene = " + currentScene);

            if (currentScene < 0)
            {
                currentScene++; 
            Debug.Log("3. Current Scene = " + currentScene);
                return;
            }

            scenes[currentScene].StartScene(this);
        }

        public void ProgressCurrentScene()
        {
            if (currentScene >= scenes.Count)
                return;

            scenes[currentScene].ProgressScene();
        }

        public void RegressCurrentScene()
        {
            if (currentScene < 0)
                return;

            scenes[currentScene].RegressScene();
        }

        public void SceneEnded()
        {
            SceneEnded(true);
        }
        public void SceneEnded(bool increaseSceneCount)
        {
            if (currentScene >= scenes.Count)
                return;

            scenes[currentScene].ResetScene();

            if(increaseSceneCount)
                currentScene++;
        }

        public void ResetFlags()
        {
            buttonID = TutorialScene.NO_BUTTON_ID;
            selectedTarget = null;
            frameAddedTo = null;
            screenActivated = null;
            altarWasActivated = false;
        }
    }
}

