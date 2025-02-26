using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Queue<float> attackQueue = new Queue<float>(); // 공격 시간을 저장할 큐
    float pressStartTime = 0f;
    bool isHolding = false;

    private const int maxAttacks = 4; // 최대 4번 공격 가능
    private float lastAttackTime = 0f; // 마지막 공격 시간 기록
    private const float resetTime = 3f; // 3초 후 초기화     나중에 애니메이션 프레임에 따라 시간을 조정

    private const float maxHoldTime = 2f; // 누른 시간이 2초 초과 시 강제 해제


    private Animator CharAni;
    private int animHash_Attack1 = Animator.StringToHash("isAttack1");
    private int animHash_Attack2 = Animator.StringToHash("isAttack2");
    private int animHash_Attack3 = Animator.StringToHash("isAttack3");
    private int animHash_Attack4 = Animator.StringToHash("isAttack4");

    [SerializeField]private Animation[] Attackanis;// 공격 애니메이션 별 종료시간을 참고하기 위함

    private void Start()
    {
        TryGetComponent<Animator>(out CharAni);
    }
    
    void Update()
    {

        HandleInput();
        CheckResetTimer();
    }

    void HandleInput()// 콤보 공격
    {
        if (Input.GetButtonDown("Fire1") && attackQueue.Count < maxAttacks)
        {
            isHolding = true;
            pressStartTime = Time.time;
        }

        if (Input.GetButtonUp("Fire1") && attackQueue.Count < maxAttacks && isHolding)// 
        {
            float heldDuration = Time.time - pressStartTime;
            attackQueue.Enqueue(heldDuration); // 공격 큐에 추가
            lastAttackTime = Time.time; // 마지막 공격 시간 갱신
            ProcessAttackQueue();
            //CheckResetTimer();
            isHolding = false;
        }

        if(attackQueue.Count >= maxAttacks)
        {
            Debug.Log(" 공격을 최대 횟수만틈 시도함");
            ResetAttacks();
        }

        if (Time.time - pressStartTime >= maxHoldTime && isHolding)//isHolding && 
        {
            isHolding = false;
            ForceReleaseFire(); // 2초 초과 시 강제 해제
        }
    }

    void CheckResetTimer()// 공격 키를 누른 후, 제한 시간 안에 다음 공격을 날리지 않으면 콤보를 초기화
    {
        
        if (attackQueue.Count > 0 && Time.time - lastAttackTime >= resetTime)//attackQueue.Count > 0 && 
        {
            Debug.Log(" 3초 동안 추가 공격 없음");
            ResetAttacks();
        }
    }

    void ProcessAttackQueue()// 몇 번째 공격을 몇 초간 눌렀는지 재생
    {
        if (attackQueue.Count > 0)
        {
            int attackNumber = attackQueue.Count; // 현재 몇 번째 공격인지
            float attackTime = attackQueue.Peek(); // 첫 번째 공격 데이터 가져오기
             
            ExecuteAttack(attackTime, attackNumber);
        }
    }

    void ExecuteAttack(float heldDuration, int attackNumber)// 누른 시간에 따라 공격을 분류, 몇 번째 공격인지 따라 번호를 부여
    {
        string attackType;

        if (heldDuration < 0.3f)
        {
            attackType = "일반 공격";
            PlayAnimation(attackNumber);
        }
        else if (heldDuration < 2f)
        {
            attackType = "강한 공격";
        }
        else
        {
            attackType = "특수 기술 발동!";
        }

        Debug.Log($"{attackNumber}번째 {attackType}!");
    }

    void ForceReleaseFire()// 너무 오래 누르면 강제로 특수 기술로 변경
    {
        Debug.Log(" 2초 초과! 강제로 키를 뗌 → 특수 기술 발동!");
        RegisterAttack();
    }

    void RegisterAttack()// 너무 오래 눌러 HandleInput() 함수에 들어가지 못할 경우를 처리
    {
        float heldDuration = Time.time - pressStartTime;
        attackQueue.Enqueue(heldDuration); // 공격 큐에 추가
        lastAttackTime = Time.time; // 마지막 공격 시간 갱신
        ProcessAttackQueue();

        //isHolding = false;
    }

    void ResetAttacks()// 공격 초기화
    {
        attackQueue.Clear(); // 공격 큐 초기화
        lastAttackTime = 0f; // 타이머 초기화
        Debug.Log("공격 초기화!");
    }

    void PlayAnimation(int attackNumber)// 공격 번호에 따라 애니메이션을 부여
    {
        int Aniname = attackNumber switch
        {
            1 => animHash_Attack1,
            2 => animHash_Attack2,
            3 => animHash_Attack3,
            4 => animHash_Attack4,
            _ => animHash_Attack4,// Default;
        };
        CharAni.SetTrigger(Aniname);
    }
}
