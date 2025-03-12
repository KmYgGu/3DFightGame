using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAniEvent : MonoBehaviour
{
    [SerializeField] private GameObject[] attackColl;
    [SerializeField] private GameObject DefenseColl;

    [SerializeField] private EnemyStat enemyStat;

    private void OnEnable()
    {
        PlayerAttackBox.EnemyDam += AttackCoiReset;
    }

    private void OnDisable()
    {
        PlayerAttackBox.EnemyDam -= AttackCoiReset;
    }

    void AttackCoiReset(PlayerAttackBox EnemyDam)// ���� �������� �Ծ��� ���, �ٷ� ������������ ����
    {
        foreach (GameObject obj in attackColl)
        {
            obj.SetActive(false);
        }
        DefenseColl.SetActive(false);
    }

    private void attackcolEnable(int number)// ���� �ڽ� ����� �̺�Ʈ �Լ�
    {
        //enemyStat.ChangeisAttackfalse(); // ���ݹڽ��� ������ ���� �ƴ� ������ �������ڸ��� �ٲٴ� �ɷ� ����ߵ�
        switch (number)//Į ������ �ƴ� ��쿡�� Ư�� ���� Ȱ��ȭ
        {
            case 5:// ���� Į ����
                enemyStat.ChangeisAttackfalse();
                attackColl[0].SetActive(true);
                break;
            case 6: // ���� Į ����
                enemyStat.ChangeisAttackfalse();
                attackColl[0].SetActive(true);
                break;
            case 7:
                attackColl[1].SetActive(true);
                break;
            case 8:
                attackColl[2].SetActive(true);
                break;
            default:
                attackColl[0].SetActive(true);
                break;
        }

    }

    private void attackcolDisable(int number)// ���� �ڽ� ������� �̺�Ʈ �Լ�
    {
        switch (number)
        {
            case 7:
                attackColl[1].SetActive(false);
                break;
            case 8:
                attackColl[2].SetActive(false);
                break;
            default:
                attackColl[0].SetActive(false);
                break;
        }

    }
}
