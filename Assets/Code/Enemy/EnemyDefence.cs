using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefence : MonoBehaviour
{
    private Animator CharAni;
    private AIEnemy aIEnemy;
    private EnemyStat enemyStat;
    private EnemyMove enemyMove;

    private int animHash_Gurad = Animator.StringToHash("isGuard");
    private int animHash_GuradUP = Animator.StringToHash("isGuardUp");

    //[SerializeField] private GameObject GuardCol;

    [SerializeField] private AnimationClip[] aniClip;

    public bool GameEnd;// = false;

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent<Animator>(out CharAni);
        aIEnemy = GetComponentInParent<AIEnemy>();
        enemyStat = GetComponentInParent<EnemyStat>();
        enemyMove = GetComponentInParent<EnemyMove>();
    }

    private void OnEnable()
    {
        PlayerAttackBox.EnemyDam += EnemtDamageStopAI;
        PlayerAttackBox.EnemyGad += EnemtGuardStopAI;

        EventManager.Instance.PlayerDied += StopMove;
        EventManager.Instance.EnemyDied += StopMove;
    }

    private void OnDisable()
    {
        PlayerAttackBox.EnemyDam -= EnemtDamageStopAI;
        PlayerAttackBox.EnemyGad -= EnemtGuardStopAI;

        EventManager.Instance.PlayerDied -= StopMove;
        EventManager.Instance.EnemyDied -= StopMove;
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            //CharAni.ResetTrigger(animHash_Gurad);
            CharAni.ResetTrigger(animHash_GuradUP);
        }

        if (Input.GetButton("Fire2"))// S키 버튼
        {
            

            CharAni.SetTrigger(animHash_Gurad);
            EventManager.Instance.EnemyaniEvent();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            
            StartCoroutine("GuardDisable");

        }
    }*/

    public IEnumerator GuardStart()
    {
        Debug.Log("적이 방어함");
        CharAni.ResetTrigger(animHash_GuradUP);
        CharAni.SetTrigger(animHash_Gurad);
        EventManager.Instance.EnemyaniEvent();

        aIEnemy.ChangedenemyAi(EnemyAIis.idle);
        yield return new WaitForSeconds(aniClip[0].length/2);
        if (!GameEnd)
        {
            if (enemyMove.DistanceCheck())//방어 중인데 거리 안에 있는지 체크
            {
                yield return new WaitForSeconds(aniClip[0].length / 2);// 방어 지속
            }
            else
            {
                CharAni.SetTrigger(animHash_GuradUP);
                enemyStat.ISGuarding = false;
            }

            StartCoroutine(aIEnemy.AIStart());
        }
        
    }

    IEnumerator GuardDisable()
    {
        StopCoroutine(GuardDisable());

        yield return new WaitForSeconds(0.05f);

        //GuardCol.SetActive(false);//빨리 연타하면 가드가 바로 사라지는 데, 이를 몇번 시도해보고 남아있는 지 여부를 설정
        enemyStat.ISGuarding = false;

        CharAni.ResetTrigger(animHash_Gurad);
        CharAni.SetTrigger(animHash_GuradUP);

        CharAni.SetBool("isWalk", false);
        CharAni.ResetTrigger("isGuardSucess");
    }

    private void StartGuard()// 가드 이벤트 상태 호출
    {
        //Debug.Log("상대 가드 활성화");
        //GuardCol.SetActive(true);
        enemyStat.ISGuarding = true;

        

    }

    private void EndGuard()// 가드 이벤트 상태 호출
    {
        //Debug.Log("상대 가드가 풀림");
        //GuardCol.SetActive(false);
        enemyStat.ISGuarding = false;

        

    }

    public void EnemtDamageStopAI(PlayerAttackBox EnemyDam)// 데미지를 입으면, 모든 코루틴 중지
    {

        StopAllCoroutines();

    }

    public void EnemtGuardStopAI(PlayerAttackBox EnemyGad)// 데미지를 입었을 때, 잠시 행동 중지 메소드
    {
        StopAllCoroutines();
    }

    void StopMove()
    {
        GameEnd = true;
        StopAllCoroutines();
    }
}
