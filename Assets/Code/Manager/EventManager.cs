using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //InitializeTagDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public event Action<int> OnCustomEvent;  // 여러 스크립트에서 정수값을 전달할 이벤트
    public event Action<AnimationTag> OnCustomEvent2;

    public event Action OnCustomEvent3;
    public event Action EnemyAni;

    public void TriggerEvent(int index)
    {
        OnCustomEvent?.Invoke(index);  // 이벤트 호출
    }
    public void TriggerEvent()
    {
        OnCustomEvent3?.Invoke();  // PlayerStat으로 이벤트 호출

    }
    public void EnemyaniEvent()
    {
        EnemyAni?.Invoke();  // EnemyStat으로 이벤트 호출

    }

    //EventManager.Instance.TriggerEvent(index);  // 다른 스크립트에서 사용할 경우,
}
