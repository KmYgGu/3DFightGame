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

    private bool isAttack = false;// 나중에 이건 공격을 선언할 때마다 초기화 되도록


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
            Debug.Log("HashSet 초기화됨.");
        }
    }

    /*private void attackcolEnable(int number)// 어택 박스 생기는 이벤트 함수
    {
        switch (number)//칼 공격이 아닌 경우에만 특정 판정 활성화
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

    private void attackcolDisable(int number)// 어택 박스 사라지는 이벤트 함수
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

        // 이미 충돌했던 오브젝트라면 무시
        //if (collidedObjects.Contains(other.gameObject)) return;
        //collidedObjects.Add(other.gameObject);

        if (!isAttack)
        {
            isAttack = true;

            if (other.CompareTag("Guard"))
            {
                //Debug.Log("상대가 가드함, 이 애니메이션의 공격판정은 사라짐");
                //attackColl[attackNumber].SetActive(false);// 1대1이라서 가능한 방법

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
