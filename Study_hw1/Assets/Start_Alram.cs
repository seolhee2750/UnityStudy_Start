using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Alram : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        ChangeScene();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("study2_page2");
    }
}
