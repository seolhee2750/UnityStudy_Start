using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    Rigidbody myRigid;
    [SerializeField]
    private float moveSpeed;

    NavMeshAgent agent;

    [SerializeField]
    private Transform tf_Destination;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            agent.SetDestination(tf_Destination.position);
            myRigid.MovePosition(this.transform.position + transform.forward * moveSpeed * Time.deltaTime);
        }

        /*if (Input.GetKey(KeyCode.S))
        {
            myRigid.MovePosition(this.transform.position + Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            myRigid.MovePosition(this.transform.position + transform.right * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            myRigid.MovePosition(this.transform.position + Vector3.left * moveSpeed * Time.deltaTime);
        }*/

    }
}
