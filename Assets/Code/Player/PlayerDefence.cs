using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefence : MonoBehaviour
{
    private Animator CharAni;

    private int animHash_Gurad = Animator.StringToHash("isGuard");
    private int animHash_GuradUP = Animator.StringToHash("isGuardUp");

    private PlayerStat playerStat;
    //[SerializeField] private GameObject GuardCol;

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent<Animator>(out CharAni);
        playerStat = GetComponentInParent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerDefenceUpdate();
    }

    public void PlayerDefenceUpdate()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            //CharAni.ResetTrigger(animHash_Gurad);
            CharAni.ResetTrigger(animHash_GuradUP);
        }

        if (Input.GetButton("Fire2"))// SŰ ��ư
        {
            Vector3 moveArrow = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            float newAngle = Mathf.Atan2(moveArrow.x, moveArrow.z) * Mathf.Rad2Deg;
            float targetAngle = Mathf.Round(newAngle / 45.0f) * 45.0f; // 8���� ����

            if (moveArrow != Vector3.zero)
                transform.parent.rotation = Quaternion.AngleAxis(targetAngle, Vector3.up);

            CharAni.SetTrigger(animHash_Gurad);
            EventManager.Instance.TriggerEvent();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            /*GuardCol.SetActive(false);//���� ��Ÿ�ϸ� ���尡 �ٷ� ������� ��, �̸� ��� �õ��غ��� �����ִ� �� ���θ� ����
            CharAni.ResetTrigger(animHash_Gurad);
            CharAni.SetTrigger(animHash_GuradUP);

            CharAni.SetBool("isWalk", false);
            CharAni.ResetTrigger("isGuardSucess");*/

            StartCoroutine("GuardDisable");

        }
    }

    IEnumerator GuardDisable()
    {
        StopCoroutine(GuardDisable());

        yield return new WaitForEndOfFrame();
        //yield return new WaitForSeconds(0.05f);

        

            CharAni.ResetTrigger(animHash_Gurad);
            CharAni.SetTrigger(animHash_GuradUP);

            CharAni.SetBool("isWalk", false);
            CharAni.ResetTrigger("isGuardSucess");

        CharAni.SetBool("isRun", false);
    }

    private void StartGuard()// ���� �̺�Ʈ ���� ȣ��
    {
        //Debug.Log("���� Ȱ��ȭ");

        //GuardCol.SetActive(true);
        playerStat.ISGuarding = true;

    }

    private void EndGuard()// ���� �̺�Ʈ ���� ȣ��
    {
        Debug.Log("�÷��̾� ���尡 Ǯ��");

        //GuardCol.SetActive(false);
        playerStat.ISGuarding = false;

    }


}
