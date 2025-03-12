using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // 적은 플레이어가 공격을 맞았는 지, 막았는 지에 따라 공격 콤보가 달라지게 변경

    private EnemyStat enemyStat;
    private AIEnemy aIEnemy;
    private EnemyMove enemyMove;

    private Animator animator; // 재생되는 애니메이션 길이 가져오기
    private int animHash_Attack1 = Animator.StringToHash("isAttack1");
    private int animHash_Attack2 = Animator.StringToHash("isAttack2");
    private int animHash_Attack3 = Animator.StringToHash("isAttack3");
    private int animHash_Attack4 = Animator.StringToHash("isAttack4");

    private int animHash_SAttack1 = Animator.StringToHash("isSAttack1");
    private int animHash_SAttack2 = Animator.StringToHash("isSAttack2");
    private int animHash_SAttack3 = Animator.StringToHash("isSAttack3");
    private int animHash_SAttack4 = Animator.StringToHash("isSAttack4");

    [SerializeField] private AnimationClip[] aniClip;


    [SerializeField]private bool isAttacking = false;// 공격중이면 다른 공격은 안되게

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

        Debug.Log("공격 코루틴 끝");
        //aIEnemy.ChangedenemyAi(EnemyAIis.idle);
        StartCoroutine(aIEnemy.AIStart());
    }

    public IEnumerator RandomAction(int index)
    {
        if (enemyMove.DistanceCheck())//거리안
        {
            Debug.Log("거리 안");
            int randomValue = Random.Range(1, 3);
            switch (randomValue)
            {
                case 1:
                    Debug.Log("1번 다음 공격");
                    //StartCoroutine(coroutines[index]);
                    yield return StartCoroutine(coroutines[index]);
                    break;
                
                case 2:
                    Debug.Log("2번 발차기 콤보");
                    
                    yield return StartCoroutine(SAttack2());
                    break;
                default:
                    break;
            }
        }
        else//거리 밖
        {
            Debug.Log("거리 밖");
            int randomValue = Random.Range(1, 3);
            switch (randomValue)
            {
                case 1:
                    Debug.Log("1번 이동");
                    aIEnemy.ChangedenemyAi(EnemyAIis.idle);

                    yield return new WaitForEndOfFrame();
                    //yield return new WaitForFixedUpdate();
                    //StartCoroutine(aIEnemy.AIStart());
                    break;

                case 2:
                    Debug.Log("2번 검기날리기");
                    //StartCoroutine(SAttack1());
                    yield return StartCoroutine(SAttack1());
                    break;
                default:
                    break;
            }
                        
        }
        
    }
    
    public IEnumerator Attack1()// 한 손 슬래쉬
    {
        enemyMove.QuickLook();
        enemyStat.ChangeisAttackfalse();
        isAttacking = true;
        
        animator.SetTrigger(animHash_Attack1);

        EventManager.Instance.EnemyaniEvent();//Attack1
               

        //yield return new WaitForEndOfFrame(); // 애니메이션 길이 만큼 기다리기
        yield return new WaitForSeconds(aniClip[0].length);
        isAttacking = false;


        yield return StartCoroutine(RandomAction(1));
        //StartCoroutine(RandomAction(1));


    }

    IEnumerator Attack2()// 반 자른 두손검
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

    IEnumerator Attack3()// 회전 베기1 // 칼위치를 조정할 필요 있음
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

    IEnumerator Attack4()// 회전 베기2
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

    IEnumerator SAttack1()// 검기 날리기
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

    IEnumerator SAttack2()// 두 손검킥 1
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
                Debug.Log("1번 이동");
                aIEnemy.ChangedenemyAi(EnemyAIis.idle);

                yield return new WaitForEndOfFrame();
                //yield return new WaitForFixedUpdate();
                //StartCoroutine(aIEnemy.AIStart());
                break;

            default:
                Debug.Log("다음 발차기 공격");
                //StartCoroutine(SAttack1());
                yield return StartCoroutine(SAttack3());
                break;
            
                
        }
    }

    IEnumerator SAttack3()// 두 손검킥 2
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

    IEnumerator SAttack4()// 3연속 베기
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
