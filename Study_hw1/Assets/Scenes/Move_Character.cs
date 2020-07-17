using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_Character : MonoBehaviour
{
    private int canMove = 0;
    private int CapsuleLight = 0;
     
    private Vector3 target;

    [SerializeField]
    private Light theLight;

    [SerializeField]
    private Light theLight2;

    [SerializeField]
    private Light theLight3;

    [SerializeField] 
    private Light theLight4;

    [SerializeField]
    private Light theLight5;

    [SerializeField]
    private Light theLight6;

    [SerializeField]
    private Light theLight_character;

    private float targetIntensity = 1;
    private float currentIntensity = 0;

    private int twinkle_light = 0;
    private int twinkle_light2 = 0;
    private int twinkle_light3 = 0;

    private Ray ray;
    private RaycastHit hit;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canMove == 0)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        // 클릭 이벤트 //

        if (Physics.Raycast(ray, out hit, 10000.0f))
        {
            if (hit.collider.gameObject.tag == "Cube")
            {
                if (Input.GetMouseButtonUp(0))
                {
                    canMove = 1;
                }
                target = hit.point;
            }

            if (hit.collider.gameObject.tag == "Cube2")
            {
                if (Input.GetMouseButtonUp(0))
                {
                    canMove = 2;
                }
                target = hit.point;
            }

            if (hit.collider.gameObject.tag == "Cube3")
            {
                if (Input.GetMouseButtonUp(0))
                {
                    canMove = 3;
                }
                target = hit.point;
            }

            if (hit.collider.gameObject.tag == "Capsule")
            {
                if (Input.GetMouseButtonUp(0))
                {
                    CapsuleLight = 1;
                }
            }

            if (hit.collider.gameObject.tag == "Capsule2")
            {
                if (Input.GetMouseButtonUp(0))
                {
                    CapsuleLight = 2;
                }
            }

            if (hit.collider.gameObject.tag == "Capsule3")
            {
                if (Input.GetMouseButtonUp(0))
                {
                    CapsuleLight = 3;
                }
            }
        }

        // 이동 및 라이트 이벤트 //

        if (canMove == 1)
        {
            this.transform.LookAt(target);
            animator.SetBool("Run", true);
            transform.position = Vector3.MoveTowards(transform.position, target, 5f * Time.deltaTime);

            theLight.intensity = targetIntensity;

            float distance = Vector3.Distance(transform.position, target);

            if (distance == 0.0f)
            {
                canMove = 0;
                animator.SetBool("Run", false);
                twinkle_light = 1;
            }
        }

        if (canMove == 2)
        {
            this.transform.LookAt(target);
            animator.SetBool("Run", true);
            transform.position = Vector3.MoveTowards(transform.position, target, 5f * Time.deltaTime);

            theLight2.intensity = targetIntensity;

            float distance = Vector3.Distance(transform.position, target);

            if (distance == 0.0f)
            {
                canMove = 0;
                animator.SetBool("Run", false);
                twinkle_light2 = 1;
            }
        }

        if (canMove == 3)
        {
            this.transform.LookAt(target);
            animator.SetBool("Run", true);
            transform.position = Vector3.MoveTowards(transform.position, target, 5f * Time.deltaTime);

            theLight3.intensity = targetIntensity;

            float distance = Vector3.Distance(transform.position, target);

            if (distance == 0.0f)
            {
                canMove = 0;
                animator.SetBool("Run", false);
                twinkle_light3 = 1;
            }
        }

        if (CapsuleLight == 1)
        {
            theLight4.intensity = targetIntensity;
        }

        if (CapsuleLight != 1 | canMove == 1 | canMove == 2 | canMove == 3)
        {
            theLight4.intensity = currentIntensity;
        }

        if (CapsuleLight == 2)
        {
            theLight5.intensity = targetIntensity;
        }

        if (CapsuleLight != 2 | canMove == 1 | canMove == 2 | canMove == 3)
        {
            theLight5.intensity = currentIntensity;
        }

        if (CapsuleLight == 3)
        {
            theLight6.intensity = targetIntensity;
        }

        if (CapsuleLight !=3 | canMove == 1 | canMove == 2 | canMove == 3)
        {
            theLight6.intensity = currentIntensity;
        }

        if (theLight6.intensity == currentIntensity | theLight5.intensity == currentIntensity | theLight4.intensity == currentIntensity)
        {
            CapsuleLight = 0;
        }

        if (theLight.intensity == 1 | theLight2.intensity == 1 | theLight3.intensity ==1)
        {
            theLight_character.intensity = currentIntensity;
        }

        if (twinkle_light == 1 && twinkle_light2 == 1 && twinkle_light3 == 1)
        {
            StartCoroutine(Twinkle());            
        }
    }            

    IEnumerator Twinkle()
    {
        theLight.intensity = currentIntensity;
        theLight2.intensity = currentIntensity;
        theLight3.intensity = currentIntensity;
        yield return new WaitForSeconds(0.2f);
        theLight.intensity = targetIntensity;
        theLight2.intensity = targetIntensity;
        theLight3.intensity = targetIntensity;
        yield return new WaitForSeconds(0.2f);
        theLight.intensity = currentIntensity;
        theLight2.intensity = currentIntensity;
        theLight3.intensity = currentIntensity;
        yield return new WaitForSeconds(0.2f);
        theLight.intensity = targetIntensity;
        theLight2.intensity = targetIntensity;
        theLight3.intensity = targetIntensity;
        yield return new WaitForSeconds(0.2f);
        theLight.intensity = currentIntensity;
        theLight2.intensity = currentIntensity;
        theLight3.intensity = currentIntensity;
        yield return new WaitForSeconds(0.2f);
        theLight.intensity = targetIntensity;
        theLight2.intensity = targetIntensity;
        theLight3.intensity = targetIntensity;
        yield return new WaitForSeconds(0.2f);
        theLight.intensity = currentIntensity;
        theLight2.intensity = currentIntensity;
        theLight3.intensity = currentIntensity;
        yield return new WaitForSeconds(0.2f);
        theLight.intensity = targetIntensity;
        theLight2.intensity = targetIntensity;
        theLight3.intensity = targetIntensity;
        yield return new WaitForSeconds(0.2f);
        theLight.intensity = currentIntensity;
        theLight2.intensity = currentIntensity;
        theLight3.intensity = currentIntensity;
        yield return new WaitForSeconds(0.2f);
        theLight.intensity = targetIntensity;
        theLight2.intensity = targetIntensity;
        theLight3.intensity = targetIntensity;
        yield return new WaitForSeconds(0.2f);
        theLight.intensity = currentIntensity;
        theLight2.intensity = currentIntensity;
        theLight3.intensity = currentIntensity;
        yield return new WaitForSeconds(0.2f);
        ChangeScene();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("study2_restart");
    }
}