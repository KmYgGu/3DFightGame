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
    [SerializeField]EnemyAIis enemyAIis = EnemyAIis.idle; // 처음에는 idle로

    private EnemyMove move;
    private EnemyAttack attack;
    private EnemyStat stat;
    private EnemyDefence defence;
    // Start is called before the first frame update


    void Awake()
    {
        TryGetComponent<EnemyMove>(out move);
        attack = GetComponentInChildren<EnemyAttack>();
        TryGetComponent<EnemyStat>(out stat);
        defence = GetComponentInChildren<EnemyDefence>();

        //AIStart();
    }
    private void Start()
    {
        //StartCoroutine(AIStart());
        
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

    public IEnumerator AIStart()
    {
        
        switch (enemyAIis)
        {
            case EnemyAIis.idle:
                yield return StartCoroutine(move.EnemyAlMove());
                break;
            case EnemyAIis.canAttack:
                //if(stat.aniState == AnimationTag.Idle)
                yield return StartCoroutine(attack.AttackChoice());
                break;
            case EnemyAIis.Damage:
                yield return StartCoroutine(defence.GuardStart());
                break;
            default:
                break;
        }

        yield return new WaitForEndOfFrame();
        //yield return new WaitForSeconds(1f);

        // 작업이 끝난 후 다시 반복
        //StartCoroutine(AIStart());
    }

    public void ChangedenemyAi(EnemyAIis AIis)
    {
        enemyAIis = AIis;
    }

    public void EnemtDamageStopAI(PlayerAttackBox EnemyDam)// 데미지를 입었을 때, 잠시 행동 중지 메소드
    {
        Debug.Log("데미지 입음 발동");
        StopAllCoroutines();
        //StopCoroutine(AIStart());
        enemyAIis = EnemyAIis.Damage;
        StartCoroutine(AIStart());
    }

    public void EnemtGuardStopAI(PlayerAttackBox EnemyGad)// 아무래도 애니메이션 나오는 동안 대기 시간 필요할 것
    {
        Debug.Log("데미지 입음 발동");
        StopAllCoroutines();
        //StopCoroutine(AIStart());
        enemyAIis = EnemyAIis.Damage;
        StartCoroutine(AIStart());
    }

    public void StopMove()
    {
        StopAllCoroutines();
    }
}
