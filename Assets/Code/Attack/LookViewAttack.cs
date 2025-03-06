using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터가 공격을 하는 데, 캐릭터의 공격 방향에 적이 있으면 자동으로 적을 향하는 스크립트
public class LookViewAttack : MonoBehaviour
{
    [SerializeField]private Transform target;  // 상대 캐릭터의 Transform
    private float viewAngle = 120f; // 시야 범위 좌 -30, 우 +30
    private float viewDistance = 1f; // 감지 거리

    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        CheckAndRotateToTarget();
    }

    void CheckAndRotateToTarget()
    {
        if (target == null) return;

        // 내 캐릭터와 상대 캐릭터의 방향 벡터
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        // 내 캐릭터의 전방 벡터
        Vector3 forward = transform.forward;//new Vector3(0, transform.position.y, 0);

        // 내 캐릭터의 Y축 회전 각도 체크
        float angleToTarget = Vector3.Angle(forward, directionToTarget);

        // 거리 체크
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // 시야 범위와 거리 조건 확인
        if (angleToTarget <= viewAngle && distanceToTarget <= viewDistance)
        {
            Debug.Log("적 발견");

            transform.rotation = Quaternion.LookRotation(directionToTarget);
        }
    }
}
