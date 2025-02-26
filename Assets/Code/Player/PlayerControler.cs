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

    //private Vector3 lastMoveDelta = Vector3.zero; // ���� ���� ����


    private void Start()
    {
        TryGetComponent<CharacterController>(out controller);
        playerAnimtor = gameObject.GetComponentInChildren<Animator>();

        //controller.Move(Vector3.zero); // �ʱ�ȭ�� Move() ȣ��

        //StartCoroutine(HandleRotation()); // �ڷ�ƾ ����
    }

    private void Update()
    {
        Walk();

    }


    private float currentAngle = 0.0f; // ���� ȸ�� ����
    private float targetAngle = 0.0f; // ��ǥ ȸ�� ����


    private void Walk()
    {

        //moveDelta.x = Input.GetAxis("Horizontal");
        //moveDelta.y = 0.0f;
        //moveDelta.z = Input.GetAxis("Vertical");

        moveArrow.x = Input.GetAxisRaw("Horizontal");
        moveArrow.y = 0.0f;
        moveArrow.z = Input.GetAxisRaw("Vertical");

        //  ����Ű �Է��� ������ ��ǥ ���� ������Ʈ
        if (moveArrow.x != 0 || moveArrow.z != 0)
        {
            float newAngle = Mathf.Atan2(moveArrow.x, moveArrow.z) * Mathf.Rad2Deg;
            targetAngle = Mathf.Round(newAngle / 45.0f) * 45.0f; // 8���� ����

            //  ���� ������ ��ǥ ������ ��45�� �̳��� �ε巯�� ȸ��
            if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) <= 45f)
            {
                currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * 30);
            }
            else
            {
                //  90�� �̻� ���� ���� ��� ȸ��
                currentAngle = targetAngle;
            }
            //  ȸ�� ����
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
