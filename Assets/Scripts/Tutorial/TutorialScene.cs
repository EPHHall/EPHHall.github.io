using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Util;
using SS.GameController;

namespace Tutorial
{
    /*
    Each tutorial "Scene" will hold a list of tutorial steps, and will provide functions for progressing to later steps
    */
    public class TutorialScene : MonoBehaviour
    {
        public const string NO_BUTTON_ID = "NO ID";

        public TutorialHandler handler;
        [Space(5)]

        public List<TutorialStep> steps;
        public int currentStep;

        private bool started;
        private bool ended;

        public TurnTaker trigger_MustBeThisTurn;
        public bool trigger_StartASAP;
        public bool trigger_SpellAttempted;
        public bool trigger_SpellSuccessfullyCast;
        public string trigger_ButtonPressed = NO_BUTTON_ID;
        public SS.Spells.Target trigger_TargetWasSelected = null;
        public GameObject trigger_Destroyed; [System.NonSerialized] public bool objectWasNotNull;
        public KeyCode trigger_KeyPress = KeyCode.None;
        public GameObject trigger_ScreenActivated = null;
        public SS.UI.MagicFrame trigger_FrameAddedTo = null;
        public bool trigger_RangeWasShown;

        private void Start()
        {
            if (trigger_ButtonPressed == "")
            {
                trigger_ButtonPressed = NO_BUTTON_ID;
            }

            objectWasNotNull = trigger_Destroyed != null;

            steps.Clear();
            foreach (TutorialStep step in transform.GetComponentsInChildren<TutorialStep>())
            {
                steps.Add(step);
            }
        }

        public void StartScene(TutorialHandler handledBy)
        {
            handler = handledBy;

            started = true;
            ended = false;
            currentStep = 0;
            steps[currentStep].StartStep(this);
        }

        public void ProgressScene()
        {
            steps[currentStep].EndStep();

            handler.ResetFlags();

            currentStep++;

            if (currentStep >= steps.Count)
            {
                EndScene();
                return;

            }

            steps[currentStep].StartStep(this);
        }

        public void EndScene()
        {
            ended = true;
            handler.SceneEnded();
        }

        public bool GetEnded()
        {
            return ended;
        }
        public bool GetStarted()
        {
            return started;
        }

        public void ResetScene()
        {
            started = false;
            ended = false;
        }
    }
}
