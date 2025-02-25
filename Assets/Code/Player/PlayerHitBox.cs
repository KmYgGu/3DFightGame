using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private bool isInvulnerable = false; // 일정 시간 동안 무적 여부 체크

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground")) return;

        if (isInvulnerable) return;

        //Debug.Log("감지됨");
        StartCoroutine(ActivateInvulnerability());
    }

    private IEnumerator ActivateInvulnerability()
    {
        isInvulnerable = true; // 무적 상태 활성화

        yield return new WaitForSeconds(0.5f); // 0.5초 동안 공격 무시

        //yield return new WaitForEndOfFrame();
        isInvulnerable = false; // 다시 공격 받을 수 있도록 설정
    }
}
