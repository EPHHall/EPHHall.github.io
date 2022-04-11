using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Animation
{
    [System.Serializable]
    public class AnimationPlusObject
    {
        public AnimationObject anim;
        public string triggerName;
        private Transform trans;
        public bool finished = false;

        public AnimationPlusObject(AnimationObject anim, Transform trans, string triggerName)
        {
            this.anim = anim;
            this.trans = trans;
            this.triggerName = triggerName;
        }

        public void Run(float speed)
        {
            //Debug.Log("AnimationPlusObject.Run");

            if (trans != null)
                anim.transform.position = trans.position;

            finished = false;
            anim.apo = this;
            anim.RunAnimation(triggerName, speed);
        }

        public void Stop()
        {
            //Debug.Log("AnimationPlusObject.Stop");

            anim.transform.position = new Vector2(int.MinValue, int.MinValue);
            finished = true;
        }

        /// <summary>
        /// Returns true if the AnimationPlusObject is identical to the one passed in, apo
        /// </summary>
        /// <param name="apo"></param>
        /// <returns></returns>
        public bool Compare(AnimationPlusObject apo)
        {
            bool result = apo.anim == anim && apo.trans == trans && apo.triggerName == triggerName;
            return result;
        }
    }
}
