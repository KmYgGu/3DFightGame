using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Transform target;  // 상대 캐릭터의 Transform
    private CharacterController controller;
    [SerializeField]private Animator animator;

    private int animHash_walk = Animator.StringToHash("isWalk");
    private int animHash_Run = Animator.StringToHash("isRun");

    private float MoveDistance = 0.6f; // 감지 거리
    private float EnemymoveSpeed = 1.5f;

    private float RunMultiplier = 2f; // 달리기 시 속도 배율
    private float RunStartDistance = 2f; // 달리기 시작 거리

    [SerializeField]private bool isRunning = false; // 현재 달리기 중인지 확인
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

        float distanceToTarget = Mathf.Sqrt(absX * absX + absZ * absZ); // 2D 거리 계산 (y축 제외)

        if (absX < MoveDistance && absZ < MoveDistance)//목표 거리에 다가옴
        {
            animator.SetBool(animHash_walk, false);
            animator.SetBool(animHash_Run, false);
            isRunning = false;
            ismoveDone = true;
            aIEnemy.ChangedenemyAi(EnemyAIis.canAttack);// 이동 완료후엔 무조건 공격
            return; // 타겟과 너무 가까우면 이동 안 함
        }

        // 달리기 시작 조건
        if (distanceToTarget > RunStartDistance)
        {
            isRunning = true;
        }
                

        Vector3 moveDirection = Vector3.zero;

        if (absX >= MoveDistance && absZ >= MoveDistance)
        {
            // 대각선 이동 (X, Z 둘 다 차이가 MoveDistance 이상)
            moveDirection = new Vector3(Mathf.Sign(deltaX), 0, Mathf.Sign(deltaZ)).normalized;
        }
        else if (absX > absZ)
        {
            // 가로(X) 차이가 더 크면 X축 이동
            moveDirection = new Vector3(Mathf.Sign(deltaX), 0, 0);
        }
        else
        {
            // 세로(Z) 차이가 더 크면 Z축 이동
            moveDirection = new Vector3(0, 0, Mathf.Sign(deltaZ));
        }

        //controller.Move(moveDirection.normalized * EnemymoveSpeed * Time.deltaTime);

        // 이동 속도 결정 (달리기 상태라면 2배 속도)
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

        float distanceToTarget = Mathf.Sqrt(absX * absX + absZ * absZ); // 2D 거리 계산 (y축 제외)

        if (absX < MoveDistance && absZ < MoveDistance)//목표 거리에 다가옴
        {
            animator.SetBool(animHash_walk, false);
            animator.SetBool(animHash_Run, false);
            isRunning = false;
            ismoveDone = true;
            aIEnemy.ChangedenemyAi(EnemyAIis.canAttack);// 이동 완료후엔 무조건 공격

            yield break; // 타겟과 너무 가까우면 이동 안 함
            //yield return null;
        }

        // 달리기 시작 조건
        if (distanceToTarget > RunStartDistance)
        {
            isRunning = true;
        }


        Vector3 moveDirection = Vector3.zero;

        if (absX >= MoveDistance && absZ >= MoveDistance)
        {
            // 대각선 이동 (X, Z 둘 다 차이가 MoveDistance 이상)
            moveDirection = new Vector3(Mathf.Sign(deltaX), 0, Mathf.Sign(deltaZ)).normalized;
        }
        else if (absX > absZ)
        {
            // 가로(X) 차이가 더 크면 X축 이동
            moveDirection = new Vector3(Mathf.Sign(deltaX), 0, 0);
        }
        else
        {
            // 세로(Z) 차이가 더 크면 Z축 이동
            moveDirection = new Vector3(0, 0, Mathf.Sign(deltaZ));
        }

        //controller.Move(moveDirection.normalized * EnemymoveSpeed * Time.deltaTime);

        // 이동 속도 결정 (달리기 상태라면 2배 속도)
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

        // 0벡터면 회전하지 않음
        if (directionToTarget.sqrMagnitude < 0.001f)
            return;

        // 현재 방향과 Z축(전방) 사이의 각도 (x, z 순서 주의)
        float angle = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;
        // 45도 단위로 각도 스냅 (8방향: 0°, 45°, 90°, ... 315°)
        float snappedAngle = Mathf.Round(angle / 45f) * 45f;
        Quaternion targetRotation = Quaternion.Euler(0, snappedAngle, 0);

        // 부드러운 회전을 위해 현재 회전에서 목표 회전으로 일정 속도로 회전
        float turnSpeed = 360f; // 초당 회전 각도 (조절 가능)
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        //transform.rotation = targetRotation;
    }
}
