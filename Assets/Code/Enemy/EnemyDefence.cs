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
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            //CharAni.ResetTrigger(animHash_Gurad);
            CharAni.ResetTrigger(animHash_GuradUP);
        }

        if (Input.GetButton("Fire2"))// S키 버튼
        {
            //Vector3 moveArrow = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            //float newAngle = Mathf.Atan2(moveArrow.x, moveArrow.z) * Mathf.Rad2Deg;
            //float targetAngle = Mathf.Round(newAngle / 45.0f) * 45.0f; // 8방향 스냅

            //if (moveArrow != Vector3.zero)
                //transform.parent.rotation = Quaternion.AngleAxis(targetAngle, Vector3.up);

            CharAni.SetTrigger(animHash_Gurad);
            EventManager.Instance.EnemyaniEvent();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            
            StartCoroutine("GuardDisable");

        }
    }

    IEnumerator GuardDisable()
    {
        StopCoroutine(GuardDisable());

        yield return new WaitForSeconds(0.05f);

        GuardCol.SetActive(false);//빨리 연타하면 가드가 바로 사라지는 데, 이를 몇번 시도해보고 남아있는 지 여부를 설정
        CharAni.ResetTrigger(animHash_Gurad);
        CharAni.SetTrigger(animHash_GuradUP);

        CharAni.SetBool("isWalk", false);
        CharAni.ResetTrigger("isGuardSucess");
    }

    private void StartGuard()// 가드 이벤트 상태 호출
    {
        //Debug.Log("가드 활성화");
        GuardCol.SetActive(true);

    }

    private void EndGuard()// 가드 이벤트 상태 호출
    {
        //Debug.Log("가드가 풀림");
        GuardCol.SetActive(false);

    }
}
