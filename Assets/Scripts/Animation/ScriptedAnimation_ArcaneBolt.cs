using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Spells;
using SS.Animation;

namespace SS.Animation
{
    public class ScriptedAnimation_ArcaneBolt : MonoBehaviour
    {
        public Vector2 startPoint;
        public Vector2 endPoint;
        public float speed;
        public float acceleration;

        private float defaultSpeed;
        private float t;
        private bool play;
        private Animator animator;
        private Target target;

        private void Start()
        {
            defaultSpeed = speed;
        }

        private void Update()
        {
            if(play && t != 1)
            {
                transform.position = startPoint + (endPoint - startPoint) * t;
                t += Time.deltaTime * speed;
                t = Mathf.Clamp(t, 0, 1);
                speed += acceleration * Time.deltaTime;
            }
            else if(play)
            {
                if (target != null)
                {
                    target.InflictOnAnimationHitDamage();
                }

                if (animator != null)
                {
                    animator.SetTrigger("End");
                    animator = null;
                    play = false;

                    transform.position = new Vector2(int.MinValue, int.MinValue);
                }
            }
        }

        public void StartAnimation()
        {
            play = true;

            t = 0;

            transform.position = startPoint;
        }

        public void SetParams(Animator animator)
        {
            startPoint = SpellManager.caster.position;

            target = SpellManager.currentTargets[0];
            endPoint = target.transform.position;
            speed = defaultSpeed;
            this.animator = animator;
        }
    }
}
