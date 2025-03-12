using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAniEvent : MonoBehaviour
{
    private PlayerAttack playerAttack;
    [SerializeField] private GameObject[] attackColl;
    [SerializeField] private GameObject DefenseColl;

    private void Awake()
    {
        TryGetComponent<PlayerAttack>(out playerAttack);
    }

    private void OnEnable()
    {
        EnemyAttackBox.PlayerDam += AttackCoiReset;
    }

    private void OnDisable()
    {
        EnemyAttackBox.PlayerDam -= AttackCoiReset;
    }

    void AttackCoiReset(EnemyAttackBox PlayerDam)// ���� �������� �Ծ��� ���, �ٷ� ������������ ����, ������, ���� ������ ����
    {
        foreach (GameObject obj in attackColl)
        {
            obj.SetActive(false);
        }
        DefenseColl.SetActive(false);
        playerAttack.canAttack = true;
        playerAttack.AttackClear();
    }

    private void aniEnent(int number)// ���� �ڽ� ����� �̺�Ʈ �Լ�
    {

        //attackAni = playerAttack.GetAnimationClip(number);
        //Debug.Log(attackAni.name);

        attackColl[number].SetActive(true);
    }

    private void attackcolDisable(int number)// ���� �ڽ� �������
    {

        //attackAni = playerAttack.GetAnimationClip(number);

        attackColl[number].SetActive(false);

        switch (number)//������ ���ݵ����� �ƴ� ��쿡�� ���� �������� ������ ������ �� ����
        {
            case 3:
            case 7:

                break;

            case 9:
                playerAttack.changeLastAttack();
                break;
            default:
                playerAttack.changeAttackCan();
                break;
        }


    }

    private void LastAttackDelay()//������ ������ �ִϸ��̼��� �� ������ ������ ����
    {
        //Debug.Log("������ ���ݳ�");

        playerAttack.changeLastAttack();
        playerAttack.changeAttackCan();//������ �����ϰ� ����
    }

    public void isGroundjumpAttackCoi()//���� ������� ��� ���� ���������� ������ �ϱ�
    {
        attackColl[8].SetActive(false);
    }

    
}
