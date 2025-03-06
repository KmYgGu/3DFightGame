using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ĳ���Ͱ� ������ �ϴ� ��, ĳ������ ���� ���⿡ ���� ������ �ڵ����� ���� ���ϴ� ��ũ��Ʈ
public class LookViewAttack : MonoBehaviour
{
    [SerializeField]private Transform target;  // ��� ĳ������ Transform
    private float viewAngle = 120f; // �þ� ���� �� -30, �� +30
    private float viewDistance = 1f; // ���� �Ÿ�

    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        CheckAndRotateToTarget();
    }

    void CheckAndRotateToTarget()
    {
        if (target == null) return;

        // �� ĳ���Ϳ� ��� ĳ������ ���� ����
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        // �� ĳ������ ���� ����
        Vector3 forward = transform.forward;//new Vector3(0, transform.position.y, 0);

        // �� ĳ������ Y�� ȸ�� ���� üũ
        float angleToTarget = Vector3.Angle(forward, directionToTarget);

        // �Ÿ� üũ
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // �þ� ������ �Ÿ� ���� Ȯ��
        if (angleToTarget <= viewAngle && distanceToTarget <= viewDistance)
        {
            Debug.Log("�� �߰�");

            transform.rotation = Quaternion.LookRotation(directionToTarget);
        }
    }
}
