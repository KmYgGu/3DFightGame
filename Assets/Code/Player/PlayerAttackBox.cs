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
        TryGetComponent<PlayerAttack>(out playerAttack); // 같은 오브젝트에 있는 스크립트 가져오기
              
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
}
