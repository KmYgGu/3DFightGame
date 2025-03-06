using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private bool isInvulnerable = false; // 일정 시간 동안 무적 여부 체크

    [SerializeField] Animator animator;

    private int animHash_Damage1 = Animator.StringToHash("DamageS");
    private int animHash_Damage2 = Animator.StringToHash("DamageM");
    private int animHash_Damage4 = Animator.StringToHash("DamageF");

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Ground")) return;

        if (other.CompareTag("PlayerBody")) return;

        if (isInvulnerable) return;

        
        //Debug.Log(other.gameObject.name);


        animator.SetTrigger(animHash_Damage1);
        StartCoroutine(ActivateInvulnerability());
    }

    private IEnumerator ActivateInvulnerability()
    {
        isInvulnerable = true; // 무적 상태 활성화

        yield return new WaitForSeconds(0.2f); // 0.5초 동안 공격 무시

        //yield return new WaitForEndOfFrame();
        isInvulnerable = false; // 다시 공격 받을 수 있도록 설정
    }
}
