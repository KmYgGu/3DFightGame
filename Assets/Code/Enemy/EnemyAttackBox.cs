using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
 
    [SerializeField] private GameObject playerHitbox; // 플레이어 히트박스들의 부모 
    private Transform playerHitboxTransform;
    private PlayerHitBox playerHitBox;

    [SerializeField] private GameObject playerCharCon;// 플레이어 캐릭터 컨트롤러
    [SerializeField] private GameObject EnemyCharCon;// 플레이어 캐릭터 컨트롤러

    //private bool isAttack = false;// 나중에 이건 공격을 선언할 때마다 초기화 되도록

    public delegate void PlayerDamaged(EnemyAttackBox EnemyHB);
    public static event PlayerDamaged PlayerDam;


    [SerializeField] private EnemyStat enemyStat;

    [SerializeField] private PlayerStat playerStat;
    // Start is called before the first frame update
    void Awake()
    {
        
        playerHitBox = playerHitbox.GetComponent<PlayerHitBox>();
        playerHitboxTransform = playerHitbox.transform;
    }
       

    private void OnTriggerEnter(Collider other)
    {
              

        if (other.CompareTag("Ground")) return;

        if (other.CompareTag("EnemyBody")) return;

        if (other.gameObject == playerCharCon) return;

        if (other.gameObject == EnemyCharCon) return;


        if (!enemyStat.isattack)
        {
            //isAttack = true;
            enemyStat.ChangeisAttacktrue();

            if (playerStat.ISGuarding)
            {
                Vector3 attackDirection = (EnemyCharCon.transform.position - playerCharCon.transform.position).normalized;
                float dot = Vector3.Dot(playerCharCon.transform.forward, attackDirection);

                if (dot > 0)
                {
                    //Debug.Log("앞면 공격!");
                    playerHitBox.Defence();
                }
                else
                {
                    //Debug.Log("뒷면 공격!");
                    HitImpactManager.Instance.SpawnAttack(other.transform);// 맞은 곳에 임팩트 생성
                    PlayerDam?.Invoke(this);
                }
            }
            else
            {
                HitImpactManager.Instance.SpawnAttack(other.transform);// 맞은 곳에 임팩트 생성
                PlayerDam?.Invoke(this);
            }


            /*if (other.CompareTag("Guard"))
            {
                

                playerHitBox.Defence();
                return;

            }
            else if (other.gameObject.transform.IsChildOf(playerHitboxTransform) && (!other.CompareTag("Guard")))
            {

                HitImpactManager.Instance.SpawnAttack(other.transform);// 맞은 곳에 임팩트 생성
                //Debug.Log(other.gameObject.name);
                PlayerDam?.Invoke(this);

            }*/
            



        }
                        
    }

    IEnumerator CheckotherCoi(Collider other)
    {
        yield return new WaitForFixedUpdate(); // 한 프레임 대기
        //yield return new WaitForSeconds(0.5f);
        if (other.gameObject.transform.IsChildOf(playerHitboxTransform) && (!other.CompareTag("Guard")))
        {

            
            //Debug.Log(other.gameObject.name);
            PlayerDam?.Invoke(this);

        }
        else
        {
            yield return null;
        }
        
    }
}
