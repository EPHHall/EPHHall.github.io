using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SS.Animation
{
    public class AnimationObject : MonoBehaviour
    {
        private Animator animator;
        public AnimationPlusObject apo;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void RunAnimation(string triggerName, float speed)
        {
            Debug.Log("In RunAnimation()", animator.gameObject);

            animator.speed = speed;
            animator.GetBehaviour<SetDoneFlags>().animObject = this;
            animator.SetTrigger(triggerName);
        }

        public void FinishAnimation()
        {
            //Debug.Log("ANimationObject.FinishAnimation");
            apo.Stop();
        }

        public Animator GetAnimator()
        {
            return animator;
        }
    }
}
