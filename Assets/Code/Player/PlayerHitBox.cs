using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private bool isInvulnerable = false; // ���� �ð� ���� ���� ���� üũ

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
        isInvulnerable = true; // ���� ���� Ȱ��ȭ

        yield return new WaitForSeconds(0.2f); // 0.5�� ���� ���� ����

        //yield return new WaitForEndOfFrame();
        isInvulnerable = false; // �ٽ� ���� ���� �� �ֵ��� ����
    }
}
