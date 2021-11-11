using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjects : MonoBehaviour
{
    public List<GameObject> gameObjects;

    private void Update()
    {
        foreach (GameObject gameObject in gameObjects)
        {

            gameObject.SetActive(true);
        }
    }
}
