using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // ���� �÷��̾ ������ �¾Ҵ� ��, ���Ҵ� ���� ���� ���� �޺��� �޶����� ����

    private EnemyStat enemyStat;
    private AIEnemy aIEnemy;
    private EnemyMove enemyMove;

    private Animator animator; // ����Ǵ� �ִϸ��̼� ���� ��������
    private int animHash_Attack1 = Animator.StringToHash("isAttack1");
    private int animHash_Attack2 = Animator.StringToHash("isAttack2");
    private int animHash_Attack3 = Animator.StringToHash("isAttack3");
    private int animHash_Attack4 = Animator.StringToHash("isAttack4");

    private int animHash_SAttack1 = Animator.StringToHash("isSAttack1");
    private int animHash_SAttack2 = Animator.StringToHash("isSAttack2");
    private int animHash_SAttack3 = Animator.StringToHash("isSAttack3");
    private int animHash_SAttack4 = Animator.StringToHash("isSAttack4");

    [SerializeField] private AnimationClip[] aniClip;


    [SerializeField]private bool isAttacking = false;// �������̸� �ٸ� ������ �ȵǰ�

    private IEnumerator[] coroutines;
    

    // Start is called before the first frame update
    void Awake()
    {
        enemyStat =GetComponentInParent<EnemyStat>();
        TryGetComponent<Animator>(out animator);
        aIEnemy = GetComponentInParent<AIEnemy>();
        enemyMove = GetComponentInParent<EnemyMove>();

        coroutines = new IEnumerator[]
        {
            Attack1(),
            Attack2(),
            Attack3(),
            Attack4(),
            SAttack1(),
            SAttack2(),
            SAttack3(),
            SAttack4(),
        };

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
        
            yield return StartCoroutine(Attack1());

        Debug.Log("���� �ڷ�ƾ ��");
        //aIEnemy.ChangedenemyAi(EnemyAIis.idle);
        StartCoroutine(aIEnemy.AIStart());
    }

    public IEnumerator RandomAction(int index)
    {
        if (enemyMove.DistanceCheck())//�Ÿ���
        {
            Debug.Log("�Ÿ� ��");
            int randomValue = Random.Range(1, 3);
            switch (randomValue)
            {
                case 1:
                    Debug.Log("1�� ���� ����");
                    //StartCoroutine(coroutines[index]);
                    yield return StartCoroutine(coroutines[index]);
                    break;
                
                case 2:
                    Debug.Log("2�� ������ �޺�");
                    
                    yield return StartCoroutine(SAttack2());
                    break;
                default:
                    break;
            }
        }
        else//�Ÿ� ��
        {
            Debug.Log("�Ÿ� ��");
            int randomValue = Random.Range(1, 3);
            switch (randomValue)
            {
                case 1:
                    Debug.Log("1�� �̵�");
                    aIEnemy.ChangedenemyAi(EnemyAIis.idle);

                    yield return new WaitForEndOfFrame();
                    //yield return new WaitForFixedUpdate();
                    //StartCoroutine(aIEnemy.AIStart());
                    break;

                case 2:
                    Debug.Log("2�� �˱⳯����");
                    //StartCoroutine(SAttack1());
                    yield return StartCoroutine(SAttack1());
                    break;
                default:
                    break;
            }
                        
        }
        
    }
    
    public IEnumerator Attack1()// �� �� ������
    {
        enemyMove.QuickLook();
        enemyStat.ChangeisAttackfalse();
        isAttacking = true;
        
        animator.SetTrigger(animHash_Attack1);

        EventManager.Instance.EnemyaniEvent();//Attack1
               

        //yield return new WaitForEndOfFrame(); // �ִϸ��̼� ���� ��ŭ ��ٸ���
        yield return new WaitForSeconds(aniClip[0].length);
        isAttacking = false;


        yield return StartCoroutine(RandomAction(1));
        //StartCoroutine(RandomAction(1));


    }

    IEnumerator Attack2()// �� �ڸ� �μհ�
    {
        enemyMove.QuickLook();
        enemyStat.ChangeisAttackfalse();
        isAttacking = true;

        animator.SetTrigger(animHash_Attack2);
        EventManager.Instance.EnemyaniEvent();//Attack2

        //yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(aniClip[1].length);
        isAttacking = false;

        yield return StartCoroutine(RandomAction(2));
        //StartCoroutine(RandomAction(2));
    }

    IEnumerator Attack3()// ȸ�� ����1 // Į��ġ�� ������ �ʿ� ����
    {
        enemyMove.QuickLook();
        enemyStat.ChangeisAttackfalse();
        isAttacking = true;

        animator.SetTrigger(animHash_Attack3);
        EventManager.Instance.EnemyaniEvent();//Attack3

        //yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(aniClip[2].length);
        isAttacking = false;

        yield return StartCoroutine(RandomAction(3));
        //StartCoroutine(RandomAction(3));
    }

    IEnumerator Attack4()// ȸ�� ����2
    {
        enemyMove.QuickLook();
        enemyStat.ChangeisAttackfalse();
        isAttacking = true;

        animator.SetTrigger(animHash_Attack4);
        EventManager.Instance.EnemyaniEvent();//Attack4

        aIEnemy.ChangedenemyAi(EnemyAIis.idle);
        //yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(aniClip[3].length);
        isAttacking = false;

        //StartCoroutine(aIEnemy.AIStart());


    }

    IEnumerator SAttack1()// �˱� ������
    {
        enemyMove.QuickLook();
        enemyStat.ChangeisAttackfalse();
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack1);
        EventManager.Instance.EnemyaniEvent();//Attack5
        SwordWindManager.Instance.SpawnAttack(gameObject.transform);

        //yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(aniClip[4].length);
        isAttacking = false;

        yield return StartCoroutine(RandomAction(1));
        //StartCoroutine(RandomAction(1));
    }

    IEnumerator SAttack2()// �� �հ�ű 1
    {
        enemyMove.QuickLook();
        enemyStat.ChangeisAttackfalse();
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack2);
        EventManager.Instance.EnemyaniEvent();//Attack6

        //yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(aniClip[5].length);
        isAttacking = false;

        int randomValue = Random.Range(1, 5);
        switch (randomValue)
        {
            case 1:
                Debug.Log("1�� �̵�");
                aIEnemy.ChangedenemyAi(EnemyAIis.idle);

                yield return new WaitForEndOfFrame();
                //yield return new WaitForFixedUpdate();
                //StartCoroutine(aIEnemy.AIStart());
                break;

            default:
                Debug.Log("���� ������ ����");
                //StartCoroutine(SAttack1());
                yield return StartCoroutine(SAttack3());
                break;
            
                
        }
    }

    IEnumerator SAttack3()// �� �հ�ű 2
    {
        enemyMove.QuickLook();
        enemyStat.ChangeisAttackfalse();
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack3);
        EventManager.Instance.EnemyaniEvent();//Attack7

        //yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(aniClip[6].length);
        isAttacking = false;

        aIEnemy.ChangedenemyAi(EnemyAIis.idle);

        yield return new WaitForEndOfFrame();
    }

    IEnumerator SAttack4()// 3���� ����
    {
        enemyMove.QuickLook();
        enemyStat.ChangeisAttackfalse();
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack4);
        EventManager.Instance.EnemyaniEvent();//Attack8

        //yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(aniClip[7].length);
        isAttacking = false;
    }
}
