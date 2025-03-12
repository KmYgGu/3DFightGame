using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI textMeshProUGUI;

    private float timer = 99;
    void Start()
    {
        StartCoroutine(CountdownTimer()); // Ÿ�̸� ����
    }

    IEnumerator CountdownTimer()
    {
        while (timer > 0)
        {
            textMeshProUGUI.text = timer.ToString(); // UI ������Ʈ
            yield return new WaitForSeconds(1f); // 1�� ���
            timer--; // �ð� ����
        }

        textMeshProUGUI.text = "0"; // Ÿ�̸� ���� �� 0 ǥ��
        Debug.Log("Ÿ�̸� ����!");
    }
}
