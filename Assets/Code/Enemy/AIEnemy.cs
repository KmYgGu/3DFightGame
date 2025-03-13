using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAIis
{
    idle,// �⺻ ���
    move,// �̵�
    canAttack,// ���� ������ �Ÿ�
    Attacking,// ������..
    fall, // �Ÿ��� ����
    Defense,// ���
    Damage,// ����
    escape,// ����
}


public class AIEnemy : MonoBehaviour
{
    [SerializeField]EnemyAIis enemyAIis = EnemyAIis.idle; // ó������ idle��

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

        // �۾��� ���� �� �ٽ� �ݺ�
        //StartCoroutine(AIStart());
    }

    public void ChangedenemyAi(EnemyAIis AIis)
    {
        enemyAIis = AIis;
    }

    public void EnemtDamageStopAI(PlayerAttackBox EnemyDam)// �������� �Ծ��� ��, ��� �ൿ ���� �޼ҵ�
    {
        Debug.Log("������ ���� �ߵ�");
        StopAllCoroutines();
        //StopCoroutine(AIStart());
        enemyAIis = EnemyAIis.Damage;
        StartCoroutine(AIStart());
    }

    public void EnemtGuardStopAI(PlayerAttackBox EnemyGad)// �ƹ����� �ִϸ��̼� ������ ���� ��� �ð� �ʿ��� ��
    {
        Debug.Log("������ ���� �ߵ�");
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
