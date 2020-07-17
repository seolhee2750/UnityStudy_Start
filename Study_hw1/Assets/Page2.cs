using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
 
public class Page2 : MonoBehaviour
{
    /*private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }*/

    NavMeshAgent agent;

    [SerializeField]
    private Transform destination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 10.0f;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, destination.position);

        agent.SetDestination(destination.position);

        if (distance <= 2.0f)
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("study2_page3");
    }
}
