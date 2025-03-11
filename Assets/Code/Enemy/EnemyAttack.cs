using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // 적은 플레이어가 공격을 맞았는 지, 막았는 지에 따라 공격 콤보가 달라지게 변경

    private EnemyStat enemyStat;
    private AIEnemy aIEnemy;

    private Animator animator; // 재생되는 애니메이션 길이 가져오기
    private int animHash_Attack1 = Animator.StringToHash("isAttack1");
    private int animHash_Attack2 = Animator.StringToHash("isAttack2");
    private int animHash_Attack3 = Animator.StringToHash("isAttack3");
    private int animHash_Attack4 = Animator.StringToHash("isAttack4");

    private int animHash_SAttack1 = Animator.StringToHash("isSAttack1");
    private int animHash_SAttack2 = Animator.StringToHash("isSAttack2");
    private int animHash_SAttack3 = Animator.StringToHash("isSAttack3");
    private int animHash_SAttack4 = Animator.StringToHash("isSAttack4");


    [SerializeField]private bool isAttacking = false;// 공격중이면 다른 공격은 안되게

    // Start is called before the first frame update
    void Awake()
    {
        enemyStat =GetComponentInParent<EnemyStat>();
        TryGetComponent<Animator>(out animator);
        aIEnemy = GetComponentInParent<AIEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) && !isAttacking)
        {
            //enemyStat.ChangeisAttackfalse();
            StartCoroutine("Attack1");
            
        }
            

        if (Input.GetKeyDown(KeyCode.Keypad2) && !isAttacking)
        {
            //enemyStat.ChangeisAttackfalse();
            StartCoroutine("SAttack2");
        }
            

        if (Input.GetKeyDown(KeyCode.Keypad3) && !isAttacking)
        {
            //enemyStat.ChangeisAttackfalse();
            StartCoroutine("SAttack3");
        }
            

        if (Input.GetKeyDown(KeyCode.Keypad4) && !isAttacking)
        {
            //enemyStat.ChangeisAttackfalse();
            StartCoroutine("Attack4");
        }
            

        if (Input.GetKeyDown(KeyCode.Keypad5) && !isAttacking)
        {
            //enemyStat.ChangeisAttackfalse();
            StartCoroutine("SAttack4");
        }
            

    }
    public IEnumerator AttackChoice()
    {
        //aIEnemy.ChangedenemyAi(EnemyAIis.Attacking);
        //if (!enemyStat.isattack)
        {
            yield return StartCoroutine(Attack1());
        }
        /*else if (enemyStat.isattack && enemyStat.aniState == AnimationTag.Idle)
        {
            enemyStat.ChangeisAttackfalse();
        }*/
        
    }
    
    public IEnumerator Attack1()// 한 손 슬래쉬
    {

        enemyStat.ChangeisAttackfalse();
        isAttacking = true;

        //Debug.Log(enemyStat.aniState);
        
        animator.SetTrigger(animHash_Attack1);

        EventManager.Instance.EnemyaniEvent();//Attack1

        //Debug.Log(1);
        aIEnemy.ChangedenemyAi(EnemyAIis.idle);
        yield return new WaitForEndOfFrame(); // 애니메이션 길이 만큼 기다리기
        isAttacking = false;

        

        /*if (enemyStat.isattack)// 공격이 맞았을 경우,
        {

        }
        else if(!enemyStat.isattack && enemyStat.aniState == AnimationTag.Idle) // 공격이 맞지 않았고, 적이 대기상태 애니메이션일 경우.
        {
            
        }*/


    }

    IEnumerator Attack2()// 반 자른 두손검
    {
        isAttacking = true;

        animator.SetTrigger(animHash_Attack2);
        EventManager.Instance.EnemyaniEvent();//Attack2

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator Attack3()// 회전 베기1 // 칼위치를 조정할 필요 있음
    {
        isAttacking = true;

        animator.SetTrigger(animHash_Attack3);
        EventManager.Instance.EnemyaniEvent();//Attack3

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator Attack4()// 회전 베기2
    {
        isAttacking = true;

        animator.SetTrigger(animHash_Attack4);
        EventManager.Instance.EnemyaniEvent();//Attack4

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator SAttack1()// 검기 날리기
    {
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack1);
        EventManager.Instance.EnemyaniEvent();//Attack5

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator SAttack2()// 두 손검킥 1
    {
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack2);
        EventManager.Instance.EnemyaniEvent();//Attack6

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator SAttack3()// 두 손검킥 2
    {
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack3);
        EventManager.Instance.EnemyaniEvent();//Attack7

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator SAttack4()// 3연속 베기
    {
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack4);
        EventManager.Instance.EnemyaniEvent();//Attack8

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }
}
