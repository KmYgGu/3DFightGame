using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefenceBox : MonoBehaviour
{
    public bool isDenfence = false;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground")) return;

        if (other.CompareTag("PlayerBody")) return;

        if (other.CompareTag("EnemyBody")) return;

        if (other.CompareTag("Guard")) return;

        if (!isDenfence)
        {
            isDenfence = true; // 방패가 충돌했음을 표시
            //Debug.Log("방패가 먼저 충돌!");
        }
    }
}
