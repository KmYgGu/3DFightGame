using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    //능력치에 변화가 있을 때마다 EventManager를 호출

    private void OnEnable()
    {
        EventManager.Instance.OnCustomEvent += OnEventReceived;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnCustomEvent -= OnEventReceived;
    }

    private void OnEventReceived()
    {
        //여기에 여러 이벤트를 받은 수치를 정리
    }
}
