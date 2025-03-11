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
    [SerializeField] private GameObject EnemyCharCon;// �÷��̾� ĳ���� ��Ʈ�ѷ�

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

        if (other.gameObject == EnemyCharCon) return;


        if (!enemyStat.isattack)
        {
            //isAttack = true;
            enemyStat.ChangeisAttacktrue();

            //Debug.Log(other.gameObject.name);
            if (other.CompareTag("Guard"))
            {
                

                playerHitBox.Defence();
                return;

            }
            else if (other.gameObject.transform.IsChildOf(playerHitboxTransform) && (!other.CompareTag("Guard")))
            {


                Debug.Log(other.gameObject.name);
                PlayerDam?.Invoke(this);

            }
            



        }
                        
    }

    IEnumerator CheckotherCoi(Collider other)
    {
        yield return new WaitForFixedUpdate(); // �� ������ ���
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
