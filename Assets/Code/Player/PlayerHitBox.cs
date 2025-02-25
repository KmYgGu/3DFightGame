using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private bool isInvulnerable = false; // ���� �ð� ���� ���� ���� üũ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground")) return;

        if (isInvulnerable) return;

        //Debug.Log("������");
        StartCoroutine(ActivateInvulnerability());
    }

    private IEnumerator ActivateInvulnerability()
    {
        isInvulnerable = true; // ���� ���� Ȱ��ȭ

        yield return new WaitForSeconds(0.5f); // 0.5�� ���� ���� ����

        //yield return new WaitForEndOfFrame();
        isInvulnerable = false; // �ٽ� ���� ���� �� �ֵ��� ����
    }
}
