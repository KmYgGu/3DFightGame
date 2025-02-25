using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float pressStartTime = 0f;
    bool isHolding = false;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isHolding = true;
            pressStartTime = Time.time; // ���� �ð� ����
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isHolding = false;
            float heldDuration = Time.time - pressStartTime; // ���� �ð� ���
            //Debug.Log($"Fire ��ư�� {heldDuration:F2}�� ���� ������");
            if (heldDuration < 1f)
            {
                Debug.Log("ª�� ������ �� �Ϲ� ����");
            }
            else if (heldDuration < 3f)
            {
                Debug.Log("�߰� ���̷� ������ �� ���� ����");
            }
            else
            {
                Debug.Log("���� ������ �� Ư�� ��� �ߵ�!");
            }
        }
    }
}
