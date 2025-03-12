using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    [SerializeField] private GameObject EnemyHitbox; // 플레이어 히트박스들의 부모 
    private Transform EnemyHitboxTransform;
    private EnemyHitBox EnemyHitBox;

    [SerializeField] private GameObject playerCharCon;// 플레이어 캐릭터 컨트롤러
    [SerializeField] private GameObject EnemyCharCon;// 플레이어 캐릭터 컨트롤러

    

    public delegate void EnemyDamaged(PlayerAttackBox PlayerHB);
    public static event EnemyDamaged EnemyDam;

    public delegate void EnemyGuard(PlayerAttackBox PlayerGB);
    public static event EnemyDamaged EnemyGad;

    [SerializeField] private PlayerStat playerStat;

    void Awake()
    {

        EnemyHitBox = EnemyHitbox.GetComponent<EnemyHitBox>();
        EnemyHitboxTransform = EnemyHitbox.transform;
    }

    
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Ground")) return;

        if (other.CompareTag("PlayerBody")) return;

        if (other.gameObject == EnemyCharCon) return;

        if (other.gameObject == playerCharCon) return;



        if (!playerStat.isattack)
        {
            playerStat.ChangeisAttacktrue();

            if (other.CompareTag("Guard"))
            {


                EnemyHitBox.Defence();
                EnemyGad?.Invoke(this);

            }
            if (other.gameObject.transform.IsChildOf(EnemyHitboxTransform) && (!other.CompareTag("Guard")))
            {

                //EnemyHitBox.HitAniDamage();
                HitImpactManager.Instance.SpawnAttack(other.transform);// 맞은 곳에 임팩트 생성


                EnemyDam?.Invoke(this);
            }



        }


    }

}
