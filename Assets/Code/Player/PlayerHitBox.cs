using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private bool isInvulnerable = false; // ���� �ð� ���� ���� ���� üũ

    [SerializeField] private Animator animator;
    private PlayerStat stat;

    private int animHash_Damage1 = Animator.StringToHash("DamageS");
    private int animHash_Damage2 = Animator.StringToHash("DamageM");
    private int animHash_Damage4 = Animator.StringToHash("DamageF");

    private int animHash_Guard = Animator.StringToHash("isGuardSucess");

    // �浹�� ������Ʈ�� ������ HashSet
    //private HashSet<GameObject> collidedObjects = new HashSet<GameObject>();

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        stat = GetComponentInParent<PlayerStat>();
    }

    private void OnTriggerEnter(Collider other)// �÷��̾� ���� �ݶ��̴��� ���ļ� �浹���� �ʰ� ����
    {
        
        if (other.CompareTag("Ground")) return;

        if (other.CompareTag("PlayerBody")) return;

        if (other.CompareTag("Guard")) return;

        if (isInvulnerable) return;

        // �̹� �浹�ߴ� ������Ʈ��� ����
        if (stat.collidedObjects.Contains(other.gameObject)) return;


        Debug.Log(other.gameObject.name);
        stat.collidedObjects.Add(other.gameObject);

        //Debug.Log("�¾Ҵ�");

        //StartCoroutine(ActivateInvulnerability());
    }


    private IEnumerator ActivateInvulnerability()
    {
        isInvulnerable = true; // ���� ���� Ȱ��ȭ

        yield return new WaitForSeconds(0.5f); // 0.5�� ���� ���� ����

        //yield return new WaitForEndOfFrame();
        isInvulnerable = false; // �ٽ� ���� ���� �� �ֵ��� ����
    }

    public void HitAniDamage()
    {

        animator.SetTrigger(animHash_Damage1);
        EventManager.Instance.TriggerEvent();//Damage
    }

    public void Defence()
    {
        animator.SetTrigger(animHash_Guard);
        Debug.Log("����");  
    }
}
