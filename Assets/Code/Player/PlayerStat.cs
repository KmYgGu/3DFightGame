using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    //�ɷ�ġ�� ��ȭ�� ���� ������ EventManager�� ȣ��

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
        //���⿡ ���� �̺�Ʈ�� ���� ��ġ�� ����
    }
}
