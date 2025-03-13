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
        //StartCoroutine(CountdownTimer()); // Ÿ�̸� ����
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
                textMeshProUGUI.text = timer.ToString(); // UI ������Ʈ
                yield return new WaitForSeconds(1f); // 1�� ���
                timer--; // �ð� ����
            }
            if(timer <=0)
            {
                //textMeshProUGUI.text = "0"; // Ÿ�̸� ���� �� 0 ǥ��

                //Debug.Log("Ÿ�̸� ����!");
            }
            
        }
                
    }

    void TimerStop()
    {
        timerGo = false;
    }
}
