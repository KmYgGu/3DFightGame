using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // ���� �÷��̾ ������ �¾Ҵ� ��, ���Ҵ� ���� ���� ���� �޺��� �޶����� ����

    private Animator animator;
    private int animHash_Attack1 = Animator.StringToHash("isAttack1");
    private int animHash_Attack2 = Animator.StringToHash("isAttack2");
    private int animHash_Attack3 = Animator.StringToHash("isAttack3");
    private int animHash_Attack4 = Animator.StringToHash("isAttack4");

    private int animHash_SAttack1 = Animator.StringToHash("isSAttack1");
    private int animHash_SAttack2 = Animator.StringToHash("isSAttack2");
    private int animHash_SAttack3 = Animator.StringToHash("isSAttack3");
    private int animHash_SAttack4 = Animator.StringToHash("isSAttack4");


    [SerializeField]private bool isAttacking = false;// �������̸� �ٸ� ������ �ȵǰ�

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<Animator>(out animator);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
            StartCoroutine("SAttack4");

    }

    IEnumerator Attack1()// �μհ� ű
    {
        isAttacking = true;

        animator.SetTrigger(animHash_Attack1);

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator Attack2()// �� �� ������
    {
        isAttacking = true;

        animator.SetTrigger(animHash_Attack2);

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator Attack3()// ȸ�� ����1 // Į��ġ�� ������ �ʿ� ����
    {
        isAttacking = true;

        animator.SetTrigger(animHash_Attack3);

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator Attack4()// ȸ�� ����2
    {
        isAttacking = true;

        animator.SetTrigger(animHash_Attack4);

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator SAttack1()// �˱� ������
    {
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack1);

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator SAttack2()// �� �հ�ű 1
    {
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack2);

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator SAttack3()// �� �հ�ű 2
    {
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack3);

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    IEnumerator SAttack4()// 3���� ����
    {
        isAttacking = true;

        animator.SetTrigger(animHash_SAttack4);

        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }
}
