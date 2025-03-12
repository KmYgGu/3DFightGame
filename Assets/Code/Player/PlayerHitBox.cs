using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
    
    [SerializeField] PlayerDefenceBox PlayerDefenceBox;
    [SerializeField] private GameObject DefenseColl;

    private int animHash_Damage1 = Animator.StringToHash("DamageS");
    private int animHash_Damage2 = Animator.StringToHash("DamageM");
    private int animHash_Damage4 = Animator.StringToHash("DamageF");

    private int animHash_walk = Animator.StringToHash("isWalk");
    private int animHash_Run = Animator.StringToHash("isRun");

    private int animHash_Guard = Animator.StringToHash("isGuardSucess");

    [SerializeField] private EnemyStat enemyStat;
    private PlayerStat playerStat;


    private ParticleSystem ps;


    private void Awake()
    {
        playerStat = GetComponentInParent<PlayerStat>();
        animator = GetComponentInParent<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
        
    }
    private void OnEnable()
    {
        EnemyAttackBox.PlayerDam += DamageAniPlay;

        EnemySwordWind.PlayerSWDam += DamageSWDAniPlay;
    }

    private void OnDisable()
    {
        EnemyAttackBox.PlayerDam -= DamageAniPlay;
        EnemySwordWind.PlayerSWDam -= DamageSWDAniPlay;
    }

    void DamageAniPlay(EnemyAttackBox PlayerDam)
    {
        switch (enemyStat.aniState)
        {

            case AnimationTag.Guard:
                break;
            default:
                animator.SetBool(animHash_walk, false);
                animator.SetBool(animHash_Run, false);

                if(playerStat.aniState == AnimationTag.Jump || playerStat.aniState == AnimationTag.JumpAttack)
                    animator.SetTrigger(animHash_Damage4);
                else
                {
                    animator.SetTrigger(animHash_Damage1);
                }

                
                EventManager.Instance.TriggerEvent();//Damage
                //Debug.Log($"현재 공격한 적의 애니메이션은 {enemyStat.aniState}");
                break;

        }

    }
    void DamageSWDAniPlay(EnemySwordWind SWD)// 검기 관련 피해
    {
        animator.SetBool(animHash_walk, false);
        animator.SetBool(animHash_Run, false);
        animator.SetTrigger(animHash_Damage2);
        EventManager.Instance.TriggerEvent();//Damage
    }



    private void OnTriggerEnter(Collider other)// 플레이어 몸의 콜라이더가 겹쳐서 충돌되지 않게 방지
    {
        
        if (other.CompareTag("Ground")) return;

        if (other.CompareTag("PlayerBody")) return;

        if (other.CompareTag("EnemyBody")) return;

        if (other.CompareTag("Guard")) return;

        if (PlayerDefenceBox.isDenfence)
        {
            Debug.Log("몸통 충돌 무시 (방패가 먼저 충돌함)");
            return;
        }
        

    }
    


    public void Defence()//방어 성공
    {
        animator.SetTrigger(animHash_Guard);
        ps.Play();
        DefenseColl.SetActive(false);//방어 성공시 방어판정 비활성화
        //Debug.Log("방어성공");  
    }

}
