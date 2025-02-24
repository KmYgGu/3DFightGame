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

    [SerializeField] private Vector3 moveDelta;
    [SerializeField] private Vector3 moveArrow;
    Quaternion toRotation;//
    [SerializeField]private bool firstSpin = false;

    //private Vector3 lastMoveDelta = Vector3.zero; // ���� ���� ����


    private void Start()
    {
        TryGetComponent<CharacterController>(out controller);
        //controller.Move(Vector3.zero); // �ʱ�ȭ�� Move() ȣ��

        
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

            yield return null; // ���� �����ӱ��� ���
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
        if (moveDelta != Vector3.zero)// Ű �Է��� ���� ����

        {
            if (!firstSpin)
            {
                transform.forward = moveArrow;

                //toRotation = Quaternion.LookRotation(moveArrow);
                //transform.rotation = toRotation;


                firstSpin = true;
            }

            if (moveDelta.sqrMagnitude > 0.1f) // ������ ����� ū ��쿡�� ȸ��
            {
                //transform.forward = moveDelta;

                toRotation = Quaternion.LookRotation(moveDelta);
                transform.rotation = toRotation;
            }
            //moveArrow.Normalize();
            moveDelta.Normalize();
            //controller.Move(moveDelta * (playermoveSpeed * Time.deltaTime * moveDelta.sqrMagnitude));// ����Ű�� ���� ���� ���� �� �ӵ��� ���� Mathf.Max(moveDelta.sqrMagnitude, 0.1f)
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
        //Debug.Log("PlayerControler���� ������ �Ǿ��� ");
        //OnGrounded?.Invoke(this);

        //if (controller.isGrounded)

        //transform.rotation = Quaternion.Euler(0, Mathf.Atan2(moveDelta.x, moveDelta.z) * Mathf.Rad2Deg, 0);


    }

}
