using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] PlayerDefenceBox PlayerDefenceBox;

    private int animHash_Damage1 = Animator.StringToHash("DamageS");
    private int animHash_Damage2 = Animator.StringToHash("DamageM");
    private int animHash_Damage4 = Animator.StringToHash("DamageF");

    [SerializeField] private PlayerStat playerStat;

    //private int animHash_Guard = Animator.StringToHash("isGuardSucess");
    
    private void OnEnable()
    {
       

        PlayerAttackBox.EnemyDam += DamageAniPlay;
    }

    private void OnDisable()
    {
            

        PlayerAttackBox.EnemyDam -= DamageAniPlay;
    }

    private void Start()
    {
        animator = GetComponentInParent<Animator>();

    }

    private void OnTriggerEnter(Collider other)// 플레이어 몸의 콜라이더가 겹쳐서 충돌되지 않게 방지
    {

        if (other.CompareTag("Ground")) return;

        if (other.CompareTag("PlayerBody")) return;

        if (other.CompareTag("EnemyBody")) return;

        if (other.CompareTag("Guard")) return;

        if (PlayerDefenceBox.isDenfence) return;

    }

    void DamageAniPlay(PlayerAttackBox EnemyDam)
    {
        switch (playerStat.aniState)
        {

            case AnimationTag.Guard:
                break;
            default:
                animator.SetTrigger(animHash_Damage1);
                EventManager.Instance.EnemyaniEvent();
                //Debug.Log(playerStat.aniState);
                break;

        }
        //animator.SetTrigger(animHash_Damage1);

        //EventManager.Instance.TriggerEvent();//Damage
    }

    public void HitAniDamage()
    {
        
        switch (playerStat.aniState)
        {
            
            case AnimationTag.Guard :
            break;
            default :
                animator.SetTrigger(animHash_Damage1);
                Debug.Log(playerStat.aniState);
                break;

        }
        
        
        //animator.SetTrigger(animHash_Damage1);
        //EventManager.Instance.TriggerEvent();//Damage
    }

    public void Defence()
    {
        //animator.SetTrigger(animHash_Guard); // 아직 상대방은 방어 모션이 없음
        //Debug.Log("방어성공");
    }
}
