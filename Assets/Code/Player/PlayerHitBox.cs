using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
    
    [SerializeField] PlayerDefenceBox PlayerDefenceBox;

    private int animHash_Damage1 = Animator.StringToHash("DamageS");
    private int animHash_Damage2 = Animator.StringToHash("DamageM");
    private int animHash_Damage4 = Animator.StringToHash("DamageF");

    private int animHash_Guard = Animator.StringToHash("isGuardSucess");

    [SerializeField] private EnemyStat enemyStat;


    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        
    }
    private void OnEnable()
    {
        EnemyAttackBox.PlayerDam += DamageAniPlay;
    }

    private void OnDisable()
    {
        EnemyAttackBox.PlayerDam -= DamageAniPlay;
    }

    void DamageAniPlay(EnemyAttackBox PlayerDam)
    {
        switch (enemyStat.aniState)
        {

            case AnimationTag.Guard:
                break;
            default:
                animator.SetTrigger(animHash_Damage1);
                EventManager.Instance.TriggerEvent();//Damage
                Debug.Log($"현재 공격한 적의 애니메이션은 {enemyStat.aniState}");
                break;

        }

    }
    

    private void OnTriggerEnter(Collider other)// 플레이어 몸의 콜라이더가 겹쳐서 충돌되지 않게 방지
    {
        
        if (other.CompareTag("Ground")) return;

        if (other.CompareTag("PlayerBody")) return;

        if (other.CompareTag("EnemyBody")) return;

        if (other.CompareTag("Guard")) return;

        if (PlayerDefenceBox.isDenfence)
        {
            //Debug.Log("몸통 충돌 무시 (방패가 먼저 충돌함)");
            return;
        }
        
        //Debug.Log(other.gameObject.name);
               
    }


    public void Defence()
    {
        animator.SetTrigger(animHash_Guard);
        Debug.Log("방어성공");  
    }

}
