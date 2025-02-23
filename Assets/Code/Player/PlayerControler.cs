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
    [SerializeField]private bool firstSpin = false;

    private void Start()
    {
        TryGetComponent<CharacterController>(out controller);
        controller.Move(Vector3.zero); // �ʱ�ȭ�� Move() ȣ��

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

        if (moveDelta != Vector3.zero)// Ű �Է��� ���� ����
        
        {
            if (!firstSpin)
            {
                //transform.forward = moveDelta;

                toRotation = Quaternion.LookRotation(moveDelta);
                transform.rotation = toRotation;


                firstSpin = true;
            }

            if (moveDelta.sqrMagnitude > 0.1f) // ������ ����� ū ��쿡�� ȸ��
            {
                //transform.forward = moveDelta;
                toRotation = Quaternion.LookRotation(moveDelta);
                transform.rotation = toRotation;
            }
            moveDelta.Normalize();
            controller.Move(moveDelta * (playermoveSpeed * Time.deltaTime * moveDelta.sqrMagnitude));// ����Ű�� ���� ���� ���� �� �ӵ��� ���� Mathf.Max(moveDelta.sqrMagnitude, 0.1f)

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
            return 0; // 0�� ������ 0���� ����
        }
        else if (Mathf.Abs(value - 1) < 0.2f)
        {
            return 1; // 1�� ������ 1�� ����
        }
        else if (Mathf.Abs(value - 0.5f) < 0.2f)
        {
            return 0.5f; // 0.5�� ������ 0.5�� ����
        }
        return value; // �� ���� ���� ���� �� �״�� ���
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("�浹�� ������Ʈ: ");
    }

}
