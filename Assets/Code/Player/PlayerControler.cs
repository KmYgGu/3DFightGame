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

    
    [SerializeField]private bool isRunning = false;         // �޸��� �� �ȱ⸦ ����
    private bool goRun = false;             // ��� �޸��� ������
    private Vector3 lastInputDirection = Vector3.zero; // ���� ���� ����
    private float lastTapTime = 0f;
    private readonly float doubleTapThreshold = 0.8f; // �� �� �� ���� �ִ� ��� �ð� (��)

    // �÷��̾� ����
    private PlayerStat playerStat;

    private void Start()
    {
        TryGetComponent<CharacterController>(out controller);
        playerAnimtor = gameObject.GetComponentInChildren<Animator>();
        TryGetComponent<PlayerStat>(out playerStat);
        //controller.Move(Vector3.zero); // �ʱ�ȭ�� Move() ȣ��

        //StartCoroutine(HandleRotation()); // �ڷ�ƾ ����
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
        

        // �Է��� ������ �޸��� ���¸� ����
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
            // �޸��� ���� (���� ��)
            Run();
        }

        // �޸��� ���°� �ƴ϶�� �ȱ� ���� ����
        if (!isRunning)
        {
            Walk();
        }

    }

    private float currentAngle = 0.0f; // ���� ȸ�� ����
    private float targetAngle = 0.0f; // ��ǥ ȸ�� ����
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
        // �Է��� ���� ���� �˻�
        if (moveArrow != Vector3.zero)
        {
            // ����Ű�� ���� ������ �������� ó��
            if ((Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))&& (!goRun))
            {
                // ���� �Է� ����� ����, ������ �� ���� ��� �ð��� �Ӱ�ġ �̳��� �޸���� �Ǵ�
                if (moveArrow == lastInputDirection && (Time.time - lastTapTime) < doubleTapThreshold)
                {
                    //Debug.Log("Running!");
                    playerAnimtor.SetBool(animHash_walk, false);
                    playerAnimtor.SetBool(animHash_Run, true);
                    


                    isRunning = true;
                    goRun = true;
                }
                // ���� �Է� ������ ����
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

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("PlayerControler���� ������ �Ǿ��� ");
        //OnGrounded?.Invoke(this);

        //if (controller.isGrounded)

        //transform.rotation = Quaternion.Euler(0, Mathf.Atan2(moveDelta.x, moveDelta.z) * Mathf.Rad2Deg, 0);


    }

    

}
