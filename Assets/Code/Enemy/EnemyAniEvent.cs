using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAniEvent : MonoBehaviour
{
    [SerializeField] private GameObject[] attackColl;
    

    private void attackcolEnable(int number)// ���� �ڽ� ����� �̺�Ʈ �Լ�
    {
        switch (number)//Į ������ �ƴ� ��쿡�� Ư�� ���� Ȱ��ȭ
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
