using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAIis
{
    idle,// 기본 대기
    move,// 이동
    canAttack,// 공격 가능한 거리
    Attacking,// 공격중..
    fall, // 거리가 멀음
    Defense,// 방어
    Damage,// 피해
    escape,// 도망
}


public class AIEnemy : MonoBehaviour
{
    EnemyAIis enemyAIis = EnemyAIis.idle;

    private EnemyMove move;
    private EnemyAttack attack;
    private EnemyStat stat;
    // Start is called before the first frame update


    void Awake()
    {
        TryGetComponent<EnemyMove>(out move);
        attack = GetComponentInChildren<EnemyAttack>();
        TryGetComponent<EnemyStat>(out stat);

        //AIStart();
    }
    private void Start()
    {
        StartCoroutine(AIStart());
        
    }


    private IEnumerator AIStart()
    {
        
        switch (enemyAIis)
        {
            case EnemyAIis.idle:
                yield return StartCoroutine(move.EnemyAlMove());
                break;
            case EnemyAIis.canAttack:
                if(stat.aniState == AnimationTag.Idle)
                yield return StartCoroutine(attack.AttackChoice());
                break;
            default:
                break;
        }

        yield return new WaitForEndOfFrame();
        //yield return new WaitForSeconds(1f);

        // 작업이 끝난 후 다시 반복
        StartCoroutine(AIStart());
    }

    public void ChangedenemyAi(EnemyAIis AIis)
    {
        enemyAIis = AIis;
    }

    public void StopAI()// 데미지를 입었을 때, 잠시 행동 중지 메소드
    {
        StopCoroutine(AIStart());
    }

    public void DoAi()// 다시 움직임을 재개
    {
        StartCoroutine(AIStart());
    }
}
