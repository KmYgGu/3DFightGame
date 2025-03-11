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

    private int animHash_walk = Animator.StringToHash("isWalk");
    private int animHash_Run = Animator.StringToHash("isRun");

    private int animHash_Guard = Animator.StringToHash("isGuardSucess");

    [SerializeField] private EnemyStat enemyStat;

    private ParticleSystem ps;


    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
        
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
                animator.SetBool(animHash_walk, false);
                animator.SetBool(animHash_Run, false);


                animator.SetTrigger(animHash_Damage1);

                EventManager.Instance.TriggerEvent();//Damage
                Debug.Log($"���� ������ ���� �ִϸ��̼��� {enemyStat.aniState}");
                break;

        }

    }
    

    private void OnTriggerEnter(Collider other)// �÷��̾� ���� �ݶ��̴��� ���ļ� �浹���� �ʰ� ����
    {
        
        if (other.CompareTag("Ground")) return;

        if (other.CompareTag("PlayerBody")) return;

        if (other.CompareTag("EnemyBody")) return;

        if (other.CompareTag("Guard")) return;

        if (PlayerDefenceBox.isDenfence)
        {
            Debug.Log("���� �浹 ���� (���а� ���� �浹��)");
            return;
        }
        //else
        //{
        //    Debug.Log("���� ����");
        //}

        //Debug.Log(other.gameObject.name);

    }
    


    public void Defence()
    {
        animator.SetTrigger(animHash_Guard);
        ps.Play();
        Debug.Log("����");  
    }

}
