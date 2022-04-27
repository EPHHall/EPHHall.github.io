using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SS.GameController
{
    public class DestroyedTracker : MonoBehaviour
    {
        public static DestroyedTracker instance;

        [System.Serializable]
        public class IDPlusDelay
        {
            public string destroyedID;
            public int delay; //In Frames

            public IDPlusDelay(string destroyedID, int delay)
            {
                this.destroyedID = destroyedID;
                this.delay = delay;
            }
        }

        public List<IDPlusDelay> destroyedInTheLast2Frames = new List<IDPlusDelay>();

        private void Start()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                if (GetComponent<Util.ID>() != null)
                {
                    instance.TrackDestroyedObject(GetComponent<Util.ID>().id);
                }

                Destroy(gameObject);
            }
        }

        private void Update()
        {
            List<IDPlusDelay> expired = new List<IDPlusDelay>();
            foreach (IDPlusDelay destroyPlusDelay in destroyedInTheLast2Frames)
            {
                destroyPlusDelay.delay--;
                if (destroyPlusDelay.delay <= 0)
                {
                    expired.Add(destroyPlusDelay);
                }
            }

            for (int i = 0; i < expired.Count; i++)
            {
                destroyedInTheLast2Frames.Remove(expired[i]);
            }
        }

        public void TrackDestroyedObject(string id)
        {
            destroyedInTheLast2Frames.Add(new IDPlusDelay(id, 99999999));
        }

        public bool ListContainsID(string id)
        {
            bool result = false;

            foreach(IDPlusDelay idPlusDelay in destroyedInTheLast2Frames)
            {
                if(idPlusDelay.destroyedID != null)
                {
                    result = idPlusDelay.destroyedID == id;
                }

                if (result)
                {
                    break;
                }
            }

            return result;
        }
    }
}
