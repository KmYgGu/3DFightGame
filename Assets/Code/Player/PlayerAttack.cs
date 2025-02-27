using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    private enum PlayerAttackType
    {
        normalAttack,
        StrongAttack,
        SpecialAttack,
    }

    private Stack<float> attackStack = new Stack<float>(); // ���� �ð��� ������ ����
    float pressStartTime = 0f;
    bool isHolding = false;

    private const int maxAttacks = 4; // �ִ� 4�� ���� ����
    private float lastAttackTime = 0f; // ������ ���� �ð� ���
    private float resetTime = 3f; // 3�� �� �ʱ�ȭ     ���߿� �ִϸ��̼� �����ӿ� ���� �ð��� ����

    private const float maxHoldTime = 1.5f; // ���� �ð��� 2�� �ʰ� �� ���� ����

    private string attackType;

    private Animator CharAni;
    private int animHash_Attack1 = Animator.StringToHash("isAttack1");
    private int animHash_Attack2 = Animator.StringToHash("isAttack2");
    private int animHash_Attack3 = Animator.StringToHash("isAttack3");
    private int animHash_Attack4 = Animator.StringToHash("isAttack4");

    private int animHash_SAttack1 = Animator.StringToHash("isSAttack1");
    private int animHash_SAttack2 = Animator.StringToHash("isSAttack2");
    private int animHash_SAttack3 = Animator.StringToHash("isSAttack3");
    private int animHash_SAttack4 = Animator.StringToHash("isSAttack4");

    //[SerializeField]private Animation[] Attackanis;// ���� �ִϸ��̼� �� ����ð��� �����ϱ� ����

    [SerializeField]private AnimationClip[] animationClips;

    private void Start()
    {
        TryGetComponent<Animator>(out CharAni);
    }
    
    void Update()
    {

        HandleInput();
        CheckResetTimer();
    }


    #region �޺� ����
    void HandleInput()// �޺� ����
    {
        if (Input.GetButtonDown("Fire1") && attackStack.Count < maxAttacks)//����Ű�� ������ ��, �ش� ť�� ���̰� �ִ� ���ݰ��� ������ ���� ��
        {
            isHolding = true;
            pressStartTime = Time.time;
            
        }

        if (Input.GetButtonUp("Fire1") && attackStack.Count < maxAttacks && isHolding)// 
        {
            float heldDuration = Time.time - pressStartTime;
            
            attackStack.Push(heldDuration); // ���� ���ÿ� �߰�
                   

            lastAttackTime = Time.time; // ������ ���� �ð� ����
            ProcessAttackQueue();
            
            isHolding = false;
        }

        if(attackStack.Count >= maxAttacks)
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
        if (attackStack.Count > 0 && !isHolding)//&& !isHolding ����Ű Ȧ�� ���� �ƴ� ����
        {

            if (attackType == "�Ϲ� ����")
            resetTime = animationClips[attackStack.Count-1].length - 0.2f;// + 0.1f

            if (attackType == "���� ����")
                resetTime = animationClips[attackStack.Count + 3].length - 0.2f;// + 0.1f


            if (Time.time - lastAttackTime >= resetTime)
            {
                Debug.Log($" {resetTime}�� ���� �߰� ���� ����");
                ResetAttacks();
            }
        }
            

        /*if (attackStack.Count > 0 && Time.time - lastAttackTime >= resetTime)
        {
            Debug.Log(" 3�� ���� �߰� ���� ����");
            ResetAttacks();
        }*/
    }

    void ProcessAttackQueue()// �� ��° ������ �� �ʰ� �������� ���
    {
        if (attackStack.Count > 0)
        {
            int attackNumber = attackStack.Count; // ���� �� ��° ��������
            float attackTime = attackStack.Peek(); // �������� ���� ������ ��������
            //Debug.Log($"{attackNumber}: {attackTime}");
             
            ExecuteAttack(attackTime, attackNumber);
        }
    }

    void ExecuteAttack(float heldDuration, int attackNumber)// ���� �ð��� ���� ������ �з�, �� ��° �������� ���� ��ȣ�� �ο�
    {
        //string attackType;

        if (heldDuration < 0.3f)
        {
            attackType = "�Ϲ� ����";
            PlayAnimation(attackNumber);
        }
        else if (heldDuration < 1.5f)
        {
            attackType = "���� ����";
            PlaySattackAnimation(attackNumber);
        }
        else
        {
            attackType = "Ư�� ��� �ߵ�!";
        }

        Debug.Log($"{attackNumber}��° {attackType}!  �ð� : {heldDuration}");
    }

    void ForceReleaseFire()// �ʹ� ���� ������ ������ Ư�� ����� ����
    {
        Debug.Log(" 2�� �ʰ�! ������ Ű�� �� �� Ư�� ��� �ߵ�!");
        RegisterAttack();
    }

    void RegisterAttack()// �ʹ� ���� ���� HandleInput() �Լ��� ���� ���� ��츦 ó��
    {
        float heldDuration = Time.time - pressStartTime;
        attackStack.Push(heldDuration); // ���� ���ÿ� �߰�
        lastAttackTime = Time.time; // ������ ���� �ð� ����
        ProcessAttackQueue();

        //isHolding = false;
    }

    void ResetAttacks()// ���� �ʱ�ȭ
    {
        attackStack.Clear(); // ���� ���� �ʱ�ȭ
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

    void PlaySattackAnimation(int attackNumber)// ���� ��ȣ�� ���� �ִϸ��̼��� �ο�
    {
        int Aniname = attackNumber switch
        {
            1 => animHash_SAttack1,
            2 => animHash_SAttack2,
            3 => animHash_SAttack3,
            4 => animHash_SAttack4,
            _ => animHash_SAttack4,// Default;
        };
        CharAni.SetTrigger(Aniname);
    }
    #endregion



}
