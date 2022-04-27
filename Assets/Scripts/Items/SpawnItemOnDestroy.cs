using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemOnDestroy : MonoBehaviour
{
    public GameObject toSpawn;

    private void OnDestroy()
    {
        Instantiate(toSpawn).name = toSpawn.name;

        if (GetComponent<SS.Util.ID>() != null)
        {
            SS.GameController.DestroyedTracker.instance.TrackDestroyedObject(GetComponent<SS.Util.ID>().id);
        }
    }
}
