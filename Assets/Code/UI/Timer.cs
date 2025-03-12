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
        StartCoroutine(CountdownTimer()); // 타이머 시작
    }

    IEnumerator CountdownTimer()
    {
        while (timer > 0)
        {
            textMeshProUGUI.text = timer.ToString(); // UI 업데이트
            yield return new WaitForSeconds(1f); // 1초 대기
            timer--; // 시간 감소
        }

        textMeshProUGUI.text = "0"; // 타이머 종료 후 0 표시
        Debug.Log("타이머 종료!");
    }
}
