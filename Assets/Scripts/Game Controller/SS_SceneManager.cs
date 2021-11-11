using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SS.GameController
{
    public class SS_SceneManager : MonoBehaviour
    {
        public void ChangeScene(int scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
