using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventMovement : MonoBehaviour
{
    SS.PlayerMovement.SS_PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<SS.PlayerMovement.SS_PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController != null)
        {
            playerController.PauseMovement_ForCutscene();
        }
    }

    private void OnDestroy()
    {
        if (playerController != null)
        {
            playerController.UnPauseMovement_ForCutscene();
        }
    }
}
