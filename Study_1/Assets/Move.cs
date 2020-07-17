using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour // 스크립트 이름
{

    float speed = 10.0f; // 속도 생성. float라서 f를 붙인다.

    // Start is called before the first frame update
    void Start() // 초기 설정 (오브젝트가 생성이 되면서 제일 먼저 불러지는 값들. 초기화도 여기서!!)
    {
        
    }

    // Update is called once per frame
    void Update() // 1초당 60프레임 정도 --> 1초에 60번 업데이트되는 것!
    {
        float objectmove = speed * Time.deltaTime;
        float key = Input.GetAxis("Horizontal"); // 수평은 -1~1 값. --> -1은 왼쪽 1은 오른쪽
        transform.Translate(Vector3.right * objectmove * key, Space.World); // Space.World : World에서 보는 좌표로, 상대적인 좌표가 아닌 절대적인 좌표
    }

    void Awake() // 자동으로 생성되는 것은 아니지만, start 보다 더 빨리 실행되는 함수이다. 꽤 많이 사용.
    {
        
    }

    private void FixedUpdate() // 물리 효과에 관련된 고정 프레임 계산에 쓰이는 함수이다. 자동으로 생성되진 않는다.
    {
        
    }
}
