using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Queue<float> attackQueue = new Queue<float>(); // ���� �ð��� ������ ť
    float pressStartTime = 0f;
    bool isHolding = false;

    private const int maxAttacks = 4; // �ִ� 4�� ���� ����
    private float lastAttackTime = 0f; // ������ ���� �ð� ���
    private const float resetTime = 5f; // 5�� �� �ʱ�ȭ

    private const float maxHoldTime = 3f; // 3�� �ʰ� �� ���� ����

    void notUse()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    //isHolding = true;
        //    pressStartTime = Time.time; // ���� �ð� ����
        //}

        //if (Input.GetButtonUp("Fire1"))
        //{
        //    //isHolding = false;
        //    float heldDuration = Time.time - pressStartTime; // ���� �ð� ���

        //    if (heldDuration < 1f)
        //    {
        //        Debug.Log("ª�� ������ �� �Ϲ� ����");
        //    }
        //    else if (heldDuration < 2f)
        //    {
        //        Debug.Log("�߰� ���̷� ������ �� ���� ����");
        //    }
        //    else
        //    {
        //        Debug.Log("���� ������ �� Ư�� ��� �ߵ�!");
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
            attackQueue.Enqueue(heldDuration); // ���� ť�� �߰�
            lastAttackTime = Time.time; // ������ ���� �ð� ����
            ProcessAttackQueue();

            isHolding = false;
        }

        if (Time.time - pressStartTime >= maxHoldTime)//isHolding && 
        {
            ForceReleaseFire(); // 3�� �ʰ� �� ���� ����
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
            int attackNumber = attackQueue.Count; // ���� �� ��° ��������
            float attackTime = attackQueue.Peek();
            //float attackTime = attackQueue.Dequeue(); // ù ��° ���� �ð� ��������
            ExecuteAttack(attackTime, attackNumber);
        }
    }

    void ExecuteAttack(float heldDuration, int attackNumber)
    {
        string attackType;

        if (heldDuration < 1f)
        {
            attackType = "�Ϲ� ����";
        }
        else if (heldDuration < 3f)
        {
            attackType = "���� ����";
        }
        else
        {
            attackType = "Ư�� ��� �ߵ�!";
        }

        Debug.Log($"{attackNumber}��° {attackType}!");
    }

    void ForceReleaseFire()
    {
        Debug.Log(" 3�� �ʰ�! ������ Ű�� �� �� Ư�� ��� �ߵ�!");
        RegisterAttack();
    }

    void RegisterAttack()
    {
        float heldDuration = Time.time - pressStartTime;
        attackQueue.Enqueue(heldDuration); // ���� ť�� �߰�
        lastAttackTime = Time.time; // ������ ���� �ð� ����
        ProcessAttackQueue();

        //isHolding = false;
    }

    void ResetAttacks()
    {
        attackQueue.Clear(); // ���� ť �ʱ�ȭ
        lastAttackTime = 0f; // Ÿ�̸� �ʱ�ȭ
        Debug.Log(" 5�� ���� �߰� ���� ���� �� ���� �ʱ�ȭ!");
    }
}
