using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
   
    private CharacterController controller;
    [SerializeField] private Animator playerAnimtor;

    private int animHash_walk = Animator.StringToHash("isWalk");

    [SerializeField] private float playermoveSpeed;

    [SerializeField] private Vector3 moveDelta;
    [SerializeField] private Vector3 moveArrow;
    Quaternion toRotation;//
    [SerializeField] private bool firstSpin = false;

    [SerializeField] private Queue<Vector3> moveHistory = new Queue<Vector3>();

    //private Vector3 lastMoveDelta = Vector3.zero; // 이전 방향 저장


    private void Start()
    {
        TryGetComponent<CharacterController>(out controller);
        playerAnimtor = gameObject.GetComponentInChildren<Animator>();

        //controller.Move(Vector3.zero); // 초기화용 Move() 호출

        //StartCoroutine(HandleRotation()); // 코루틴 시작
    }

    private void Update()
    {
        Walk();

    }


    private float currentAngle = 0.0f; // 현재 회전 각도
    private float targetAngle = 0.0f; // 목표 회전 각도


    private void Walk()
    {

        //moveDelta.x = Input.GetAxis("Horizontal");
        //moveDelta.y = 0.0f;
        //moveDelta.z = Input.GetAxis("Vertical");

        moveArrow.x = Input.GetAxisRaw("Horizontal");
        moveArrow.y = 0.0f;
        moveArrow.z = Input.GetAxisRaw("Vertical");

        //  방향키 입력이 있으면 목표 각도 업데이트
        if (moveArrow.x != 0 || moveArrow.z != 0)
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

            moveArrow.Normalize();
            controller.Move(moveArrow * (playermoveSpeed * Time.deltaTime));
        }
        else
        {

        }
            
    }

    

    void notuse()
    {
        if (moveDelta != Vector3.zero)// 키 입력이 있을 때만

        {
            if (!firstSpin)
            {
                transform.forward = moveArrow;

                //toRotation = Quaternion.LookRotation(moveArrow);
                //transform.rotation = toRotation;


                firstSpin = true;
            }

            if (moveDelta.sqrMagnitude > 0.1f) // 방향이 충분히 큰 경우에만 회전
            {
                //transform.forward = moveDelta;

                toRotation = Quaternion.LookRotation(moveDelta);
                transform.rotation = toRotation;
            }
            //moveArrow.Normalize();
            moveDelta.Normalize();
            //controller.Move(moveDelta * (playermoveSpeed * Time.deltaTime * moveDelta.sqrMagnitude));// 방향키를 오래 누를 수록 제 속도가 나옴 Mathf.Max(moveDelta.sqrMagnitude, 0.1f)
            controller.Move(moveArrow * (playermoveSpeed * Time.deltaTime));


            //toRotation = Quaternion.LookRotation(moveDelta);
            //transform.rotation = toRotation;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 1); //*Time.deltaTime
        }
        else
        {
            firstSpin = false;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("PlayerControler에선 측정이 되었음 ");
        //OnGrounded?.Invoke(this);

        //if (controller.isGrounded)

        //transform.rotation = Quaternion.Euler(0, Mathf.Atan2(moveDelta.x, moveDelta.z) * Mathf.Rad2Deg, 0);


    }

}
