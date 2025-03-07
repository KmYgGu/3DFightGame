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

    private Stack<float> attackStack = new Stack<float>(); // 공격 시간을 저장할 스택
    private float pressStartTime = 0f;
    private bool isHolding = false;

    [SerializeField]private bool canAttack = true;//공격을 시도하면 어택박스가 사라지기 전까진 추가적으로 공격불가
    [SerializeField]private bool waitLastAttack = true;//마지막 공격 후에는 애니메이션은 불가하지만 스크립트는 계속 작동하기에 방지

    private const int maxAttacks = 4; // 최대 4번 공격 가능
    private float lastAttackTime = 0f; // 마지막 공격 시간 기록
    private float resetTime = 3f; // 3초 후 초기화     나중에 애니메이션 프레임에 따라 시간을 조정

    private const float maxHoldTime = 1f; // 누른 시간이 2초 초과 시 강제 해제

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

    // 플레이어 스텟
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
        TryGetComponent<BodyTail>(out bodyTail); // 같은 오브젝트에 있는 스크립트 가져오기
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


    #region 콤보 공격
    public void changeAttackCan()
    {
        canAttack = true;
    }

    public void changeLastAttack()
    {
        waitLastAttack = true;
        
        
    }
    
    void HandleInput()// 콤보 공격
    {
        // 마지막 공격을 다 기다리고 현재 애니메이션 상태가 표준 상태이거나 공격중일 때
        
        if(!(playerStat.aniState == AnimationTag.Run))
        {
            if (waitLastAttack)// && !((playerStat.aniState == AnimationTag.Jump)) // && transform.parent.position.y < 0.01
            {
                //공격키를 눌렀을 때, 해당 큐의 길이가 최대 공격가능 수보다 작을 때
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
                        attackStack.Push(heldDuration); // 공격 스택에 추가


                    lastAttackTime = Time.time; // 마지막 공격 시간 갱신


                    ProcessAttackQueue();

                    isHolding = false;
                }

                /*if (attackStack.Count >= maxAttacks)// 사실상 마지막 키입력 없음이 해주니 없어도 됨
                {
                    Debug.Log(" 공격을 최대 횟수만틈 시도함");
                    ResetAttacks();
                }*/

                if (Time.time - pressStartTime >= maxHoldTime && isHolding)//isHolding && 
                {
                    isHolding = false;
                    ForceReleaseFire(); // 2초 초과 시 강제 해제
                }
            }
        }

            
                
    }

    void CheckResetTimer()// 공격 키를 누른 후, 제한 시간 안에 다음 공격을 날리지 않으면 콤보를 초기화
    {
        if (attackStack.Count > 0 && !isHolding)//&& !isHolding 공격키 홀딩 중이 아닐 때만
        {

            if (attackType == "일반 공격")
            {
                resetTime = animationClips[attackStack.Count - 1].length;// - 0.1f;// + 0.1f
                                
            }
            

            if (attackType == "강한 공격")
            {
                resetTime = animationClips[attackStack.Count + 3].length;// - 0.1f;// + 0.1f
                //bodyTail.SetTail(attackStack.Count + 3);
            }
                


            if (Time.time - lastAttackTime >= resetTime)
            {
                Debug.Log($" {resetTime}초 동안 추가 공격 없음");
                ResetAttacks();
            }

        }
            

        /*if (attackStack.Count > 0 && Time.time - lastAttackTime >= resetTime)
        {
            Debug.Log(" 3초 동안 추가 공격 없음");
            ResetAttacks();
        }*/
    }

    void ProcessAttackQueue()// 몇 번째 공격을 몇 초간 눌렀는지 재생
    {
        if (playerJump.isground)
        {
            if(playerStat.aniState == AnimationTag.walk || playerStat.aniState == AnimationTag.Idle || playerStat.aniState == AnimationTag.Attack)
            {
                if (attackStack.Count > 0)
                {

                    int attackNumber = attackStack.Count; // 현재 몇 번째 공격인지
                    float attackTime = attackStack.Peek(); // 마지막의 공격 데이터 가져오기
                                                           //Debug.Log($"{attackNumber}: {attackTime}");

                    ExecuteAttack(attackTime, attackNumber);
                }
            }
            else if (playerStat.aniState == AnimationTag.GuardSucess)// 반격
            {
                attackStack.Clear(); // 공격 스택 초기화
                lastAttackTime = 0f; // 타이머 초기화

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
            //Debug.Log("이 공격은 공중 공격입니다");
            attackStack.Clear(); // 공격 스택 초기화
            lastAttackTime = 0f; // 타이머 초기화

            //CharAni.ResetTrigger("isAttack1");
            //CharAni.ResetTrigger("isSAttack1");
            canAttack = true;

            //Debug.Log(transform.parent.position.y);
            CharAni.ResetTrigger(animHash_FAttack1);
            CharAni.SetTrigger(animHash_FAttack1);
        }
        
    }

    void ExecuteAttack(float heldDuration, int attackNumber)// 누른 시간에 따라 공격을 분류, 몇 번째 공격인지 따라 번호를 부여
    {
        //string attackType;
        if (canAttack)
        {
            if (heldDuration < 0.2f)
            {
                canAttack = false;
                attackType = "일반 공격";
                bodyTail.SetTail(attackNumber - 1);
                PlayAnimation(attackNumber);

                if(attackNumber == 4)
                    waitLastAttack = false;

                
            }
            else //if (heldDuration < 1.5f)
            {
                canAttack = false;
                attackType = "강한 공격";
                bodyTail.SetTail(attackNumber + 3);
                PlaySattackAnimation(attackNumber);

                if (attackNumber == 4)
                    waitLastAttack = false;
            }
            Debug.Log($"{attackNumber}번째 {attackType}!  시간 : {heldDuration}");
        }


        //Debug.Log($"{attackNumber}번째 {attackType}!  시간 : {heldDuration}");
    }

    void ForceReleaseFire()// 너무 오래 누르면 강제로 특수 기술로 변경
    {
        Debug.Log(" 1.5초 초과! 강제로 키를 뗌");
        RegisterAttack();
    }

    void RegisterAttack()// 너무 오래 눌러 HandleInput() 함수에 들어가지 못할 경우를 처리
    {
        float heldDuration = Time.time - pressStartTime;
        attackStack.Push(heldDuration); // 공격 스택에 추가
        lastAttackTime = Time.time; // 마지막 공격 시간 갱신
        ProcessAttackQueue();

        //isHolding = false;
    }

    void ResetAttacks()// 공격 초기화
    {
        attackStack.Clear(); // 공격 스택 초기화
        lastAttackTime = 0f; // 타이머 초기화
        Debug.Log("공격 초기화!");
        bodyTail.SetTail(30);
        //EventManager.Instance.TriggerEvent();
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

        lookView.CheckAndRotateToTarget();// 시야 안에 적이 있으면 적을 향해 캐릭터를 회전
        EventManager.Instance.TriggerEvent();//attack
        //StartCoroutine("GetAnimationTag");
    }

    void PlaySattackAnimation(int attackNumber)// 공격 번호에 따라 애니메이션을 부여
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
