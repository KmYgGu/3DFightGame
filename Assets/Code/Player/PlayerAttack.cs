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
    private const float resetTime = 5f; // 5초 후 초기화

    private const float maxHoldTime = 3f; // 3초 초과 시 강제 해제

    void notUse()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    //isHolding = true;
        //    pressStartTime = Time.time; // 현재 시간 저장
        //}

        //if (Input.GetButtonUp("Fire1"))
        //{
        //    //isHolding = false;
        //    float heldDuration = Time.time - pressStartTime; // 누른 시간 계산

        //    if (heldDuration < 1f)
        //    {
        //        Debug.Log("짧게 눌렀음 → 일반 공격");
        //    }
        //    else if (heldDuration < 2f)
        //    {
        //        Debug.Log("중간 길이로 눌렀음 → 강한 공격");
        //    }
        //    else
        //    {
        //        Debug.Log("오래 눌렀음 → 특수 기술 발동!");
        //    }
        //}
    }

    void Update()
    {
        

        HandleInput();
        CheckResetTimer();
    }

    void HandleInput()
    {
        if (Input.GetButtonDown("Fire1") && attackQueue.Count < maxAttacks)
        {
            isHolding = true;
            pressStartTime = Time.time;
        }

        if (Input.GetButtonUp("Fire1"))// && isHolding
        {
            float heldDuration = Time.time - pressStartTime;
            attackQueue.Enqueue(heldDuration); // 공격 큐에 추가
            lastAttackTime = Time.time; // 마지막 공격 시간 갱신
            ProcessAttackQueue();

            isHolding = false;
        }

        if (Time.time - pressStartTime >= maxHoldTime)//isHolding && 
        {
            ForceReleaseFire(); // 3초 초과 시 강제 해제
        }
    }

    void CheckResetTimer()
    {
        if (attackQueue.Count > 0 && Time.time - lastAttackTime >= resetTime)
        {
            ResetAttacks();
        }
    }

    void ProcessAttackQueue()
    {
        if (attackQueue.Count > 0)
        {
            int attackNumber = attackQueue.Count; // 현재 몇 번째 공격인지
            float attackTime = attackQueue.Peek();
            //float attackTime = attackQueue.Dequeue(); // 첫 번째 공격 시간 가져오기
            ExecuteAttack(attackTime, attackNumber);
        }
    }

    void ExecuteAttack(float heldDuration, int attackNumber)
    {
        string attackType;

        if (heldDuration < 1f)
        {
            attackType = "일반 공격";
        }
        else if (heldDuration < 3f)
        {
            attackType = "강한 공격";
        }
        else
        {
            attackType = "특수 기술 발동!";
        }

        Debug.Log($"{attackNumber}번째 {attackType}!");
    }

    void ForceReleaseFire()
    {
        Debug.Log(" 3초 초과! 강제로 키를 뗌 → 특수 기술 발동!");
        RegisterAttack();
    }

    void RegisterAttack()
    {
        float heldDuration = Time.time - pressStartTime;
        attackQueue.Enqueue(heldDuration); // 공격 큐에 추가
        lastAttackTime = Time.time; // 마지막 공격 시간 갱신
        ProcessAttackQueue();

        //isHolding = false;
    }

    void ResetAttacks()
    {
        attackQueue.Clear(); // 공격 큐 초기화
        lastAttackTime = 0f; // 타이머 초기화
        Debug.Log(" 5초 동안 추가 공격 없음 → 공격 초기화!");
    }
}
