using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene("MainScene",LoadSceneMode.Single);
        }
    }
}
