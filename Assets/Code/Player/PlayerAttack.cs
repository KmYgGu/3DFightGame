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
    private const float resetTime = 3f; // 3�� �� �ʱ�ȭ     ���߿� �ִϸ��̼� �����ӿ� ���� �ð��� ����

    private const float maxHoldTime = 2f; // ���� �ð��� 2�� �ʰ� �� ���� ����


    private Animator CharAni;
    private int animHash_Attack1 = Animator.StringToHash("isAttack1");
    private int animHash_Attack2 = Animator.StringToHash("isAttack2");
    private int animHash_Attack3 = Animator.StringToHash("isAttack3");
    private int animHash_Attack4 = Animator.StringToHash("isAttack4");

    [SerializeField]private Animation[] Attackanis;// ���� �ִϸ��̼� �� ����ð��� �����ϱ� ����

    private void Start()
    {
        TryGetComponent<Animator>(out CharAni);
    }
    
    void Update()
    {

        HandleInput();
        CheckResetTimer();
    }

    void HandleInput()// �޺� ����
    {
        if (Input.GetButtonDown("Fire1") && attackQueue.Count < maxAttacks)
        {
            isHolding = true;
            pressStartTime = Time.time;
        }

        if (Input.GetButtonUp("Fire1") && attackQueue.Count < maxAttacks && isHolding)// 
        {
            float heldDuration = Time.time - pressStartTime;
            attackQueue.Enqueue(heldDuration); // ���� ť�� �߰�
            lastAttackTime = Time.time; // ������ ���� �ð� ����
            ProcessAttackQueue();
            //CheckResetTimer();
            isHolding = false;
        }

        if(attackQueue.Count >= maxAttacks)
        {
            Debug.Log(" ������ �ִ� Ƚ����ƴ �õ���");
            ResetAttacks();
        }

        if (Time.time - pressStartTime >= maxHoldTime && isHolding)//isHolding && 
        {
            isHolding = false;
            ForceReleaseFire(); // 2�� �ʰ� �� ���� ����
        }
    }

    void CheckResetTimer()// ���� Ű�� ���� ��, ���� �ð� �ȿ� ���� ������ ������ ������ �޺��� �ʱ�ȭ
    {
        
        if (attackQueue.Count > 0 && Time.time - lastAttackTime >= resetTime)//attackQueue.Count > 0 && 
        {
            Debug.Log(" 3�� ���� �߰� ���� ����");
            ResetAttacks();
        }
    }

    void ProcessAttackQueue()// �� ��° ������ �� �ʰ� �������� ���
    {
        if (attackQueue.Count > 0)
        {
            int attackNumber = attackQueue.Count; // ���� �� ��° ��������
            float attackTime = attackQueue.Peek(); // ù ��° ���� ������ ��������
             
            ExecuteAttack(attackTime, attackNumber);
        }
    }

    void ExecuteAttack(float heldDuration, int attackNumber)// ���� �ð��� ���� ������ �з�, �� ��° �������� ���� ��ȣ�� �ο�
    {
        string attackType;

        if (heldDuration < 0.3f)
        {
            attackType = "�Ϲ� ����";
            PlayAnimation(attackNumber);
        }
        else if (heldDuration < 2f)
        {
            attackType = "���� ����";
        }
        else
        {
            attackType = "Ư�� ��� �ߵ�!";
        }

        Debug.Log($"{attackNumber}��° {attackType}!");
    }

    void ForceReleaseFire()// �ʹ� ���� ������ ������ Ư�� ����� ����
    {
        Debug.Log(" 2�� �ʰ�! ������ Ű�� �� �� Ư�� ��� �ߵ�!");
        RegisterAttack();
    }

    void RegisterAttack()// �ʹ� ���� ���� HandleInput() �Լ��� ���� ���� ��츦 ó��
    {
        float heldDuration = Time.time - pressStartTime;
        attackQueue.Enqueue(heldDuration); // ���� ť�� �߰�
        lastAttackTime = Time.time; // ������ ���� �ð� ����
        ProcessAttackQueue();

        //isHolding = false;
    }

    void ResetAttacks()// ���� �ʱ�ȭ
    {
        attackQueue.Clear(); // ���� ť �ʱ�ȭ
        lastAttackTime = 0f; // Ÿ�̸� �ʱ�ȭ
        Debug.Log("���� �ʱ�ȭ!");
    }

    void PlayAnimation(int attackNumber)// ���� ��ȣ�� ���� �ִϸ��̼��� �ο�
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
