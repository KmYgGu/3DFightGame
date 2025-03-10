using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
 
    [SerializeField] private GameObject playerHitbox; // �÷��̾� ��Ʈ�ڽ����� �θ� 
    private Transform playerHitboxTransform;
    private PlayerHitBox playerHitBox;

    [SerializeField] private GameObject playerCharCon;// �÷��̾� ĳ���� ��Ʈ�ѷ�

    //private bool isAttack = false;// ���߿� �̰� ������ ������ ������ �ʱ�ȭ �ǵ���

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
