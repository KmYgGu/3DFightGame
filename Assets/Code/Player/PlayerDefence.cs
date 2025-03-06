using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefence : MonoBehaviour
{
    private Animator CharAni;

    private int animHash_Gurad = Animator.StringToHash("isGuard");
    private int animHash_GuradUP = Animator.StringToHash("isGuardUp");

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

        if (Input.GetButton("Fire2"))// S키 버튼
        {
            CharAni.SetTrigger(animHash_Gurad);
            EventManager.Instance.TriggerEvent();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            CharAni.ResetTrigger(animHash_Gurad);
            CharAni.SetTrigger(animHash_GuradUP);
            //StartCoroutine("GuardToidle");
        }
    }

    IEnumerator GuardToidle()
    {
        yield return new WaitForSeconds(0.26f);
        //EventManager.Instance.TriggerEvent();// idle 상태를 출력하기 위한 부분
    }
}
