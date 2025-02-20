using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
   
    // ���⼱ ĳ���� ������ ������ �̵����� ����
    // �����Ҷ� ĳ���� �Ž�(metaRig)�� ����

    // ĳ���� ���� ���� ���� �ý���()�� ����� 

    // ��ũ��Ʈ�� �ϳ� ���� ���� �ݶ��̴��� ������ �̸����� ����

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

        

        if (moveDelta != Vector3.zero)// Ű �Է��� ���� ����
        {

            toRotation = Quaternion.LookRotation(moveDelta, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 1); //*Time.deltaTime
            controller.Move(moveDelta * (playermoveSpeed * Time.deltaTime));
        }
    }
}
