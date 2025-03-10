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

    //private bool isAttack = false;// 나중에 이건 공격을 선언할 때마다 초기화 되도록

    public delegate void PlayerDamaged(EnemyAttackBox EnemyHB);
    public static event PlayerDamaged PlayerDam;


    [SerializeField] private EnemyStat enemyStat;
    // Start is called before the first frame update
    void Start()
    {
        
        playerHitBox = playerHitbox.GetComponent<PlayerHitBox>();
        playerHitboxTransform = playerHitbox.transform;
    }
       

    private void OnTriggerEnter(Collider other)
    {
              

        if (other.CompareTag("Ground")) return;

        if (other.CompareTag("EnemyBody")) return;

        if (other.gameObject == playerCharCon) return;


        if (!enemyStat.isattack)
        {
            //isAttack = true;
            enemyStat.ChangeisAttacktrue();

            if (other.CompareTag("Guard"))
            {
                

                playerHitBox.Defence();

            }
            if (other.gameObject.transform.IsChildOf(playerHitboxTransform)&&(!other.CompareTag("Guard")))
            {

                //playerHitBox.HitAniDamage(gameObject);

                PlayerDam?.Invoke(this);


            }
                        

            
        }
        
                
    }
}
