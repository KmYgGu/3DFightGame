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

    void AttackCoiReset(EnemyAttackBox PlayerDam)// 만약 데미지를 입었을 경우, 바로 공격판정들을 끄기, 방어끄기, 공격 가능한 상태
    {
        foreach (GameObject obj in attackColl)
        {
            obj.SetActive(false);
        }
        DefenseColl.SetActive(false);
        playerAttack.canAttack = true;
        playerAttack.AttackClear();
    }

    private void aniEnent(int number)// 어택 박스 생기는 이벤트 함수
    {

        //attackAni = playerAttack.GetAnimationClip(number);
        //Debug.Log(attackAni.name);

        attackColl[number].SetActive(true);
    }

    private void attackcolDisable(int number)// 어택 박스 사라지는
    {

        //attackAni = playerAttack.GetAnimationClip(number);

        attackColl[number].SetActive(false);

        switch (number)//마지막 공격동작이 아닌 경우에만 다음 공격으로 빠르게 공격할 수 있음
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

    private void LastAttackDelay()//마지막 공격은 애니메이션이 다 끝나야 공격이 가능
    {
        //Debug.Log("마지막 공격끝");

        playerAttack.changeLastAttack();
        playerAttack.changeAttackCan();//공격이 가능하게 실행
    }

    public void isGroundjumpAttackCoi()//땅에 닿았으면 즉시 점프 공격판정을 꺼지게 하기
    {
        attackColl[8].SetActive(false);
    }

    
}
