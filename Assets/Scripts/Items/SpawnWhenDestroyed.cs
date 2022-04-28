using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWhenDestroyed : MonoBehaviour
{
    public GameObject toSpawn;

    private void OnDestroy()
    {
        toSpawn.SetActive(true);
    }
}
