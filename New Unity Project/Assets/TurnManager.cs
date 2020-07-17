using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    void Start() // 게임이 처음 시작할 때 실행되는 부분
    {
        
    }

    void Update() // 게임이 시작하는 순간부터 종료할 때까지 반복되는 부분
    {
        gameObject.transform.Rotate(0, 1, 0); // 객체의 Transform 속성에서 Rotation 속성을 X축으로 0, Y축으로 1, Z축으로 1만큼 더하는 코드
    }
}
