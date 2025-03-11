using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // ���� �÷��̾ ������ �¾Ҵ� ��, ���Ҵ� ���� ���� ���� �޺��� �޶����� ����

    private EnemyStat enemyStat;
    private AIEnemy aIEnemy;

    private Animator animator; // ����Ǵ� �ִϸ��̼� ���� ��������
    private int animHash_Attack1 = Animator.StringToHash("isAttack1");
    private int animHash_Attack2 = Animator.StringToHash("isAttack2");
    private int animHash_Attack3 = Animator.StringToHash("isAttack3");
    private int animHash_Attack4 = Animator.StringToHash("isAttack4");

    private int animHash_SAttack1 = Animator.StringToHash("isSAttack1");
    private int animHash_SAttack2 = Animator.StringToHash("isSAttack2");
    private int animHash_SAttack3 = Animator.StringToHash("isSAttack3");
    private int animHash_SAttack4 = Animator.StringToHash("isSAttack4");


    [SerializeField]private bool isAttacking = false;// �������̸� �ٸ� ������ �ȵǰ�

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
    
    public IEnumerator Attack1()// �� �� ������
    {

        enemyStat.ChangeisAttackfalse();
        isAttacking = true;

        //Debug.Log(enemyStat.aniState);
        
        animator.SetTrigger(animHash_Attack1);

        EventManager.Instance.EnemyaniEvent();//Attack1

        //Debug.Log(1);
        aIEnemy.ChangedenemyAi(EnemyAIis.idle);
        yield return new WaitForEndOfFrame(); // �ִϸ��̼� ���� ��ŭ ��ٸ���
        isAttacking = false;

        

        /*if (enemyStat.isattack)// ������ �¾��� ���,
        {

        }
        else if(!enemyStat.isattack && enemyStat.aniState == AnimationTag.Idle) // ������ ���� �ʾҰ�, ���� ������ �ִϸ��̼��� ���.
        {
            
        }*/


    }

    IEnumerator Attack2()// �� �ڸ� �μհ�
    {
        isAttacking = true;

        animator.SetTrigger(animHash_Attack2);
        EventManager.Instance.EnemyaniEvent();//Attack2

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator Attack3()// ȸ�� ����1 // Į��ġ�� ������ �ʿ� ����
    {
        isAttacking = true;

        animator.SetTrigger(animHash_Attack3);
        EventManager.Instance.EnemyaniEvent();//Attack3

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator Attack4()// ȸ�� ����2
    {
        isAttacking = true;

        animator.SetTrigger(animHash_Attack4);
        EventManager.Instance.EnemyaniEvent();//Attack4

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator SAttack1()// �˱� ������
    {
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack1);
        EventManager.Instance.EnemyaniEvent();//Attack5

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator SAttack2()// �� �հ�ű 1
    {
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack2);
        EventManager.Instance.EnemyaniEvent();//Attack6

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator SAttack3()// �� �հ�ű 2
    {
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack3);
        EventManager.Instance.EnemyaniEvent();//Attack7

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator SAttack4()// 3���� ����
    {
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack4);
        EventManager.Instance.EnemyaniEvent();//Attack8

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }
}
