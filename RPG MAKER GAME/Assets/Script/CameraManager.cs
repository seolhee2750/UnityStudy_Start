using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed; // 카메라가 대상을 얼마나 빠르게 따라갈 것인지
    public Vector3 targetPosition; // 대상의 현재 위치 값


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() // 카메라는 매 프레임 마다 호출이 이뤄져야 하기 때문에 Update 함수에 코드를 작성
    {
        if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            // Vextor3기 때문에 세 개의 값이 필요하다. 
            // z값에 들어가는 this는 camera를 가리킨다. (* this는 이 스크립트가 적용될 객체를 말한다.)
            // 단, this는 생략이 가능하다.
            // 유니티에서 카메라는 z값이 -10으로 설정되어있다. (캐릭터를 비춰야 하는데, 캐릭터와 위치가 같으면 안보이기 때문이다.)

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            // Lerp란 (a, b, t) 라고 했을 때, a에서 b까지 t의 속도로 움직이게 하는 것
            // Time.deltaTime은 1초에 moveSpeed 만큼 이동시키겠다는 의미
        }
    }
}
