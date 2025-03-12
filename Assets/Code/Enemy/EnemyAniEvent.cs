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

    void AttackCoiReset(PlayerAttackBox EnemyDam)// 만약 데미지를 입었을 경우, 바로 공격판정들을 끄기
    {
        foreach (GameObject obj in attackColl)
        {
            obj.SetActive(false);
        }
        DefenseColl.SetActive(false);
    }

    private void attackcolEnable(int number)// 어택 박스 생기는 이벤트 함수
    {
        //enemyStat.ChangeisAttackfalse(); // 공격박스가 생성될 때가 아닌 공격을 시작하자마자 바꾸는 걸로 해줘야됨
        switch (number)//칼 공격이 아닌 경우에만 특정 판정 활성화
        {
            case 5:// 연속 칼 공격
                enemyStat.ChangeisAttackfalse();
                attackColl[0].SetActive(true);
                break;
            case 6: // 연속 칼 공격
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

    private void attackcolDisable(int number)// 어택 박스 사라지는 이벤트 함수
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
