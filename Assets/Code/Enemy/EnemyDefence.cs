using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefence : MonoBehaviour
{
    private Animator CharAni;

    private int animHash_Gurad = Animator.StringToHash("isGuard");
    private int animHash_GuradUP = Animator.StringToHash("isGuardUp");

    [SerializeField] private GameObject GuardCol;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<Animator>(out CharAni);
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            //CharAni.ResetTrigger(animHash_Gurad);
            CharAni.ResetTrigger(animHash_GuradUP);
        }

        if (Input.GetButton("Fire2"))// SŰ ��ư
        {
            

            CharAni.SetTrigger(animHash_Gurad);
            EventManager.Instance.EnemyaniEvent();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            
            StartCoroutine("GuardDisable");

        }
    }*/

    IEnumerator GuardDisable()
    {
        StopCoroutine(GuardDisable());

        yield return new WaitForSeconds(0.05f);

        GuardCol.SetActive(false);//���� ��Ÿ�ϸ� ���尡 �ٷ� ������� ��, �̸� ��� �õ��غ��� �����ִ� �� ���θ� ����
        CharAni.ResetTrigger(animHash_Gurad);
        CharAni.SetTrigger(animHash_GuradUP);

        CharAni.SetBool("isWalk", false);
        CharAni.ResetTrigger("isGuardSucess");
    }

    private void StartGuard()// ���� �̺�Ʈ ���� ȣ��
    {
        //Debug.Log("���� Ȱ��ȭ");
        GuardCol.SetActive(true);

    }

    private void EndGuard()// ���� �̺�Ʈ ���� ȣ��
    {
        //Debug.Log("���尡 Ǯ��");
        GuardCol.SetActive(false);

    }
}
