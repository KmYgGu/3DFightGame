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

    private void Start()
    {
        TryGetComponent<CharacterController>(out controller);
        
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

        moveDelta.Normalize();

        

        if (moveDelta != Vector3.zero)// 키 입력이 있을 때만
        {

            toRotation = Quaternion.LookRotation(moveDelta, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 1); //*Time.deltaTime
            controller.Move(moveDelta * (playermoveSpeed * Time.deltaTime));
        }
    }
}
