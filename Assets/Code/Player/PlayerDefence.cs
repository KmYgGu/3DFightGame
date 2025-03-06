using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefence : MonoBehaviour
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
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            //CharAni.ResetTrigger(animHash_Gurad);
            CharAni.ResetTrigger(animHash_GuradUP);
        }

        if (Input.GetButton("Fire2"))// SŰ ��ư
        {
            CharAni.SetTrigger(animHash_Gurad);
            EventManager.Instance.TriggerEvent();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            GuardCol.SetActive(false);//���� ��Ÿ�ϸ� ���尡 �ٷ� ������� ��, �̸� ��� �õ��غ��� �����ִ� �� ���θ� ����
            CharAni.ResetTrigger(animHash_Gurad);
            CharAni.SetTrigger(animHash_GuradUP);
            
        }
    }

    private void StartGuard()// ���� �̺�Ʈ ���� ȣ��
    {
        Debug.Log("���� Ȱ��ȭ");
        GuardCol.SetActive(true);

    }

    private void EndGuard()// ���� �̺�Ʈ ���� ȣ��
    {
        Debug.Log("���尡 Ǯ��");
        GuardCol.SetActive(false);

    }


}
