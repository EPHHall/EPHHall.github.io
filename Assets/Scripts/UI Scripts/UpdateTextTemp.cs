using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.UI
{
    public class UpdateTextTemp : UpdateText
    {
        public bool activated = false;

        public override void Start()
        {
            text = GetComponent<Text>();

            foreach (UpdateText uText in FindObjectsOfType<UpdateText>())
            {
                updateTexts.Add(uText);
            }
        }

        public override void SetMessage(string message)
        {
            base.SetMessage(message);

            activated = true;
        }

        public override void Update()
        {
            base.Update();

            if (text.color.a == 0)
            {
                if (GetComponent<SS.Util.ID>() != null)
                {
                    SS.GameController.DestroyedTracker.instance.TrackDestroyedObject(GetComponent<SS.Util.ID>().id);
                }

                Destroy(gameObject);
            }
        }
    }
}
