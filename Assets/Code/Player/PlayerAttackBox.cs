using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    [SerializeField] private GameObject EnemyHitbox; // �÷��̾� ��Ʈ�ڽ����� �θ� 
    private Transform EnemyHitboxTransform;
    private EnemyHitBox EnemyHitBox;

    [SerializeField] private GameObject EnemyCharCon;// �÷��̾� ĳ���� ��Ʈ�ѷ�

    //private bool isAttack = false;// ���߿� �̰� ������ ������ ������ �ʱ�ȭ �ǵ���

    public delegate void EnemyDamaged(PlayerAttackBox PlayerHB);
    public static event EnemyDamaged EnemyDam;

    [SerializeField] private PlayerStat playerStat;

    void Start()
    {

        EnemyHitBox = EnemyHitbox.GetComponent<EnemyHitBox>();
        EnemyHitboxTransform = EnemyHitbox.transform;
    }

    
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Ground")) return;

        if (other.CompareTag("PlayerBody")) return;

        if (other.gameObject == EnemyCharCon) return;

        // �̹� �浹�ߴ� ������Ʈ��� ����
        //if (collidedObjects.Contains(other.gameObject)) return;
        //collidedObjects.Add(other.gameObject);

        if (!playerStat.isattack)
        {
            playerStat.ChangeisAttacktrue();

            if (other.CompareTag("Guard"))
            {


                EnemyHitBox.Defence();

            }
            if (other.gameObject.transform.IsChildOf(EnemyHitboxTransform) && (!other.CompareTag("Guard")))
            {

                //EnemyHitBox.HitAniDamage();

                //Debug.Log(other.gameObject.name);

                EnemyDam?.Invoke(this);
            }



        }


    }

}
