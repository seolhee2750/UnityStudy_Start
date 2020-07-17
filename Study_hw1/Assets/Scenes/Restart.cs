using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{ 
    public void ChangeScene()
    {
        SceneManager.LoadScene("study2_start");
    }

    public void ExitScene()
    {
        Application.Quit();
    }
}
