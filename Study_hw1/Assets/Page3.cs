using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Page3 : MonoBehaviour
{
    int count = 0;

    private void OnCollisionEnter(Collision collision) // collision에는 충돌한 상태에 대한 정보가 들어간다.
    {
        Debug.Log(collision.gameObject.name);
        soundManager.instance.PlaySound();
        if(collision.gameObject.tag =="ground")
        { 
            count++;
            if(count == 5)
            {
                ChangeScene();
            }
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("study1");
    }
}

