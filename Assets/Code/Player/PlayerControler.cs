using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
   
    private CharacterController controller;
    [SerializeField] private Animator playerAnimtor;
    

    private int animHash_walk = Animator.StringToHash("isWalk");
    private int animHash_Run = Animator.StringToHash("isRun");

    [SerializeField] private float playermoveSpeed;

    [SerializeField] private Vector3 moveDelta;
    [SerializeField] private Vector3 moveArrow;
    Quaternion toRotation;//
    [SerializeField] private bool firstSpin = false;

    [SerializeField] private Queue<Vector3> moveHistory = new Queue<Vector3>();

    
    [SerializeField]private bool isRunning = false;         // 달리기 와 걷기를 구분
    private bool goRun = false;             // 계속 달리기 중인지
    private Vector3 lastInputDirection = Vector3.zero; // 이전 방향 저장
    private float lastTapTime = 0f;
    private readonly float doubleTapThreshold = 0.8f; // 두 번 탭 사이 최대 허용 시간 (초)

    // 플레이어 스텟
    private PlayerStat playerStat;

    private void Start()
    {
        TryGetComponent<CharacterController>(out controller);
        playerAnimtor = gameObject.GetComponentInChildren<Animator>();
        TryGetComponent<PlayerStat>(out playerStat);
        //controller.Move(Vector3.zero); // 초기화용 Move() 호출

        //StartCoroutine(HandleRotation()); // 코루틴 시작
    }

    private void Update()
    {
        if (playerStat.aniState == AnimationTag.Idle || playerStat.aniState == AnimationTag.walk || playerStat.aniState == AnimationTag.Jump || playerStat.aniState == AnimationTag.Run || playerStat.aniState == AnimationTag.JumpAttack)
        {
            WalkAndRun();
        }
        

    }

    void WalkAndRun()
    {
        moveArrow = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        

        // 입력이 없으면 달리기 상태를 해제
        if (moveArrow == Vector3.zero)
        {
            isRunning = false;
            goRun = false;
            playerAnimtor.SetBool(animHash_walk, false);
            playerAnimtor.SetBool(animHash_Run, false);

            //EventManager.Instance.TriggerEvent();//idle
        }
        else
        {
            // 달리기 감지 (더블 탭)
            Run();
        }

        // 달리기 상태가 아니라면 걷기 동작 실행
        if (!isRunning)
        {
            Walk();
        }

    }

    private float currentAngle = 0.0f; // 현재 회전 각도
    private float targetAngle = 0.0f; // 목표 회전 각도
    private void Walk()
    {

        //if (moveArrow != Vector3.zero)     
        if (moveArrow.x != 0 || moveArrow.z != 0)
        {
            
            playerAnimtor.SetBool(animHash_walk, true);

            
            //EventManager.Instance.TriggerEvent();//walk

            CharMoveSpin();

            Vector3 normalizedMove = moveArrow.normalized;
            controller.Move(normalizedMove * (playermoveSpeed * Time.deltaTime));
            //controller.Move(new Vector3(normalizedMove.x * (playermoveSpeed * Time.deltaTime), 0, normalizedMove.z * (playermoveSpeed * Time.deltaTime)));
        }    
            
    }


    private void Run()
    {
        // 입력이 있을 때만 검사
        if (moveArrow != Vector3.zero)
        {
            // 방향키가 새로 눌리는 순간에만 처리
            if ((Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))&& (!goRun))
            {
                // 이전 입력 방향과 같고, 마지막 탭 이후 경과 시간이 임계치 이내면 달리기로 판단
                if (moveArrow == lastInputDirection && (Time.time - lastTapTime) < doubleTapThreshold)
                {
                    //Debug.Log("Running!");
                    playerAnimtor.SetBool(animHash_walk, false);
                    playerAnimtor.SetBool(animHash_Run, true);
                    


                    isRunning = true;
                    goRun = true;
                }
                // 현재 입력 정보를 갱신
                lastInputDirection = moveArrow;
                lastTapTime = Time.time;
                               
            }
            else if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && (goRun) && (isRunning))
            {
                CharMoveSpin();

                Vector3 normalizedMove = moveArrow.normalized;
                controller.Move(normalizedMove * (playermoveSpeed *2* Time.deltaTime));
                EventManager.Instance.TriggerEvent();//Run
            }
        }

            
    }

    private void CharMoveSpin()
    {
        float newAngle = Mathf.Atan2(moveArrow.x, moveArrow.z) * Mathf.Rad2Deg;
        targetAngle = Mathf.Round(newAngle / 45.0f) * 45.0f; // 8방향 스냅

        //  현재 각도와 목표 각도가 ±45도 이내면 부드러운 회전
        if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) <= 45f)
        {
            currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * 30);
        }
        else
        {
            //  90도 이상 차이 나면 즉시 회전
            currentAngle = targetAngle;
        }

        //  회전 적용
        transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.up);
        //transform.rotation = Quaternion.Euler(0, currentAngle, 0);

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("PlayerControler에선 측정이 되었음 ");
        //OnGrounded?.Invoke(this);

        //if (controller.isGrounded)

        //transform.rotation = Quaternion.Euler(0, Mathf.Atan2(moveDelta.x, moveDelta.z) * Mathf.Rad2Deg, 0);


    }

    

}
