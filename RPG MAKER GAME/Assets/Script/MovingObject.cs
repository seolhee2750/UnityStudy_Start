using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public LayerMask layerMask; // 통과가 불가능한 레이어를 만들어 준다.

    public float speed = 0; // 캐릭터가 움직이는 속도를 정하기 위함
    private Vector3 vector; // Vextor3는 x, y, z 축에 대한 값을 한 번에 가진다.
                            // 쉽게 말해 캐릭터의 x, y, z 축을 담는다.

    public float runSpeed;
    private float applyRunSpeed;
    // Shift 키를 눌렀을 때 빨리 달리는 것을 구현하기 위함

    public int WalkCount; // 1초 당 가는 거리를 표현하는 변수
    private int currentWalkCount;
    // speed가 2.4이고 WalkCount가 20이라고 치면, (2.4 * 20 = 48)이기 때문에 한 번 방향키를 누를 때마다 48픽셀 씩 움직이도록 하기 위함.
    // 결론. 움직일 때 픽셀 단위로 움직이지 않고 한 단위 씩 움직이게 하기 위함.
    // 하지만 매번 곱해주는 것은 귀찮기 때문에 while 문 사용. while 문에서 빠져나가려면 currentWalkCount 사용

    private bool canMove = true;
    // couroutine이 실행 속도가 빨라서 방향키를 누르면 한 번에 몇 칸씩 쭉 가는 것을 방지하기 위함
    // 일단 무조건 실행되어야 하기 때문에 true로 값을 설정
    // 추 후에 false로 바꾸고, 또 true로 바꿔가면서 실행

    private bool applyRunFlag = false; // 일단 false로 초기화

    private Animator animator; // 애니메이션을 가져오기 위한 선언

    void Start()
    { 
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>(); // boxCollider 변수에 boxCollide2D의 속성을 가져와서 초기화하는 함수.
                                                     // GetComponent : 해당 속성을 불러오는 함수
    }
    // 유니티에서 처음 Animator를 생성했을 때 자동으로 캐릭터에 Animator 컴포넌트가 생성된다.
    // 이 함수는 animator 변수로 Animator 컴포넌트를 제어하기 위한 함수이다.

    IEnumerator MoveCoroutine() // 연산이 너무 빠른 탓에 캐릭터가 스르륵 움직이지 않고 한 번에 휙 가버리는 것을 방지하기 위해 wait를 걸어줘야 한다. 
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) // 이 while 문으로 한 번 더 감싸주면
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed; // shift 키를 누르면 applyRunSpeed에 runSpeed 값을 넣는다. (runSpeed 값은 유니티에서 지정한 빠르기)
                applyRunFlag = true; // shift가 눌릴 경우 true로 바뀔 수 있도록 한다.
            }
            else
            {
                applyRunSpeed = 0; // shift 키를 누르지 않았을 때 0을 대입해서 속도의 변화가 없게 함
                applyRunFlag = false; // shift 키가 눌리지 않았을 경우 false
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z); // vector에 값 넣어주기
                                                                                                            // transform.position.z 대신 0을 입력해도 되지만, 어차피 2D라서 Z값은 변하지 않기 때문에 Z값을 가져오는 코드를 써도 상관없다.
                                                                                                            // x 좌표가 수평이라 Horizontal의 Input 값을 사용, y 좌표는 수직이라 Vertical의 Input 값을 사용한다."

            if (vector.x != 0)
                vector.y = 0;
            // vector.x의 값이 -1 혹은 1일 경우 == 위 혹은 아래로 움직인다는 뜻
            // * 결론. 수직 값과 수평 값을 동시에 받아들이지 않도록 한다.

            animator.SetFloat("DirX", vector.x); // 유니티에서 파라미터에서 x값이 -1이 되면 왼쪽으로 가도록 설정했음.
                                                 // 이 x 값을 vector.x로 받아서 파라미터에서 설정한 DirX로 넣어준다.
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;
            // 만약 A지점, B지점이 있다고 했을 때 A지점에서 B지점 쪽으로 레이저를 쏜다고 해보자.
            // A지점에서 무사히 B지점까지 레이저가 도착했을 떄는 hit 값으로 Null 값이 리턴된다.
            // 하지만 중간에 방해물에 걸려 도착하지 못했을 경우에는 그 방해물이 리턴된다.
            
            Vector2 start = transform.position; // A 지점, 캐릭터의 현재 위치 값
            Vector2 end = start + new Vector2(vector.x * speed * WalkCount, vector.y * speed * WalkCount); // B 지점, 캐릭터가 이동하고자 하는 위치 값

            boxCollider.enabled = false; // 박스 컬라이더가 캐릭터의 몸에 있기 때문에, 캐릭터가 바로 박스에 부딪히는 것 같은 상황이 생길 수 있다.
                                         // 따라서 이를 방지하기 위해 잠깐 박스컬라이더를 못쓰게 해준다.
            hit = Physics2D.Linecast(start, end, layerMask); // 위에서 예를 들었던 경우처럼 생각해보면, 실제로 레이저를 쏘는 경우를 얘기한다.
            boxCollider.enabled = true; // 다시 원상복귀

            if (hit.transform != null)
                break;
            // 레이저를 쐈을 때 어딘가에 부딪혔다면 그 이후는 실행하지 않겠다는 의미
            // 만약 부딪히지 않았다면 계속해서 그 다음 실행을 하게 된다.

            animator.SetBool("Walking", true); // 멈춰있던 Standing tree에서 Walking tree로 상태 전이가 일어난다.
                                               // 유니티에서 파라미터에서 설정한 Walking 값이 Walking tree에서는 true, Standing tree에서는 false로 설정했기 때문이다.

            while (currentWalkCount < WalkCount)
            {
                if (vector.x != 0) // 좌 우 로의 움직임을 관리하는 if문
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0); // transform.traslate은 기존의 값에 적힌 값을 더하는 함수
                                                                                   // speed 값에 applyRunSpeed를 더하는 식으로 해서 shift를 눌렀을 경우 지정한 runSpeed 값이 더해지게 되고,
                                                                                   // shift를 누르지 않았을 경우 0이 더해지게 된다.
                }
                else if (vector.y != 0) // 상 하 로의 움직임을 관리하는 if문
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }

                if (applyRunFlag)
                    currentWalkCount++; // shift 키가 눌리면 여기서 currentWalkCount가 1씩 증가하게 된다. (shift가 눌렸을 때 ture로 바뀌도록 했기 때문.)
                                        // * 결론. shift 키가 눌리면 이 if문에서 한 번, 그리고 아래 currentWalkCount**에서 한 번씩 증가하게 되어 총 2가 증가하게 된다.

                currentWalkCount++; // 한 회차 돌 때마다 1씩 증가시킨다.
                                    // currentWalkCount가 1씩 증가하면서 20이 될 경우 이 반복문에서 빠져나간다.
                                    // 결론. 48 픽셀 만큼 이동한 후 그 다음부터 2.4를 추가로 이동하지 않아도 된다.

                yield return new WaitForSeconds(0.01f); // coroutine의 기본 연산. 0.01초 동안 대기하게 한다.
                                                        // 이 코드를 반복문 안으로 넣어서, currentWalkCount가 20이 될 때마다 0.01초 씩 기다릴 수 있도록 한다.
                                                        // 따라서 2.4픽셀 씩 움직이는 모습이 보이게 된다.
            }
            currentWalkCount = 0; // currentWalkCount가 20이 되어 반복문에서 빠져나오면
                                  // 다시 0으로 만들어 주어 다시 반복할 수 있게 한다.
                                  // * But 이렇게만 하면 문제가 생긴다. 방향키를 누를 때마다 바로 48픽셀 순간 이동을 하게 된다.
                                  // 스르륵 이동하는게 아니라 뿅 하고 이동하는 느낌.
                                  // 원래는 2.4씩 움직여서 한번에 48픽셀을 이동하는 것인데, 연산이 너무 빨라서 2.4씩 이동하는 것은 보이지 않고 48픽셀 만큼 움직이는 것만 보이는 것.
        }
        animator.SetBool("Walking", false); // 반복문이 끝나면 다시 서 있는 모션으로 바꿔준다.
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) // Horizontal과 Vertical은 대소문자 구분 필수
            // 방향키의 값을 받는 코드
            // Input.GetAxisRaw("Horizontal") 일 때
            // 우 방향키가 눌리면 1을 리턴, 좌 방향키가 눌리면 -1 리턴
            // Input.GetAxisRaw("Vertical") 일 때
            // 상 방향키가 눌리면 1을 리턴, 하 방향키가 눌리면 -1 리턴
            // 결국은 방향키가 눌리지 않았을 때를 의미하는 코드이다.
            {
                canMove = false; // 이 구간이 두 번 연속 실행되는 것을 막아준다.
                                 // 방향키를 누르는 순간 canMove가 flase가 되고, 다시 coroutine에서 true가 되어 실행 할 수 있도록 한다.
                StartCoroutine(MoveCoroutine()); // coroutine을 실행시키는 명령어
                                                 // * But 이렇게만 하면 방향키를 누를 때 coroutine이 너무 빠르게 실행돼서 한 번에 여러번 coroutine이 실행되어 버린다.
                                                 // 이 문제점을 막기 위해 canMove를 만든다. canMove가 true일 때만 실행될 수 있도록 하는 것.
            }
            // 이렇게 함수가 실행되면, coroutine이 계속해서 실행된다.
            // 따라서 캐릭터가 한 번 걷고 멈추고 다시 걷는 식으로 걸음이 이어지기 때문에 
            // 키를 연속해서 누르지 않고 여러번 클릭해서 움직일 경우, 한 다리만 움직이는 식으로 되어버린다. 
            // * 이를 해결하기 위해서 coroutine 안의 실행문들을 while문으로 한 번 더 감싸준다.
            // * 그렇게 되면 coroutine은 한 번만 실행되고, 한 번 실행되면 그 안에서 계속 돌아가게 된다.
        }
    }
}
