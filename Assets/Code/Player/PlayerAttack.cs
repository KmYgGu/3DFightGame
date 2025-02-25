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
            pressStartTime = Time.time; // 현재 시간 저장
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isHolding = false;
            float heldDuration = Time.time - pressStartTime; // 누른 시간 계산
            //Debug.Log($"Fire 버튼을 {heldDuration:F2}초 동안 눌렀음");
            if (heldDuration < 1f)
            {
                Debug.Log("짧게 눌렀음 → 일반 공격");
            }
            else if (heldDuration < 3f)
            {
                Debug.Log("중간 길이로 눌렀음 → 강한 공격");
            }
            else
            {
                Debug.Log("오래 눌렀음 → 특수 기술 발동!");
            }
        }
    }
}
