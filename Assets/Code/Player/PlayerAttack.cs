using System;
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
    private float pressStartTime = 0f;
    private bool isHolding = false;

    [SerializeField]private bool canAttack = true;//������ �õ��ϸ� ���ùڽ��� ������� ������ �߰������� ���ݺҰ�
    [SerializeField]private bool waitLastAttack = true;//������ ���� �Ŀ��� �ִϸ��̼��� �Ұ������� ��ũ��Ʈ�� ��� �۵��ϱ⿡ ����

    private const int maxAttacks = 4; // �ִ� 4�� ���� ����
    private float lastAttackTime = 0f; // ������ ���� �ð� ���
    private float resetTime = 3f; // 3�� �� �ʱ�ȭ     ���߿� �ִϸ��̼� �����ӿ� ���� �ð��� ����

    private const float maxHoldTime = 1f; // ���� �ð��� 2�� �ʰ� �� ���� ����

    private string attackType;

    private Animator CharAni;
    //private AnimatorStateInfo stateInfo;
    private AnimationTagReader tagReader;

    private int animHash_Attack1 = Animator.StringToHash("isAttack1");
    private int animHash_Attack2 = Animator.StringToHash("isAttack2");
    private int animHash_Attack3 = Animator.StringToHash("isAttack3");
    private int animHash_Attack4 = Animator.StringToHash("isAttack4");

    private int animHash_SAttack1 = Animator.StringToHash("isSAttack1");
    private int animHash_SAttack2 = Animator.StringToHash("isSAttack2");
    private int animHash_SAttack3 = Animator.StringToHash("isSAttack3");
    private int animHash_SAttack4 = Animator.StringToHash("isSAttack4");

    private int animHash_FAttack1 = Animator.StringToHash("isFAttack1");
    private int animHash_CAttack1 = Animator.StringToHash("isCAttack");

    private BodyTail bodyTail;
    private PlayerJump playerJump;

    // �÷��̾� ����
    private PlayerStat playerStat;

    private LookViewAttack lookView;
    
    [SerializeField]private AnimationClip[] animationClips;

    public AnimationClip GetAnimationClip(int aniNo)
    {
        return animationClips[aniNo];
    }

    private void Start()
    {
        TryGetComponent<Animator>(out CharAni);
        TryGetComponent<BodyTail>(out bodyTail); // ���� ������Ʈ�� �ִ� ��ũ��Ʈ ��������
        TryGetComponent<AnimationTagReader>(out tagReader);

        playerStat = gameObject.GetComponentInParent<PlayerStat>();
        playerJump = gameObject.GetComponentInParent<PlayerJump>();
        lookView = gameObject.GetComponentInParent<LookViewAttack>();


    }
    
    void Update()
    {

        HandleInput();
        CheckResetTimer();
                
    }


    #region �޺� ����
    public void changeAttackCan()
    {
        canAttack = true;
    }

    public void changeLastAttack()
    {
        waitLastAttack = true;
        
        
    }
    
    void HandleInput()// �޺� ����
    {
        // ������ ������ �� ��ٸ��� ���� �ִϸ��̼� ���°� ǥ�� �����̰ų� �������� ��
        
        if(!(playerStat.aniState == AnimationTag.Run))
        {
            if (waitLastAttack)// && !((playerStat.aniState == AnimationTag.Jump)) // && transform.parent.position.y < 0.01
            {
                //����Ű�� ������ ��, �ش� ť�� ���̰� �ִ� ���ݰ��� ������ ���� ��
                if ((Input.GetButtonDown("Fire1") && attackStack.Count < maxAttacks))
                {

                    isHolding = true;
                    pressStartTime = Time.time;

                }

                if (Input.GetButtonUp("Fire1") && attackStack.Count < maxAttacks && isHolding)// 
                {
                    //Debug.Log(transform.parent.position.y);
                    float heldDuration = Time.time - pressStartTime;

                    if (canAttack)
                        attackStack.Push(heldDuration); // ���� ���ÿ� �߰�


                    lastAttackTime = Time.time; // ������ ���� �ð� ����


                    ProcessAttackQueue();

                    isHolding = false;
                }

                /*if (attackStack.Count >= maxAttacks)// ��ǻ� ������ Ű�Է� ������ ���ִ� ��� ��
                {
                    Debug.Log(" ������ �ִ� Ƚ����ƴ �õ���");
                    ResetAttacks();
                }*/

                if (Time.time - pressStartTime >= maxHoldTime && isHolding)//isHolding && 
                {
                    isHolding = false;
                    ForceReleaseFire(); // 2�� �ʰ� �� ���� ����
                }
            }
        }

            
                
    }

    void CheckResetTimer()// ���� Ű�� ���� ��, ���� �ð� �ȿ� ���� ������ ������ ������ �޺��� �ʱ�ȭ
    {
        if (attackStack.Count > 0 && !isHolding)//&& !isHolding ����Ű Ȧ�� ���� �ƴ� ����
        {

            if (attackType == "�Ϲ� ����")
            {
                resetTime = animationClips[attackStack.Count - 1].length;// - 0.1f;// + 0.1f
                                
            }
            

            if (attackType == "���� ����")
            {
                resetTime = animationClips[attackStack.Count + 3].length;// - 0.1f;// + 0.1f
                //bodyTail.SetTail(attackStack.Count + 3);
            }
                


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
        if (playerJump.isground)
        {
            if(playerStat.aniState == AnimationTag.walk || playerStat.aniState == AnimationTag.Idle || playerStat.aniState == AnimationTag.Attack)
            {
                if (attackStack.Count > 0)
                {

                    int attackNumber = attackStack.Count; // ���� �� ��° ��������
                    float attackTime = attackStack.Peek(); // �������� ���� ������ ��������
                                                           //Debug.Log($"{attackNumber}: {attackTime}");

                    ExecuteAttack(attackTime, attackNumber);
                }
            }
            else if (playerStat.aniState == AnimationTag.GuardSucess)// �ݰ�
            {
                attackStack.Clear(); // ���� ���� �ʱ�ȭ
                lastAttackTime = 0f; // Ÿ�̸� �ʱ�ȭ

                //CharAni.ResetTrigger("isAttack1");
                //CharAni.ResetTrigger("isSAttack1");
                waitLastAttack = false;

                //Debug.Log(transform.parent.position.y);
                CharAni.ResetTrigger(animHash_CAttack1);
                CharAni.SetTrigger(animHash_CAttack1);
            }

            
        }
        else
        {
            //Debug.Log("�� ������ ���� �����Դϴ�");
            attackStack.Clear(); // ���� ���� �ʱ�ȭ
            lastAttackTime = 0f; // Ÿ�̸� �ʱ�ȭ

            //CharAni.ResetTrigger("isAttack1");
            //CharAni.ResetTrigger("isSAttack1");
            canAttack = true;

            //Debug.Log(transform.parent.position.y);
            CharAni.ResetTrigger(animHash_FAttack1);
            CharAni.SetTrigger(animHash_FAttack1);
        }
        
    }

    void ExecuteAttack(float heldDuration, int attackNumber)// ���� �ð��� ���� ������ �з�, �� ��° �������� ���� ��ȣ�� �ο�
    {
        //string attackType;
        if (canAttack)
        {
            if (heldDuration < 0.2f)
            {
                canAttack = false;
                attackType = "�Ϲ� ����";
                bodyTail.SetTail(attackNumber - 1);
                PlayAnimation(attackNumber);

                if(attackNumber == 4)
                    waitLastAttack = false;

                
            }
            else //if (heldDuration < 1.5f)
            {
                canAttack = false;
                attackType = "���� ����";
                bodyTail.SetTail(attackNumber + 3);
                PlaySattackAnimation(attackNumber);

                if (attackNumber == 4)
                    waitLastAttack = false;
            }
            Debug.Log($"{attackNumber}��° {attackType}!  �ð� : {heldDuration}");
        }


        //Debug.Log($"{attackNumber}��° {attackType}!  �ð� : {heldDuration}");
    }

    void ForceReleaseFire()// �ʹ� ���� ������ ������ Ư�� ����� ����
    {
        Debug.Log(" 1.5�� �ʰ�! ������ Ű�� ��");
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
        bodyTail.SetTail(30);
        //EventManager.Instance.TriggerEvent();
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

        lookView.CheckAndRotateToTarget();// �þ� �ȿ� ���� ������ ���� ���� ĳ���͸� ȸ��
        EventManager.Instance.TriggerEvent();//attack
        //StartCoroutine("GetAnimationTag");
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

        lookView.CheckAndRotateToTarget();
        EventManager.Instance.TriggerEvent();//attack
    }

    #endregion



}
