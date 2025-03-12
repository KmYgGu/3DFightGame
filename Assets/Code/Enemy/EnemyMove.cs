using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Transform target;  // ��� ĳ������ Transform
    private CharacterController controller;
    [SerializeField]private Animator animator;

    private int animHash_walk = Animator.StringToHash("isWalk");
    private int animHash_Run = Animator.StringToHash("isRun");

    private float MoveDistance = 0.6f; // ���� �Ÿ�
    private float EnemymoveSpeed = 1.5f;

    private float RunMultiplier = 2f; // �޸��� �� �ӵ� ����
    private float RunStartDistance = 2f; // �޸��� ���� �Ÿ�

    [SerializeField]private bool isRunning = false; // ���� �޸��� ������ Ȯ��
    bool ismoveDone = false;

    AIEnemy aIEnemy;

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent<CharacterController>(out controller);
        animator = GetComponentInChildren<Animator>();
        TryGetComponent<AIEnemy>(out aIEnemy);
    }

    // Update is called once per frame
    
    public void QuickLook()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        directionToTarget.y = 0;
        transform.rotation = Quaternion.LookRotation(directionToTarget);
    }
        
    public IEnumerator EnemyAlMove()
    {
        QuickLook();
        
        //Distance();

        yield return StartCoroutine(Distance());
        LookTarget();

        //yield return new WaitForEndOfFrame();

        StartCoroutine(aIEnemy.AIStart());
    }
    public bool DistanceCheck()
    {
        float deltaX = target.position.x - transform.position.x;
        float deltaZ = target.position.z - transform.position.z;

        float absX = Mathf.Abs(deltaX);
        float absZ = Mathf.Abs(deltaZ);

        return (absX < MoveDistance && absZ < MoveDistance);
    }


    /*void Distance()
    {
        

        float deltaX = target.position.x - transform.position.x;
        float deltaZ = target.position.z - transform.position.z;

        float absX = Mathf.Abs(deltaX);
        float absZ = Mathf.Abs(deltaZ);

        float distanceToTarget = Mathf.Sqrt(absX * absX + absZ * absZ); // 2D �Ÿ� ��� (y�� ����)

        if (absX < MoveDistance && absZ < MoveDistance)//��ǥ �Ÿ��� �ٰ���
        {
            animator.SetBool(animHash_walk, false);
            animator.SetBool(animHash_Run, false);
            isRunning = false;
            ismoveDone = true;
            aIEnemy.ChangedenemyAi(EnemyAIis.canAttack);// �̵� �Ϸ��Ŀ� ������ ����
            return; // Ÿ�ٰ� �ʹ� ������ �̵� �� ��
        }

        // �޸��� ���� ����
        if (distanceToTarget > RunStartDistance)
        {
            isRunning = true;
        }
                

        Vector3 moveDirection = Vector3.zero;

        if (absX >= MoveDistance && absZ >= MoveDistance)
        {
            // �밢�� �̵� (X, Z �� �� ���̰� MoveDistance �̻�)
            moveDirection = new Vector3(Mathf.Sign(deltaX), 0, Mathf.Sign(deltaZ)).normalized;
        }
        else if (absX > absZ)
        {
            // ����(X) ���̰� �� ũ�� X�� �̵�
            moveDirection = new Vector3(Mathf.Sign(deltaX), 0, 0);
        }
        else
        {
            // ����(Z) ���̰� �� ũ�� Z�� �̵�
            moveDirection = new Vector3(0, 0, Mathf.Sign(deltaZ));
        }

        //controller.Move(moveDirection.normalized * EnemymoveSpeed * Time.deltaTime);

        // �̵� �ӵ� ���� (�޸��� ���¶�� 2�� �ӵ�)
        float currentSpeed = isRunning ? EnemymoveSpeed * RunMultiplier : EnemymoveSpeed;

        int AniHash = isRunning ? animHash_Run : animHash_walk;
        //int AniHash2 = !isRunning ? animHash_Run : animHash_walk;
        animator.SetBool(AniHash, true);
        //animator.SetBool(AniHash2, false);

        controller.Move(moveDirection * currentSpeed * Time.deltaTime);

    }*/

    IEnumerator Distance()
    {


        float deltaX = target.position.x - transform.position.x;
        float deltaZ = target.position.z - transform.position.z;

        float absX = Mathf.Abs(deltaX);
        float absZ = Mathf.Abs(deltaZ);

        float distanceToTarget = Mathf.Sqrt(absX * absX + absZ * absZ); // 2D �Ÿ� ��� (y�� ����)

        if (absX < MoveDistance && absZ < MoveDistance)//��ǥ �Ÿ��� �ٰ���
        {
            animator.SetBool(animHash_walk, false);
            animator.SetBool(animHash_Run, false);
            isRunning = false;
            ismoveDone = true;
            aIEnemy.ChangedenemyAi(EnemyAIis.canAttack);// �̵� �Ϸ��Ŀ� ������ ����

            yield break; // Ÿ�ٰ� �ʹ� ������ �̵� �� ��
            //yield return null;
        }

        // �޸��� ���� ����
        if (distanceToTarget > RunStartDistance)
        {
            isRunning = true;
        }


        Vector3 moveDirection = Vector3.zero;

        if (absX >= MoveDistance && absZ >= MoveDistance)
        {
            // �밢�� �̵� (X, Z �� �� ���̰� MoveDistance �̻�)
            moveDirection = new Vector3(Mathf.Sign(deltaX), 0, Mathf.Sign(deltaZ)).normalized;
        }
        else if (absX > absZ)
        {
            // ����(X) ���̰� �� ũ�� X�� �̵�
            moveDirection = new Vector3(Mathf.Sign(deltaX), 0, 0);
        }
        else
        {
            // ����(Z) ���̰� �� ũ�� Z�� �̵�
            moveDirection = new Vector3(0, 0, Mathf.Sign(deltaZ));
        }

        //controller.Move(moveDirection.normalized * EnemymoveSpeed * Time.deltaTime);

        // �̵� �ӵ� ���� (�޸��� ���¶�� 2�� �ӵ�)
        float currentSpeed = isRunning ? EnemymoveSpeed * RunMultiplier : EnemymoveSpeed;

        int AniHash = isRunning ? animHash_Run : animHash_walk;
        //int AniHash2 = !isRunning ? animHash_Run : animHash_walk;
        animator.SetBool(AniHash, true);
        //animator.SetBool(AniHash2, false);

        controller.Move(moveDirection * currentSpeed * Time.deltaTime);

    }

    void LookTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        directionToTarget.y = 0;
        //transform.rotation = Quaternion.LookRotation(directionToTarget);

        // 0���͸� ȸ������ ����
        if (directionToTarget.sqrMagnitude < 0.001f)
            return;

        // ���� ����� Z��(����) ������ ���� (x, z ���� ����)
        float angle = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;
        // 45�� ������ ���� ���� (8����: 0��, 45��, 90��, ... 315��)
        float snappedAngle = Mathf.Round(angle / 45f) * 45f;
        Quaternion targetRotation = Quaternion.Euler(0, snappedAngle, 0);

        // �ε巯�� ȸ���� ���� ���� ȸ������ ��ǥ ȸ������ ���� �ӵ��� ȸ��
        float turnSpeed = 360f; // �ʴ� ȸ�� ���� (���� ����)
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        //transform.rotation = targetRotation;
    }
}
