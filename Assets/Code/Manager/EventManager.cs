using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        // �̱��� ����
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

    public event Action<int> OnCustomEvent;  // ���� ��ũ��Ʈ���� �������� ������ �̺�Ʈ
    public event Action<AnimationTag> OnCustomEvent2;

    public event Action OnCustomEvent3;
    public event Action EnemyAni;

    public void TriggerEvent(int index)
    {
        OnCustomEvent?.Invoke(index);  // �̺�Ʈ ȣ��
    }
    public void TriggerEvent()
    {
        OnCustomEvent3?.Invoke();  // PlayerStat���� �̺�Ʈ ȣ��

    }
    public void EnemyaniEvent()
    {
        EnemyAni?.Invoke();  // EnemyStat���� �̺�Ʈ ȣ��

    }

    //EventManager.Instance.TriggerEvent(index);  // �ٸ� ��ũ��Ʈ���� ����� ���,
}
