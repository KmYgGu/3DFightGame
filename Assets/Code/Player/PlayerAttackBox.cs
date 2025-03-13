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
    [SerializeField] private EnemyStat enemyStat;

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

            /*if (other.CompareTag("Guard"))
            {


                EnemyHitBox.Defence();
                EnemyGad?.Invoke(this);

            }
            if (other.gameObject.transform.IsChildOf(EnemyHitboxTransform) && (!other.CompareTag("Guard")))
            {

                //EnemyHitBox.HitAniDamage();
                HitImpactManager.Instance.SpawnAttack(other.transform);// 맞은 곳에 임팩트 생성


                EnemyDam?.Invoke(this);
            }*/
            if (enemyStat.ISGuarding)
            {
                Vector3 attackDirection = (playerCharCon.transform.position - EnemyCharCon.transform.position).normalized;
                float dot = Vector3.Dot(EnemyCharCon.transform.forward, attackDirection);

                if (dot > 0)
                {
                    int randomValue = Random.Range(1, 3);//절반의 확률로 방어
                    switch (randomValue)
                    {
                        case 1:
                            //Debug.Log("적 방어");
                            EnemyHitBox.Defence();
                            EnemyGad?.Invoke(this);
                            break;

                        case 2:
                            HitImpactManager.Instance.SpawnAttack(other.transform);// 맞은 곳에 임팩트 생성
                            EnemyDam?.Invoke(this);
                            break;
                        default:
                            break;
                    }
                                        
                    //EnemyHitBox.Defence();
                    //EnemyGad?.Invoke(this);
                }
                else
                {
                    //Debug.Log("뒷면 공격!");
                    HitImpactManager.Instance.SpawnAttack(other.transform);// 맞은 곳에 임팩트 생성
                    EnemyDam?.Invoke(this);
                }
            }
            else
            {
                HitImpactManager.Instance.SpawnAttack(other.transform);// 맞은 곳에 임팩트 생성
                EnemyDam?.Invoke(this);
            }




        }


    }

}
