using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAniEvent : MonoBehaviour
{
    [SerializeField] private GameObject[] attackColl;
    

    private void attackcolEnable(int number)// 어택 박스 생기는 이벤트 함수
    {
        switch (number)//칼 공격이 아닌 경우에만 특정 판정 활성화
        {
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
