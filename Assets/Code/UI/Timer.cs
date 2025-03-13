using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI textMeshProUGUI;

    //private float timer = 99;
    bool timerGo;
    void Start()
    {
        //StartCoroutine(CountdownTimer()); // 타이머 시작
        EventManager.Instance.PlayerDied += TimerStop;
        EventManager.Instance.EnemyDied += TimerStop;
    }

    private void OnDisable()
    {
        EventManager.Instance.PlayerDied -= TimerStop;
        EventManager.Instance.EnemyDied -= TimerStop;
    }

    public IEnumerator CountdownTimer()
    {
        timerGo = true;
        float timer = 99;

        while (timerGo)
        {
            if(timer > 0)
            {
                textMeshProUGUI.text = timer.ToString(); // UI 업데이트
                yield return new WaitForSeconds(1f); // 1초 대기
                timer--; // 시간 감소
            }
            if(timer <=0)
            {
                //textMeshProUGUI.text = "0"; // 타이머 종료 후 0 표시

                //Debug.Log("타이머 종료!");
            }
            
        }
                
    }

    void TimerStop()
    {
        timerGo = false;
    }
}
