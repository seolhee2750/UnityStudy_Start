using System.Collections;
using System.Collections.Generic;
using UnityEngine; // MonoBehaviour 클래스를 담고 있는 집합체

public class Test : MonoBehaviour // unity에서 기본 제공하는 함수들을 담는 클래스
{
    private Rigidbody myRigid;

    [SerializeField] // 인스펙터 창에서 카메라의 위치를 참조하도록 하는 코드
    private GameObject go_camera; // 카메라 오브젝트를 의미하는 변수
                                  // 카메라 포지션을 불러오려면 인스펙터 창에서도 카메라를 스크립트 go_camera를 넣어주어야 한다.
    [SerializeField]
    private GameObject sun;

    Vector3 rotation;

    void Start() // 게임을 시작하자마자 최초 1회 실행되는 함수
    {
        // rotation = this.transform.eulerAngles; // 시작할 때 transform의 position의 rotation의 값을 rotation 변수에 넣어준다.
        myRigid = GetComponent<Rigidbody>(); // 시작할 때 Rigidbody의 컴포넌트를 myRigid에 넣어서, 컴포넌트를 제어할 수 있도록 한다.
    }

    // Update is called once per frame
    void Update() // 1초에 한 번씩 갱신되는 함수로, 게임이 실행되는 동안 계속해서 업데이트하며 실행되는 함수
    {
        //transform.RotateAround(sun.transform.position, Vector3.up, 50 * Time.deltaTime);
        // RotateAround는 어떠한 오브젝트를 기준으로 회전하는 함수
        // RotateAround는 세 가지 인자를 필요로 한다. (회전의 기준이 될 포지션, 회전 할 축, 얼마만큼 회전시킬 것인지)
        // (라이트의 위치, y축으로 회전, 1초에 100씩 회전)

        if (Input.GetKey(KeyCode.W)) // W 키를 누르면 앞으로 갈 수 있도록 하는 if문
        {
            //this.transform.position = transform.position + new Vector3(0, 0, 1) * Time.deltaTime;
            // 이 오브젝트의 transform의 position 값 = 이 오브젝트의 transform의 position 값 + (z축 방향으로 1 증가한 새로운 Vector3 * )
            // 보통 1초당 60프레임이 실행되기 때문에, Vector3를 60으로 나눠주지 않으면 1초에 60만큼 z 값이 증가하게 되어 너무 빠르게 이동한다.
            // 따라서 60으로 나눠줘서 1초에 1만큼 씩 이동하도록 하는 방법이 있다.
            // 하지만 컴퓨터마다 실행 환경이 달라서 정확히 1초에 60프레임을 진행한다는 보장이 없다. 따라서 다른 방법을 사용한다.
            // Time.deltaTime은 해당 실행 환경에서 1초당 실행되는 프레임 개수의 역수이다. 따라서 Time.deltaTime을 곱해주면, 해당 컴퓨터의 정확한 1초당 프레임 수로 나누는 것이 된다.

            // 하지만 더 편리한 방법 있음!
            this.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
            // Translate 함수는 원하는 만큼 원하는 곳으로 이동시켜준다.
        }

        else if (Input.GetKey(KeyCode.E)) // E 키를 누르면 오브젝트가 회전할 수 있도록 하는 else if 문
        {
            /*rotation = rotation + new Vector3(90, 0, 0) * Time.deltaTime; // rotation 변수에 회전하는 코드를 써준다.
            this.transform.eulerAngles = rotation;
            // eulerAngles는 오브젝트를 회전시키는 함수이다. 회전 시키는 함수에는 rotation 함수도 있지만, rotation은 4가지 인수를 필요로 하기 때문에 사용하기 어렵다.
            // 반면에 eulerAngles는 인자를 3가지 가지는 Vector3이다. 따라서 유니티에서 회전에 관한 속성을 가지는 transform의 position의 rotation을 의미한다고 볼 수 있다.
            // 때문에, 여기서는 eulerAngles를 사용해 오브젝트를 회전시킨다.
            // But 90도가 넘어가면 값이 이상해지면서 오브젝트가 회전하는데에 문제가 생긴다.
            // 따라서 회전하는 값에 이상한 값이 들어가지 않도록 막아주어야 한다.
            // 때문에 rotation 변수를 사용해야 한다.
            Debug.Log(transform.eulerAngles);
            // Debug는 개발 과정에서 사용하기 좋다. 이를 선언해 놓으면 유니티에서 실행 시 자세한 실행 내역을 확인할 수 있다. */


            // this.transform.Rotate(new Vector3(90, 0, 0) * Time.deltaTime); // 1초에 90도 회전할 수 있게 하는 것
            // 위에서 앞으로 움직이는 코드와 마찬가지로 Rotate 함수를 사용하면 위의 과정 없이도 쉽게 회전시킬 수 있다.
            // eulerAngles를 직접 건드리는 것 보다 이렇게 함수를 사용해서 오브젝트를 움직이는 것이 훨씬 좋다.

            // 하지만 eulerAngles를 사용하거나 Rotate를 사용하면 짐벌락 현상이 생길 수 있다. (한 축이 90도가 되면 이상 현상이 발생하는 경우)
            // 이렇게 짐벌락 현상이 생길 때는 Quaternion을 사용해야 한다. (4가지 인자를 사용!)
            rotation = rotation + new Vector3(90, 0, 0) * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(rotation);
            // 짐벌락 현상이 없을 것 같다면 굳이 Quaternion을 사용할 이유는 없다.
        }
        // 이렇게 rotation 변수를 사용하면, eulerAngles로 바로 값을 받는게 아니라 rotation에서 값을 계속 늘려서 eulerAngles에 넣어주는 형식이 되기 때문에 이상하게 작동되지 않는다.

        else if (Input.GetKey(KeyCode.R))
        {
            this.transform.localScale = this.transform.localScale + new Vector3(2, 2, 2) * Time.deltaTime;
            // 크기를 키우는 코드
        }

        else if (Input.GetKey(KeyCode.T))
        {
            this.transform.localScale = this.transform.localScale + new Vector3(-2, -2, -2) * Time.deltaTime;
            // 크기를 줄이는 코드
        }

        else if (Input.GetKey(KeyCode.S))
        {
            this.transform.LookAt(go_camera.transform.position);
            // go_camera.transform.position : 카메라의 포지션을 가져오는 코드
            // LookAt : ()안의 타겟을 바라보도록 하는 함수
        }

        else if (Input.GetKey(KeyCode.F))
        {
            myRigid.velocity = Vector3.forward; // forward : (0, 0, 1)로, z축으로 가는 코드
        }
    }
}
