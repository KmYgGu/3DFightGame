using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    private PlayerAttack playerAttack;
    [SerializeField]private AnimationClip attackAni;

    [SerializeField] private GameObject[] attackColl;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<PlayerAttack>(out playerAttack); // ���� ������Ʈ�� �ִ� ��ũ��Ʈ ��������
              
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
}
