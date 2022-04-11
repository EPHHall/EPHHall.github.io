using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SS.Animation
{
    public class AnimationManager : MonoBehaviour
    {
        public static bool animationsRunning = false;

        public  List<AnimationPlusObject> animationsToPlay = new List<AnimationPlusObject>();
        public  List<AI.Behavior_WaitForAnimations> waitForAnimations = new List<AI.Behavior_WaitForAnimations>();

        [SerializeField]
        private bool run;

        [SerializeField]
        private bool advanceAnimation;

        private float speed;
        [SerializeField]
        private int maxBeforeSpeedUp;
        [SerializeField]
        private float defaultSpeed = 1f;
        [SerializeField]
        private float spedUp1 = 1.3f;


        public void AddAnimation(AnimationPlusObject apo)
        {
            if (apo.anim == null) return;

            bool result = true;
            //foreach (AnimationPlusObject animObj in animationsToPlay)
            //{
            //    if (apo.Compare(animObj))
            //    {
            //        result = false;
            //        break;
            //    }
            //}

            if (result)
            {
                animationsToPlay.Add(apo);
            }
        }
        public void AddAnimationToNextSpot(AnimationPlusObject apo)
        {
            throw new NotImplementedException();
        }

        public void AddWaitFor(AI.Behavior_WaitForAnimations wait)
        {
            //Debug.Log("AnimationManager.AddWaitFor");

            waitForAnimations.Add(wait);
        }

        private void Update()
        {
            if (run)
            {
                if (advanceAnimation)
                {
                    if (animationsToPlay.Count > 0)
                    {
                        animationsToPlay[0].Run(speed);
                        advanceAnimation = false;
                    }
                    else
                    {
                        StopAnimations();
                        return;
                    }
                }
                else
                {
                    if (animationsToPlay[0].finished)
                    {
                        animationsToPlay.RemoveAt(0);
                        advanceAnimation = true;
                    }
                }
            }
        }

        public void RunAnimations()
        {
            //Debug.Log("AnimationManager.RunAnimations");

            run = true;
            animationsRunning = true;
            advanceAnimation = true;
            
            if(animationsToPlay.Count > maxBeforeSpeedUp)
            {
                speed = spedUp1;
            }
            else
            {
                speed = defaultSpeed;
            }
        }
        public void StopAnimations()
        {
            //Debug.Log("AnimationManager.StopAnimations");

            run = false;
            animationsRunning = false;

            //foreach WaitForAnimkation Behavior, tell them that the animations are done
            foreach (AI.Behavior_WaitForAnimations waitForAnimation in waitForAnimations)
            {
                if (waitForAnimation == null) continue;

                waitForAnimation.FinishWaiting();
            }
        }
    }
}
