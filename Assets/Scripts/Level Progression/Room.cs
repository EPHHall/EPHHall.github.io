using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SS.LevelProgression
{
    public class Room : MonoBehaviour
    {
        public bool displayCurrentlyActive = false;
        public static Room currentlyActive = null;

        public Vector3 cameraPosition;
        public bool cleared;
        public List<GameObject> toLoad = new List<GameObject>();

        public Camera cam;
        GameObject doors;
        GameObject player;
        SS.GameController.TurnManager turnManager;
        bool firstFrame = true;

        Collider2D col;

        private void Start()
        {
            cam = Camera.main;

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

            if (displayCurrentlyActive)
            {
                displayCurrentlyActive = false;

                Debug.Log(currentlyActive.name);
            }
        }

        public void ClearRoom()
        {
            doors.SetActive(false);
            cleared = true;
        }

        public void ActivateRoom()
        {
            if (!cleared)
            {
                doors.SetActive(true);
                currentlyActive = this;

                foreach (GameObject load in toLoad)
                {
                    if (load != null)
                    {
                        load.SetActive(true);
                    }
                }

                turnManager.SetTurnTakerList();
                player.GetComponent<SS.PlayerMovement.SS_PlayerMoveRange>().ResetMoveRange();
            }
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
