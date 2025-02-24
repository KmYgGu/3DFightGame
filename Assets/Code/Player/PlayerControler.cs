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

    [SerializeField] private Vector3 moveDelta;
    [SerializeField] private Vector3 moveArrow;
    Quaternion toRotation;//
    [SerializeField]private bool firstSpin = false;

    //private Vector3 lastMoveDelta = Vector3.zero; // 이전 방향 저장


    private void Start()
    {
        TryGetComponent<CharacterController>(out controller);
        //controller.Move(Vector3.zero); // 초기화용 Move() 호출

        
    }

    private void Update()
    {
        Walk();

    }
    private IEnumerator Walk2()
    {
        while (true)
        {
            /*moveDelta.x = Input.GetAxis("Horizontal");
            moveDelta.y = 0.0f;
            moveDelta.z = Input.GetAxis("Vertical");*/

            moveArrow.x = Input.GetAxisRaw("Horizontal");
            moveArrow.y = 0.0f;
            moveArrow.z = Input.GetAxisRaw("Vertical");

            //if (moveArrow != Vector3.zero)
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                if (!firstSpin)
                {
                    transform.forward = moveArrow;
                    firstSpin = true;
                }

                if (Mathf.Abs(moveArrow.x) == 1f || Mathf.Abs(moveArrow.z) == 1f)
                {
                    //transform.forward = moveArrow;
                    if (Mathf.Abs(moveArrow.x) == 1f || Mathf.Abs(moveArrow.z) == 1f)
                    {
                        toRotation = Quaternion.LookRotation(moveArrow);
                        transform.rotation = toRotation;
                    }
                }

                
                    

                moveArrow.Normalize();
                controller.Move(moveArrow * (playermoveSpeed * Time.deltaTime));
            }
            else
            {
                firstSpin = false;
            }

            yield return null; // 다음 프레임까지 대기
        }
    }

    private void Walk()
    {
        moveDelta.x = Input.GetAxis("Horizontal");
        moveDelta.y = 0.0f;
        moveDelta.z = Input.GetAxis("Vertical");

        moveArrow.x = Input.GetAxisRaw("Horizontal");
        moveArrow.y = 0.0f;
        moveArrow.z = Input.GetAxisRaw("Vertical");

        moveArrow.Normalize();

        if (moveArrow != Vector3.zero)
        {

            if (!firstSpin)
            {
                transform.forward = moveArrow;

                firstSpin = true;
            }

            
            if (moveDelta.sqrMagnitude > 0.1f)
            {

                
                toRotation = Quaternion.LookRotation(moveDelta);
                transform.rotation = toRotation;

            }
            
            moveArrow.Normalize();
            controller.Move(moveArrow * (playermoveSpeed * Time.deltaTime));
        }
        else
        {
            firstSpin = false;
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
