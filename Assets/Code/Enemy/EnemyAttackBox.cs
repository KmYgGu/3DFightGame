using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
    
    //[SerializeField] private GameObject[] attackColl;
    //private int attackNumber = 0;

    //[SerializeField]private HashSet<GameObject> collidedObjects = new HashSet<GameObject>();

    [SerializeField] private GameObject playerHitbox;
    private Transform playerHitboxTransform;
    private PlayerHitBox playerHitBox;

    private bool isAttack = false;// ���߿� �̰� ������ ������ ������ �ʱ�ȭ �ǵ���


    // Start is called before the first frame update
    void Start()
    {
        
        playerHitBox = playerHitbox.GetComponent<PlayerHitBox>();
        playerHitboxTransform = playerHitbox.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //collidedObjects.Clear();
            isAttack = false;
            Debug.Log("HashSet �ʱ�ȭ��.");
        }
    }

    /*private void attackcolEnable(int number)// ���� �ڽ� ����� �̺�Ʈ �Լ�
    {
        switch (number)//Į ������ �ƴ� ��쿡�� Ư�� ���� Ȱ��ȭ
        {
            case 1:
                attackNumber = number;
                attackColl[number].SetActive(true);
                break;
            default:
                attackNumber = 0;
                attackColl[0].SetActive(true);
                break;
        }
          
    }

    private void attackcolDisable(int number)// ���� �ڽ� ������� �̺�Ʈ �Լ�
    {
        switch (number)
        {
            case 1:
                attackColl[number].SetActive(false);
                break;
            default:
                attackColl[0].SetActive(false);
                break;
        }

    }*/

    private void OnTriggerEnter(Collider other)
    {
              

        if (other.CompareTag("Ground")) return;

        if (other.CompareTag("PlayerBody")) return;

        //if (other.CompareTag("Guard")) return;

        // �̹� �浹�ߴ� ������Ʈ��� ����
        //if (collidedObjects.Contains(other.gameObject)) return;
        //collidedObjects.Add(other.gameObject);

        if (!isAttack)
        {
            isAttack = true;

            if (other.CompareTag("Guard"))
            {
                //Debug.Log("��밡 ������, �� �ִϸ��̼��� ���������� �����");
                //attackColl[attackNumber].SetActive(false);// 1��1�̶� ������ ���

                playerHitBox.Defence();

            }
            if (other.gameObject.transform.IsChildOf(playerHitboxTransform)&&(!other.CompareTag("Guard")))
            {
                
                playerHitBox.HitAniDamage();

                //Debug.Log(other.gameObject.name);
            }
                        

            
        }
        
                
    }
}
