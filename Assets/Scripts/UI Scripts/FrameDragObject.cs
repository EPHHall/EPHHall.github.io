using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.UI
{
    public class FrameDragObject : MonoBehaviour
    {
        private MagicFrame currentFrame;
        public MagicFrame createdFrom;

        private void LateUpdate()
        {
            if (currentFrame != null)
            {
                currentFrame.Highlight();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (createdFrom is EffectFrame && collision.GetComponent<EffectFrame>() != null)
            {
                currentFrame = collision.GetComponent<MagicFrame>();
            }
            else if (createdFrom is ModifierFrame && collision.GetComponent<ModifierFrame>() != null)
            {
                currentFrame = collision.GetComponent<MagicFrame>();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<MagicFrame>() != null && currentFrame == collision.GetComponent<MagicFrame>())
            {
                currentFrame = null;
            }
        }

        public void SetIcon()
        {
            GetComponent<Image>().sprite = createdFrom.image.sprite;
        }

        public void FillCurrentFrame()
        {
            if (currentFrame != null)
            {
                currentFrame.SetContent(createdFrom.content);
            }
        }
    }
}
