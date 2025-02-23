using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
   
    // 여기선 캐릭터 점프를 제외한 이동만을 구현
    // 점프할땐 캐릭터 매쉬(metaRig)만 점프

    // 캐릭터 부위 별로 판정 시스템()을 만들고 

    // 스크립트를 하나 만들어서 판정 콜라이더를 별도의 이름으로 관리

    CharacterController controller;

    [SerializeField] private float playermoveSpeed;

    private Vector3 moveDelta;
    Quaternion toRotation;//
    [SerializeField]private bool firstSpin = false;

    private void Start()
    {
        TryGetComponent<CharacterController>(out controller);
        controller.Move(Vector3.zero); // 초기화용 Move() 호출

    }

    private void Update()
    {
        Walk();

    }

    private void Walk()
    {
        moveDelta.x = Input.GetAxis("Horizontal");
        moveDelta.y = 0.0f;
        moveDelta.z = Input.GetAxis("Vertical");

        //moveDelta.x = Input.GetAxisRaw("Horizontal");
        //moveDelta.y = 0.0f;
        //moveDelta.z = Input.GetAxisRaw("Vertical");

        if (moveDelta != Vector3.zero)// 키 입력이 있을 때만
        
        {
            if (!firstSpin)
            {
                //transform.forward = moveDelta;

                toRotation = Quaternion.LookRotation(moveDelta);
                transform.rotation = toRotation;


                firstSpin = true;
            }

            if (moveDelta.sqrMagnitude > 0.1f) // 방향이 충분히 큰 경우에만 회전
            {
                //transform.forward = moveDelta;
                toRotation = Quaternion.LookRotation(moveDelta);
                transform.rotation = toRotation;
            }
            moveDelta.Normalize();
            controller.Move(moveDelta * (playermoveSpeed * Time.deltaTime * moveDelta.sqrMagnitude));// 방향키를 오래 누를 수록 제 속도가 나옴 Mathf.Max(moveDelta.sqrMagnitude, 0.1f)

            //toRotation = Quaternion.LookRotation(moveDelta);
            //transform.rotation = toRotation;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 1); //*Time.deltaTime
        }
        else
        {
            firstSpin = false;
        }
        


    }

    private float AdjustAxisValue(float value)
    {
        if (Mathf.Abs(value - 0) < 0.2f)
        {
            return 0; // 0에 가까우면 0으로 설정
        }
        else if (Mathf.Abs(value - 1) < 0.2f)
        {
            return 1; // 1에 가까우면 1로 설정
        }
        else if (Mathf.Abs(value - 0.5f) < 0.2f)
        {
            return 0.5f; // 0.5에 가까우면 0.5로 설정
        }
        return value; // 그 외의 경우는 원래 값 그대로 사용
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("충돌한 오브젝트: ");
    }

}
