using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private bool isInvulnerable = false; // 일정 시간 동안 무적 여부 체크

    [SerializeField] private Animator animator;
    private PlayerStat stat;

    private int animHash_Damage1 = Animator.StringToHash("DamageS");
    private int animHash_Damage2 = Animator.StringToHash("DamageM");
    private int animHash_Damage4 = Animator.StringToHash("DamageF");

    private int animHash_Guard = Animator.StringToHash("isGuardSucess");

    // 충돌한 오브젝트를 저장할 HashSet
    //private HashSet<GameObject> collidedObjects = new HashSet<GameObject>();

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        stat = GetComponentInParent<PlayerStat>();
    }

    private void OnTriggerEnter(Collider other)// 플레이어 몸의 콜라이더가 겹쳐서 충돌되지 않게 방지
    {
        
        if (other.CompareTag("Ground")) return;

        if (other.CompareTag("PlayerBody")) return;

        if (other.CompareTag("Guard")) return;

        if (isInvulnerable) return;

        // 이미 충돌했던 오브젝트라면 무시
        if (stat.collidedObjects.Contains(other.gameObject)) return;


        Debug.Log(other.gameObject.name);
        stat.collidedObjects.Add(other.gameObject);

        //Debug.Log("맞았다");

        //StartCoroutine(ActivateInvulnerability());
    }


    private IEnumerator ActivateInvulnerability()
    {
        isInvulnerable = true; // 무적 상태 활성화

        yield return new WaitForSeconds(0.5f); // 0.5초 동안 공격 무시

        //yield return new WaitForEndOfFrame();
        isInvulnerable = false; // 다시 공격 받을 수 있도록 설정
    }

    public void HitAniDamage()
    {

        animator.SetTrigger(animHash_Damage1);
        EventManager.Instance.TriggerEvent();//Damage
    }

    public void Defence()
    {
        animator.SetTrigger(animHash_Guard);
        Debug.Log("방어성공");  
    }
}
