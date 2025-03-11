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

        // �۾��� ���� �� �ٽ� �ݺ�
        StartCoroutine(AIStart());
    }

    public void ChangedenemyAi(EnemyAIis AIis)
    {
        enemyAIis = AIis;
    }

    public void StopAI()// �������� �Ծ��� ��, ��� �ൿ ���� �޼ҵ�
    {
        StopCoroutine(AIStart());
    }

    public void DoAi()// �ٽ� �������� �簳
    {
        StartCoroutine(AIStart());
    }
}
