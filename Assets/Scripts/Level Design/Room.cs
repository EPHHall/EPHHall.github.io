using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SS.LevelDesign
{
    public class Room : MonoBehaviour
    {
        public bool dontActivate = false;
        public bool displayCurrentlyActive = false;
        private int updatePlayerRange = 0;
        public static Room currentlyActive = null;

        public Vector3 cameraPosition;
        public bool cleared;
        public List<GameObject> toLoad = new List<GameObject>();
        public Transform toLoadParent = null;

        public Camera cam;
        GameObject doors;
        GameObject player;
        SS.GameController.TurnManager turnManager;
        bool firstFrame = true;

        Collider2D col;
        public Tutorial.TutorialHandler tutorialHandler;

        [Space(5)]
        [Header("Rewards")]
        public List<GameObject> rewards = new List<GameObject>();
        public bool rewardsWereSpawned = false;

        [Space(5)]
        [Header("Reset Stuff")]
        public Vector2 playerDefaultPos;
        private List<GameObject> toDestroy = new List<GameObject>();
        public Transform actualParent;
        int preventClearing = 0;
        int suspendUpdate = 0;

        private void Awake()
        {
            cam = Camera.main;

            if (toLoad != null && toLoad.Count == 0)
            {
                toLoad = new List<GameObject>();
                if (toLoadParent == null)
                {
                    toLoadParent = transform.Find("To Load");
                }

                if (toLoadParent != null)
                {
                    foreach (Transform child in toLoadParent.GetComponentsInChildren<Transform>())
                    {
                        toLoad.Add(child.gameObject);
                    }
                }
            }

            doors = transform.Find("Doors").gameObject;
            if (doors != null)
            {
                doors.SetActive(false);
            }

            player = GameObject.FindGameObjectWithTag("Player");

            col = GetComponent<Collider2D>();

            turnManager = FindObjectOfType<SS.GameController.TurnManager>();

            foreach (GameObject unload in toLoad)
            {
                if (unload == null) continue;

                unload.SetActive(false);

                GameObject actualObject = Instantiate(unload, unload.transform.position, Quaternion.identity, actualParent);
                actualObject.name = unload.name;
                toDestroy.Add(actualObject);

                if (this == currentlyActive)
                {
                    actualObject.SetActive(true);
                }
                else
                {
                    actualObject.SetActive(false);
                }
            }

            cameraPosition = transform.position;
            cameraPosition.z = -10;

            foreach (GameObject reward in rewards)
            {
                reward.SetActive(false);
            }

            tutorialHandler = FindObjectOfType<Tutorial.TutorialHandler>();
        }

        public void Reset()
        {
            while (toDestroy.Count > 0)
            {
                GameObject current = toDestroy[0];
                toDestroy.RemoveAt(0);

                if (current.GetComponent<SS.Util.ID>() != null)
                {
                    SS.GameController.DestroyedTracker.instance.TrackDestroyedObject(current.GetComponent<Util.ID>().id);
                }

                Destroy(current);
            }

            Awake();
            if (doors != null)
            {
                doors.SetActive(true);
            }

            player.transform.position = playerDefaultPos;
            player.GetComponent<Character.CharacterStats>().ResetAP();
            player.GetComponent<Character.CharacterStats>().ResetHealth();
            player.GetComponent<Character.CharacterStats>().ResetMana();
            player.GetComponent<PlayerMovement.SS_PlayerController>().pauseMovementForCutscene = false;

            GameController.TurnManager.tm.ResetTurnManager();

            MoveToThisRoom();

            preventClearing = 2;
            suspendUpdate = 2;

            if(tutorialHandler != null)
            {
                //tutorialHandler.
            }
        }

        private void Update()
        {
            if (suspendUpdate > 0)
            {
                suspendUpdate--;

                return;
            }

            if (currentlyActive == this && preventClearing <= 0)
            {
                if (FindObjectOfType<SS.GameController.TurnManager>().turnTakers.Count == 1 && FindObjectOfType<SS.GameController.TurnManager>().turnTakers.Contains(player.GetComponent<SS.GameController.TurnTaker>()))
                {
                    ClearRoom();
                }
            }

            if (cleared && !rewardsWereSpawned)
            {
                rewardsWereSpawned = true;

                for (int i = 0; i < rewards.Count; i++)
                {
                    rewards[i].SetActive(true);
                }
            }

            if (displayCurrentlyActive)
            {
                displayCurrentlyActive = false;

                Debug.Log(currentlyActive.name);
            }

            if (preventClearing > 0) preventClearing--;
        }

        private void LateUpdate()
        {
            if(updatePlayerRange > 0)
            {
                player.GetComponent<SS.PlayerMovement.SS_PlayerMoveRange>().SpawnRange("Room, Late Update");
                updatePlayerRange--;
            }
        }

        public void ClearRoom()
        {
            doors.SetActive(false);
            cleared = true;

            foreach (GameObject creature in toLoad)
            {
                if (creature != null && creature.GetComponent<Character.CharacterStats>() != null && creature.tag != "Player")
                {
                    if (creature.GetComponent<SS.Util.ID>() != null)
                    {
                        if (GameController.DestroyedTracker.instance != null)
                        {
                            SS.GameController.DestroyedTracker.instance.TrackDestroyedObject(creature.GetComponent<SS.Util.ID>().id);
                        }
                    }

                    Destroy(creature.gameObject);
                }
            }
        }
        public void ClearRoom(bool clearCurrentlyActive)
        {
            currentlyActive.ClearRoom();
        }

        public void ActivateRoom()
        {
            currentlyActive = this;

            if (!dontActivate)
            {
                foreach (GameObject load in toDestroy)
                {
                    if (load != null)
                    {
                        load.SetActive(true);
                    }
                }
            }

            if (!cleared && !dontActivate)
            {
                doors.SetActive(true);

                turnManager.SetTurnTakerList();
                player.GetComponent<SS.PlayerMovement.SS_PlayerMoveRange>().ResetMoveRange(player.transform.position, "Room, ActivateRoom");
            }

            updatePlayerRange = 3;
        }

        public void MoveToThisRoom()
        {
            if (this == currentlyActive) return;

            MoveCamera();
            ActivateRoom();
        }

        public void MoveCamera()
        {
            cam.transform.position = cameraPosition;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                MoveToThisRoom();
            }
        }
    }
}
