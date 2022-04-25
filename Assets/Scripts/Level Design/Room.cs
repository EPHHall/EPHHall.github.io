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

        [Space(5)]
        [Header("Rewards")]
        public List<GameObject> rewards = new List<GameObject>();
        public bool rewardsWereSpawned = false;

        private void Awake()
        {
            cam = Camera.main;

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
                unload.SetActive(false);
            }

            cameraPosition = transform.position;
            cameraPosition.z = -10;

            foreach (GameObject reward in rewards)
            {
                reward.SetActive(false);
            }
        }

        private void Update()
        {
            if (currentlyActive == this)
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
        }

        private void LateUpdate()
        {
            if(updatePlayerRange > 0)
            {
                player.GetComponent<SS.PlayerMovement.SS_PlayerMoveRange>().SpawnRange();
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
                foreach (GameObject load in toLoad)
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
                player.GetComponent<SS.PlayerMovement.SS_PlayerMoveRange>().ResetMoveRange(player.transform.position);
            }

            updatePlayerRange = 3;
        }

        public void MoveToThisRoom()
        {
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
                //Debug.Log("Here", gameObject);

                MoveToThisRoom();
            }
        }
    }
}
